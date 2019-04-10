using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 商店表
    /// </summary>
	public class DRStore : IDataRow 
	{
		/// <summary>
		/// 商店编号
		/// </summary>
		public int Id { get; private set; }
		
		/// <summary>
		/// 商店名称
		/// </summary>
		public string Name { get; private set; }
		
		/// <summary>
		/// 道具编号列表
		/// </summary>
		public List<int> Items { get; private set; }
		
		public void ParseDataRow(string dataRowText)
		{
			var text = DataTableExtension.SplitDataRow(dataRowText);
			var index = 0;
			index++;
			Id = int.Parse(text[index++]);
			Name = text[index++];
			Items = Split(text[index++]);
		}

		/// <summary>
		/// 分割字符串
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private List<int> Split(string str)
		{
			var text = DataTableExtension.SplitValue(str);
			var list = new List<int>();
			foreach (var s in text)
			{
				list.Add(int.Parse(s));
			}

			return list;
		}
		
	}
}

