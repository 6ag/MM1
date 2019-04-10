using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 等级数据表
    /// </summary>
	public class DRLevel : IDataRow 
	{
		/// <summary>
		/// 等级编号
		/// </summary>
		public int Id { get; private set; }
		
		/// <summary>
		/// 等级
		/// </summary>
		public int Level { get; private set; }
		
		/// <summary>
		/// 所需经验
		/// </summary>
		public int Exp { get; private set; }
		
		public void ParseDataRow(string dataRowText)
		{
			string[] text = DataTableExtension.SplitDataRow(dataRowText);
			int index = 0;
			index++;
			Id = int.Parse(text[index++]);
			Level = int.Parse(text[index++]);
			Exp = int.Parse(text[index++]);
		}
	}
}

