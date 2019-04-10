using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 人类装备表
    /// </summary>
	public class DRHumanEquip : IDataRow 
	{
		public int Id { get; private set; }
		
		/// <summary>
		/// 部位
		/// </summary>
		public HumanPartType PartType { get; private set; }
		
		/// <summary>
		/// 可装备的玩家类型集合
		/// </summary>
		public List<PlayerType> UsablePlayerTypes;
		
		/// <summary>
		/// 攻击力
		/// </summary>
		public int Attack { get; private set; }
		
		/// <summary>
		/// 防御力
		/// </summary>
		public int Defence { get; private set; }
		
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
		    PartType = (HumanPartType) int.Parse(text[index++]);
			
			int[] canWorn = Split(text[index++]);
			UsablePlayerTypes = new List<PlayerType>();
			if (canWorn[0] == 1)
			{
				UsablePlayerTypes.Add(PlayerType.Rebana);
			}
			if (canWorn[1] == 1)
			{
				UsablePlayerTypes.Add(PlayerType.Cliff);
			}
			if (canWorn[2] == 1)
			{
				UsablePlayerTypes.Add(PlayerType.Margaret);
			}
			
			Attack = int.Parse(text[index++]);
			Defence = int.Parse(text[index++]);
		    RangeType = (InfluenceRangeType) int.Parse(text[index++]);
		}
		
		/// <summary>
		/// 分割字符串
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private int[] Split(string str)
		{
			string[] text = DataTableExtension.SplitValue(str);
			return new[] {int.Parse(text[0]), int.Parse(text[1]), int.Parse(text[2])};
		}
	}
}

