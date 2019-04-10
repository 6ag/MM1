using System;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace MetalMax
{
    /// <summary>
    /// 菜单流程 一般是启动游戏后的主菜单流程
    /// </summary>
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartGame = false;
        
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            m_StartGame = false;
            
            // 订阅事件
            GameEntry.Event.Subscribe(NewGameEventArgs.EventId, StartNewGame);
            
            // 打开闪屏动画界面
            GameEntry.UI.OpenUIForm(UIFormId.SplashForm, this);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            // 取消订阅事件
            GameEntry.Event.Unsubscribe(NewGameEventArgs.EventId, StartNewGame);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_StartGame)
            {
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, 2);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        /// <summary>
        /// 开始新游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gameEventArgs"></param>
        private void StartNewGame(object sender, GameEventArgs gameEventArgs)
        {
            m_StartGame = true;
        }
    }
}
