using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 物品表（除了装备）
    /// </summary>
	public class DRGoods : IDataRow 
	{
        /// <summary>
        /// 道具编号
        /// </summary>
        public int Id { get; private set; }
		
		/// <summary>
		/// 物品类型 1补充 2攻击 3辅助 4工具 5杂物
		/// </summary>
		public GoodsType GoodsType { get; private set; }
		
		/// <summary>
		/// 使用类型 1人类 2战车
		/// </summary>
		public int UseType { get; private set; }
		
		/// <summary>
		/// 是否能在战斗中使用
		/// </summary>
		public bool CanCombat { get; private set; }
		
		/// <summary>
		/// 消耗类型 1一次性 2无限次
		/// </summary>
		public GoodsConsumeType ConsumeType { get; private set; }
		
		/// <summary>
		/// 恢复人类HP
		/// </summary>
		public int HP { get; private set; }
		
		/// <summary>
		/// 恢复战车装甲片
		/// </summary>
		public int PAG { get; private set; }
		
		/// <summary>
		/// 攻击力
		/// </summary>
		public int Attack { get; private set; }
		
		/// <summary>
		/// 作用范围 1单体 2一组 3全体
		/// </summary>
		public InfluenceRangeType RangeType { get; private set; }
		
		public void ParseDataRow(string dataRowText)
		{
			string[] text = DataTableExtension.SplitDataRow(dataRowText);
			int index = 0;
			index++;
			Id = int.Parse(text[index++]);
			GoodsType = (GoodsType) int.Parse(text[index++]);
			UseType = int.Parse(text[index++]);
			CanCombat = bool.Parse(text[index++]);
		    ConsumeType = (GoodsConsumeType) int.Parse(text[index++]);
			HP = int.Parse(text[index++]);
			PAG = int.Parse(text[index++]);
			Attack = int.Parse(text[index++]);
		    RangeType = (InfluenceRangeType) int.Parse(text[index++]);
		}

	}
}

