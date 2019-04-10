using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 怪物实体类
    /// </summary>
	public class Monster : Entity
	{
		/// <summary>
		/// 怪物实体数据
		/// </summary>
		public MonsterData MonsterStats;
		
		protected override void OnInit(object userData)
		{
			base.OnInit(userData);
			MonsterStats = (MonsterData) userData;
		}

		protected override void OnShow(object userData)
		{
			base.OnShow(userData);
		}

		protected override void OnHide(object userData)
		{
			base.OnHide(userData);
		}
	}
}

