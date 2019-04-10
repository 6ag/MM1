using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// NPC实体数据类
    /// </summary>
	public class NPCData : EntityData 
	{
		/// <summary>
		/// NPC类型
		/// </summary>
		public NPCType NpcType { get; private set; }
		
		/// <summary>
		/// 商店编号
		/// </summary>
		public int StoreId { get; private set; }
		
		/// <summary>
		/// 初始位置
		/// </summary>
		public Vector3 BornPosition { get; private set; } 
		
		/// <summary>
		/// 初始动画名称
		/// </summary>
		public string BornAnimation { get; private set; }
		
		/// <summary>
		/// 对话名称
		/// </summary>
		public string Conversation { get; private set; }
		
		/// <summary>
		/// 是否可以巡逻
		/// </summary>
		public bool Patrol { get; private set; }

		public NPCData(int id, NPCType npcType, int storeId, Vector3 bornPosition, string bornAnimation, string conversation, bool patrol) : base(id)
		{
			NpcType = npcType;
			StoreId = storeId;
			BornPosition = bornPosition;
			BornAnimation = bornAnimation;
			Conversation = conversation;
			Patrol = patrol;
		}
	}
}

