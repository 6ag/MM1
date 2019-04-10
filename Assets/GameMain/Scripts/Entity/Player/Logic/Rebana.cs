using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine;
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
	/// <summary>
    /// 玩家实体类
    /// </summary>
	public class Rebana : Entity 
	{
	    /// <summary>
	    /// 是否在繁忙状态
	    /// </summary>
	    public bool IsBusy = false;

        /// <summary>
        /// 角色实体数据
        /// </summary>
        public PlayerData PlayerStats;

        /// <summary>
        /// 角色移动控制脚本
        /// </summary>
	    private RebanaMovement m_Movement;

		/// <summary>
		/// 获取玩家实体类
		/// </summary>
		/// <returns></returns>
		public static Rebana GetPlayer()
		{
			return ((Rebana) GameEntry.Entity.GetEntity(1).Logic);
		}

        protected override void OnInit(object userData)
		{
			base.OnInit(userData);
			PlayerStats = (PlayerData) userData;
		}

		protected override void OnShow(object userData)
		{
			base.OnShow(userData);

		    m_Movement = GetComponent<RebanaMovement>();
            
            // 设置相机
			if (Camera.main != null)
			{
				var cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
				cinemachineBrain.ActiveVirtualCamera.Follow = CachedTransform;
				var cinemachine2D = (CinemachineVirtualCamera) cinemachineBrain.ActiveVirtualCamera;
				cinemachine2D.m_Lens.OrthographicSize = 4.5f;
			}
		}

	    protected override void OnHide(object userData)
	    {
	        base.OnHide(userData);
	    }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(elapseSeconds, realElapseSeconds);
		}

		/// <summary>
		/// UI上右下角的按钮点击
		/// </summary>
		public void OnConfirmButtonClick()
		{
			var go = DetectionFront(1f);
			if (go == null)
			{
				Log.Debug("第一次检测，没有检测到碰撞器");
				return;
			}

            // 检测到前方是桌子
		    if (go.CompareTag("DeskBarrier"))
		    {
		        go = DetectionFront(2f);
            }

		    if (go == null)
		    {
		        Log.Debug("第二次检测，没有检测到碰撞器");
		        return;
		    }

            // 检测到前方有NPC
            if (go.CompareTag("NPC"))
			{
				var npc = go.GetComponent<NPC>();
				if (npc != null)
				{
					npc.Interaction();
				}
			}
		}

        /// <summary>
        /// 侦查角色前方的游戏物体（只有带碰撞器的游戏物体才会被检测）
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private GameObject DetectionFront(float distance)
		{
			var checkPos = Vector2.one;
			switch (m_Movement.Direction)
			{
				case DirectionType.Up:
					checkPos = transform.position + new Vector3(0, distance);
					break;
				case DirectionType.Down:
					checkPos = transform.position + new Vector3(0, -distance);
					break;
				case DirectionType.Left:
					checkPos = transform.position + new Vector3(-distance, 0);
					break;
				case DirectionType.Right:
					checkPos = transform.position + new Vector3(distance, 0);
					break;
			}
			Log.Debug("检测坐标 " + checkPos);
			var hit = Physics2D.Linecast(checkPos, checkPos);
			return hit.collider != null ? hit.collider.gameObject : null;
		}
		
    }
}

