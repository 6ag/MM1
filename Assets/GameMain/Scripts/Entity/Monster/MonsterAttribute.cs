using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 怪物属性
    /// </summary>
	[Serializable]
	public class MonsterAttribute 
	{
		/// <summary>
		/// 血量/装甲
		/// </summary>
		public int HP;
		
		/// <summary>
		/// 行动次数
		/// </summary>
		public float ActionCount;
		
		/// <summary>
		/// 攻击
		/// </summary>
		public int Attack;
		
		/// <summary>
		/// 防御
		/// </summary>
		public int Defence;
		
		/// <summary>
		/// 护甲
		/// </summary>
		public int Armor;
		
		/// <summary>
		/// 速度
		/// </summary>
		public int Speed;
		
		/// <summary>
		/// 命中率
		/// </summary>
		public int Hit;
		
		/// <summary>
		/// 闪避
		/// </summary>
		public int Dodge;

		public MonsterAttribute(int hp, float actionCount, int attack, int defence, int armor, int speed, int hit, int dodge)
		{
			HP = hp;
			ActionCount = actionCount;
			Attack = attack;
			Defence = defence;
			Armor = armor;
			Speed = speed;
			Hit = hit;
			Dodge = dodge;
		}
	}
}

