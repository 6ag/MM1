using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 怪物数据表 有备注行
    /// </summary>
	public class DRMonster : IDRAssetsRow 
	{
		/// <summary>
		/// 怪物编号
		/// </summary>
		public int Id { get; private set; }
		
		/// <summary>
		/// 怪物名字
		/// </summary>
		public string Name { get; private set; }
		
		/// <summary>
		/// 精灵图片名称
		/// </summary>
		public string SpriteName { get; private set; }
		
		/// <summary>
		/// 资源名称
		/// </summary>
		public string AssetName { get; private set; }
		
		/// <summary>
		/// 怪物类型 1仿生 2电子 3坦克
		/// </summary>
		public MonsterType Type { get; private set; }
		
		/// <summary>
		/// 血量/装甲
		/// </summary>
		public int HP { get; private set; }
		
		/// <summary>
		/// 行动次数
		/// </summary>
		public float ActionCount { get; private set; }
		
		/// <summary>
		/// 攻击
		/// </summary>
		public int Attack { get; private set; }
		
		/// <summary>
		/// 防御
		/// </summary>
		public int Defence { get; private set; }
		
		/// <summary>
		/// 护甲
		/// </summary>
		public int Armor { get; private set; }
		
		/// <summary>
		/// 速度
		/// </summary>
		public int Speed { get; private set; }
		
		/// <summary>
		/// 命中率
		/// </summary>
		public int Hit { get; private set; }
		
		/// <summary>
		/// 闪避
		/// </summary>
		public int Dodge { get; private set; }
		
		/// <summary>
		/// 死亡后获得经验值
		/// </summary>
		public int Exp { get; private set; }
		
		/// <summary>
		/// 死亡后获得金币
		/// </summary>
		public int Gold { get; private set; }
		
		/// <summary>
		/// 死亡后掉落道具id 为0则不掉落，掉落是有几率的
		/// </summary>
		public int DropId { get; private set; }
		
		public void ParseDataRow(string dataRowText)
		{
			string[] text = DataTableExtension.SplitDataRow(dataRowText);
			int index = 0;
			index++;
			Id = int.Parse(text[index++]);
			index++;
			Name = text[index++];
			SpriteName = text[index++];
			AssetName = text[index++];
			Type = (MonsterType) int.Parse(text[index++]);
			HP = int.Parse(text[index++]);
			ActionCount = float.Parse(text[index++]);
			Attack = int.Parse(text[index++]);
			Defence = int.Parse(text[index++]);
			Armor = int.Parse(text[index++]);
			Speed = int.Parse(text[index++]);
			Hit = int.Parse(text[index++]);
			Dodge = int.Parse(text[index++]);
			Exp = int.Parse(text[index++]);
			Gold = int.Parse(text[index++]);
			DropId = int.Parse(text[index++]);
		}
	}
}

