﻿// Copyright 2014 ideafixxxer
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Ideafixxxer.CsvParser
{
	public class CsvParser
	{
		private const char CommaCharacter = ',';
		private const char QuoteCharacter = '"';

		#region Nested types

		private abstract class ParserState
		{
			public static readonly LineStartState LineStartState = new LineStartState();
			public static readonly ValueStartState ValueStartState = new ValueStartState();
			public static readonly ValueState ValueState = new ValueState();
			public static readonly QuotedValueState QuotedValueState = new QuotedValueState();
			public static readonly QuoteState QuoteState = new QuoteState();

			public abstract ParserState AnyChar(char ch, ParserContext context);
			public abstract ParserState Comma(ParserContext context);
			public abstract ParserState Quote(ParserContext context);
			public abstract ParserState EndOfLine(ParserContext context);
		}

		private class LineStartState : ParserState
		{
			public override ParserState AnyChar(char ch, ParserContext context)
			{
				context.AddChar(ch);
				return ValueState;
			}

			public override ParserState Comma(ParserContext context)
			{
				context.AddValue();
				return ValueStartState;
			}

			public override ParserState Quote(ParserContext context)
			{
				return QuotedValueState;
			}

			public override ParserState EndOfLine(ParserContext context)
			{
				context.AddLine();
				return LineStartState;
			}
		}

		private class ValueStartState : LineStartState
		{
			public override ParserState EndOfLine(ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return LineStartState;
			}
		}

		private class ValueState : ParserState
		{
			public override ParserState AnyChar(char ch, ParserContext context)
			{
				context.AddChar(ch);
				return ValueState;
			}

			public override ParserState Comma(ParserContext context)
			{
				context.AddValue();
				return ValueStartState;
			}

			public override ParserState Quote(ParserContext context)
			{
				context.AddChar(QuoteCharacter);
				return ValueState;
			}

			public override ParserState EndOfLine(ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return LineStartState;
			}
		}

		private class QuotedValueState : ParserState
		{
			public override ParserState AnyChar(char ch, ParserContext context)
			{
				context.AddChar(ch);
				return QuotedValueState;
			}

			public override ParserState Comma(ParserContext context)
			{
				context.AddChar(CommaCharacter);
				return QuotedValueState;
			}

			public override ParserState Quote(ParserContext context)
			{
				return QuoteState;
			}

			public override ParserState EndOfLine(ParserContext context)
			{
				context.AddChar('\r');
				context.AddChar('\n');
				return QuotedValueState;
			}
		}

		private class QuoteState : ParserState
		{
			public override ParserState AnyChar(char ch, ParserContext context)
			{
				//undefined, ignore "
				context.AddChar(ch);
				return QuotedValueState;
			}

			public override ParserState Comma(ParserContext context)
			{
				context.AddValue();
				return ValueStartState;
			}

			public override ParserState Quote(ParserContext context)
			{
				context.AddChar(QuoteCharacter);
				return QuotedValueState;
			}

			public override ParserState EndOfLine(ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return LineStartState;
			}
		}

		private class ParserContext
		{
			private readonly StringBuilder _currentValue = new StringBuilder();
			private readonly List<string[]> _lines = new List<string[]>();
			private readonly List<string> _currentLine = new List<string>();

			public void AddChar(char ch)
			{
				_currentValue.Append(ch);
			}

			public void AddValue()
			{
				_currentLine.Add(_currentValue.ToString());
				_currentValue.Remove(0, _currentValue.Length);
			}

			public void AddLine()
			{
				_lines.Add(_currentLine.ToArray());
				_currentLine.Clear();
			}

			public List<string[]> GetAllLines()
			{
				if (_currentValue.Length > 0)
				{
					AddValue();
				}
				if (_currentLine.Count > 0)
				{
					AddLine();
				}
				return _lines;
			}
		}

		#endregion

		public string[][] Parse(string csvData)
		{
			var context = new ParserContext();

			// Handle both Windows and Mac line endings
			var lines = Regex.Split(csvData, "\n|\r\n");

			ParserState currentState = ParserState.LineStartState;
			for (int i = 0; i < lines.Length; i++) {
				var next = lines [i];

				// Skip empty entries
				if (next.Length == 0)
				{
					continue;
				}

				for (int j = 0; j < next.Length; j++) {
					var ch = next [j];

					switch (ch) {
					case CommaCharacter:
						currentState = currentState.Comma (context);
						break;
					case QuoteCharacter:
						currentState = currentState.Quote (context);
						break;
					default:
						currentState = currentState.AnyChar (ch, context);
						break;
					}
				}
				currentState = currentState.EndOfLine (context);
			}

			List<string[]> allLines = context.GetAllLines();
			return allLines.ToArray();
		}
	}
}