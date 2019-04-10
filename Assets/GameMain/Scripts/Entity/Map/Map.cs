using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
	/// <summary>
    /// 
    /// </summary>
	public class Map : Entity
	{
        /// <summary>
        /// 地图实体数据
        /// </summary>
	    public MapData MapData;
		
		/// <summary>
		/// 当前地图上的NPC集合
		/// </summary>
		private List<int> m_NpcList;

	    protected override void OnInit(object userData)
	    {
	        base.OnInit(userData);
	        MapData = (MapData) userData;
		    m_NpcList = new List<int>();
		    Log.Debug("初始化NPC集合");
	    }

	    protected override void OnShow(object userData)
		{
			base.OnShow(userData);

			GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, ShowEntitySuccess);
			
			// 创建地图上的NPC
			CreateNPCs();
			Log.Debug("创建NPC");
		}

		protected override void OnHide(object userData)
		{
			base.OnHide(userData);
			GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, ShowEntitySuccess);
			
			// 隐藏地图上的NPC
			HideNPCs();
			Log.Debug("销毁NPC");
		}

		protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(elapseSeconds, realElapseSeconds);
			
		}

		/// <summary>
		/// 创建地图上的NPC
		/// </summary>
		private void CreateNPCs()
		{
			var dtNpc = GameEntry.DataTable.GetDataTable<DRNPC>();
			foreach (var npciId in MapData.NPCs)
			{
				var drNpc = dtNpc.GetDataRow(npciId);
				if (drNpc == null)
				{
					continue;
				}
				GameEntry.Entity.ShowNPC(new NPCData(drNpc.Id, drNpc.NpcType, drNpc.StoreId, drNpc.BornPosition, drNpc.BornAnimation, drNpc.Conversation, drNpc.Patrol));
			}
		}

		/// <summary>
		/// 隐藏地图上的NPC
		/// </summary>
		private void HideNPCs()
		{
			foreach (var npcId in m_NpcList)
			{
				if (GameEntry.Entity.HasEntity(npcId))
				{
					GameEntry.Entity.HideEntity(npcId);
				}
			}
		}
		
		/// <summary>
		/// 显示实体成功
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowEntitySuccess(object sender, GameEventArgs e)
		{
			var args = (ShowEntitySuccessEventArgs) e;
			var npcData = args.UserData as NPCData;
			if (npcData != null)
			{
				m_NpcList.Add(npcData.Id);
			}
		}
		
	}
}

