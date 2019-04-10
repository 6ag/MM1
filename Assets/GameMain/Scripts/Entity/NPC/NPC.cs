using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using GameFramework;
using UnityEngine;
using Log = GameFramework.Log;

namespace MetalMax
{
	/// <summary>
    /// NPC实体类
    /// </summary>
	public class NPC : Entity
	{
		/// <summary>
		/// NPC实体数据
		/// </summary>
		public NPCData NpcData;

	    /// <summary>
	    /// 动画控制器
	    /// </summary>
	    private Animator m_Animator;

        protected override void OnInit(object userData)
		{
			base.OnInit(userData);
			NpcData = (NPCData) userData;
		}

		protected override void OnShow(object userData)
		{
			base.OnShow(userData);
			SetupNPC();

		    m_Animator = GetComponent<Animator>();

            BlockSignals.OnBlockStart += BlockSignalsOnOnBlockStart;
		    BlockSignals.OnBlockEnd += BlockSignalsOnOnBlockEnd;
        }

		protected override void OnHide(object userData)
		{
			base.OnHide(userData);

		    BlockSignals.OnBlockStart -= BlockSignalsOnOnBlockStart;
		    BlockSignals.OnBlockEnd -= BlockSignalsOnOnBlockEnd;
        }

		protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(elapseSeconds, realElapseSeconds);
		}

		/// <summary>
		/// 初始化NPC设置
		/// </summary>
		private void SetupNPC()
		{
			transform.position = NpcData.BornPosition;
			GetComponent<Animator>().SetTrigger(NpcData.BornAnimation);
			if (NpcData.Patrol && GetComponent<NPCPatrol>() == null)
			{
				gameObject.AddComponent<NPCPatrol>();
			}
		}

		/// <summary>
		/// NPC与玩家交互
		/// </summary>
		public void Interaction()
		{
			switch (NpcData.NpcType)
			{
				case NPCType.Normal:
				    Log.Debug(NpcData.Conversation);
                    Flowchart.BroadcastFungusMessage(NpcData.Conversation);
                    break;
				case NPCType.Shop:
					// 打开商店界面
					GameEntry.Controller.Shop.OpenShopForm(NpcData.StoreId);
					break;
			}
		}
        
	    private void BlockSignalsOnOnBlockStart(Block block)
	    {
            switch (Rebana.GetPlayer().GetComponent<RebanaMovement>().Direction)
	        {
	            case DirectionType.Up:
                    PlayIdle(DirectionType.Down);
	                break;
	            case DirectionType.Down:
	                PlayIdle(DirectionType.Up);
                    break;
	            case DirectionType.Left:
	                PlayIdle(DirectionType.Right);
                    break;
	            case DirectionType.Right:
	                PlayIdle(DirectionType.Left);
                    break;
	        }
	        Log.Debug("开始 " + block.BlockName);
        }

	    private void BlockSignalsOnOnBlockEnd(Block block)
	    {
            // 这里判断是否有状态在继续
	        
	        GetComponent<Animator>().SetTrigger(NpcData.BornAnimation);
            Log.Debug("结束 " + block.BlockName);
	    }

        /// <summary>
        /// 播放移动动画
        /// </summary>
        private void PlayRun(DirectionType direction)
	    {
	        if (direction == DirectionType.Right && !PlayingOrTransAnima("WalkingRight"))
	        {
	            m_Animator.SetTrigger("WalkingRight");
	        }
	        else if (direction == DirectionType.Left && !PlayingOrTransAnima("WalkingLeft"))
	        {
	            m_Animator.SetTrigger("WalkingLeft");
	        }
	        else if (direction == DirectionType.Up && !PlayingOrTransAnima("WalkingUp"))
	        {
	            m_Animator.SetTrigger("WalkingUp");
	        }
	        else if (direction == DirectionType.Down && !PlayingOrTransAnima("WalkingDown"))
	        {
	            m_Animator.SetTrigger("WalkingDown");
	        }
	    }

	    /// <summary>
	    /// 角色闲置移动
	    /// </summary>
	    private void PlayIdle(DirectionType direction)
	    {
	        if (direction == DirectionType.Right && !PlayingOrTransAnima("IdleRight"))
	        {
	            m_Animator.SetTrigger("IdleRight");
	        }
	        else if (direction == DirectionType.Left && !PlayingOrTransAnima("IdleLeft"))
	        {
	            m_Animator.SetTrigger("IdleLeft");
	        }
	        else if (direction == DirectionType.Up && !PlayingOrTransAnima("IdleUp"))
	        {
	            m_Animator.SetTrigger("IdleUp");
	        }
	        else if (direction == DirectionType.Down && !PlayingOrTransAnima("IdleDown"))
	        {
	            m_Animator.SetTrigger("IdleDown");
	        }
	    }

	    /// <summary>
	    /// 指定动画是否正在播放或者即将播放
	    /// </summary>
	    /// <param name="animName"></param>
	    /// <returns></returns>
	    private bool PlayingOrTransAnima(string animName)
	    {
	        var stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
	        var transInfo = m_Animator.GetAnimatorTransitionInfo(0);
	        return stateInfo.IsName(animName) || transInfo.IsName(animName);
	    }
    }
}


