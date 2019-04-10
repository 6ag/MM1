using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 道具表（所有道具）
    /// </summary>
	public class DRItem : IDataRow 
	{
		/// <summary>
		/// 物品编号
		/// </summary>
		public int Id { get; private set; }
		
		/// <summary>
		/// 道具名称
		/// </summary>
		public string Name { get; private set; }
		
		/// <summary>
		/// 图标
		/// </summary>
		public string Icon { get; private set; }
		
		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; private set; }
		
		/// <summary>
		/// 类型 1物品 2人类装备 3战车装备
		/// </summary>
		public ItemType ItemType { get; private set; }
		
		/// <summary>
		/// 最大堆叠数量
		/// </summary>
		public int StackCount { get; private set ;}
		
		/// <summary>
		/// 是否可丢弃
		/// </summary>
		public bool Discard { get; private set; }
		
		/// <summary>
		/// 购买价格
		/// </summary>
		public int BuyPrice { get; private set; }
		
		/// <summary>
		/// 出售价格
		/// </summary>
		public int SellPrice { get; private set; }
		
		public void ParseDataRow(string dataRowText)
		{
			string[] text = DataTableExtension.SplitDataRow(dataRowText);
			int index = 0;
			index++;
			Id = int.Parse(text[index++]);
			Name = text[index++];
			Icon = text[index++];
			Description = text[index++];
			ItemType = (ItemType) int.Parse(text[index++]);
			StackCount = int.Parse(text[index++]);
			Discard = bool.Parse(text[index++]);
			BuyPrice = int.Parse(text[index++]);
			SellPrice = int.Parse(text[index++]);
		}
	}
}

