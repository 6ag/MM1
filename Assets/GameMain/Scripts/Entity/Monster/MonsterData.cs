using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 怪物实体数据
    /// </summary>
	[Serializable]
	public class MonsterData : EntityData
	{
		/// <summary>
		/// 怪物名字
		/// </summary>
		public string Name;

		/// <summary>
		/// 精灵图片名称(加载精灵图片，给预设赋值)
		/// </summary>
		public string SpriteName;
		
		/// <summary>
		/// 怪物类型 1仿生 2电子 3坦克
		/// </summary>
		public MonsterType Type;
		
		/// <summary>
		/// 死亡后获得经验值
		/// </summary>
		public int Exp;
		
		/// <summary>
		/// 死亡后获得金币
		/// </summary>
		public int Gold;
		
		/// <summary>
		/// 死亡后掉落道具id 为0则不掉落，掉落是有几率的
		/// </summary>
		public int DropId;
		
		/// <summary>
		/// 总属性
		/// </summary>
		public MonsterAttribute TotalAttribute;
		
		/// <summary>
		/// 当前属性
		/// </summary>
		public MonsterAttribute CurrentAttribute;
		
		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="id">怪物编号</param>
		public MonsterData(int id) : base(id)
		{
			var row = GameEntry.DataTable.GetDataRow<DRMonster>(id);
			if (row == null) return;

			Name = row.Name;
			SpriteName = row.SpriteName;
			Type = row.Type;
			Exp = row.Exp;
			Gold = row.Gold;
			DropId = row.DropId;
			
			TotalAttribute = new MonsterAttribute(row.HP, row.ActionCount, row.Attack, row.Defence, row.Armor, row.Speed, row.Hit, row.Dodge);
			CurrentAttribute = new MonsterAttribute(row.HP, row.ActionCount, row.Attack, row.Defence, row.Armor, row.Speed, row.Hit, row.Dodge);
		}
	}
}

