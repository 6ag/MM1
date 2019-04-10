using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 玩家数据表
    /// </summary>
	public class DRPlayer : IDRAssetsRow 
	{
		/// <summary>
		/// 资源编号
		/// </summary>
		public int Id { get; private set; }
		
		/// <summary>
		/// 资源名称
		/// </summary>
		public string AssetName { get; private set; }
		
		/// <summary>
		/// 等级
		/// </summary>
		public int Level { get; private set; }
		
		/// <summary>
		/// 战斗等级
		/// </summary>
		public int CombatLevel { get; private set; }
		
		/// <summary>
		/// 修理等级
		/// </summary>
		public int RepairLevel { get; private set; }
		
		/// <summary>
		/// 驾驶等级
		/// </summary>
		public int DriveLevel { get; private set; }
		
		/// <summary>
		/// 血量
		/// </summary>
		public int HP { get; private set; }
		
		/// <summary>
		/// 攻击
		/// </summary>
		public int Attack { get; private set; }
		
		/// <summary>
		/// 防御
		/// </summary>
		public int Defence { get; private set; }
		
		/// <summary>
		/// 强度
		/// </summary>
		public int Strength { get; private set; }
		
		/// <summary>
		/// 智力
		/// </summary>
		public int Intelligence { get; private set; }
		
		/// <summary>
		/// 速度
		/// </summary>
		public int Speed { get; private set; }
		
		/// <summary>
		/// 体力
		/// </summary>
		public int PhysicalPower { get; private set; }
		
		/// <summary>
		/// 经验值
		/// </summary>
		public int Exp { get; private set; }
		
		/// <summary>
		/// 初始装备编号
		/// </summary>
		public List<int> InitEquipIds { get; private set; }
		
		/// <summary>
		/// 初始背包道具编号
		/// </summary>
		public List<int> InitItemIds { get; private set; }
		
		public void ParseDataRow(string dataRowText)
		{
			string[] text = DataTableExtension.SplitDataRow(dataRowText);
			int index = 0;
			index++;
			Id = int.Parse(text[index++]);
			index++;
			AssetName = text[index++];
			Level = int.Parse(text[index++]);
			CombatLevel = int.Parse(text[index++]);
			RepairLevel = int.Parse(text[index++]);
			DriveLevel = int.Parse(text[index++]);
			HP = int.Parse(text[index++]);
			Attack = int.Parse(text[index++]);
			Defence = int.Parse(text[index++]);
			Strength = int.Parse(text[index++]);
			Intelligence = int.Parse(text[index++]);
			Speed = int.Parse(text[index++]);
			PhysicalPower = int.Parse(text[index++]);
			Exp = int.Parse(text[index++]);
			InitEquipIds = Split(text[index++]);

		    var str = text[index++];
            // 初始道具可能为空
            if (str.Length > 0)
		    {
		        InitItemIds = Split(str);
            }
            else
            {
                InitItemIds = new List<int>();
            }
            
		}

		private List<int> Split(string str)
		{
			var text = DataTableExtension.SplitValue(str);
			return text.Select(int.Parse).ToList();
		}
	}
}

