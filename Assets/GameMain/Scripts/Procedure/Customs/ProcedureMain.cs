using System.Collections.Generic;
using Cinemachine;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace MetalMax
{
    /// <summary>
    /// 主流程 控制游戏
    /// </summary>
    public class ProcedureMain : ProcedureBase
    {
        private Game m_Game;

        /// <summary>
        /// 是否正在战斗
        /// </summary>
        private bool m_IsCombat = false;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_IsCombat = false;
            
            GameEntry.Event.Subscribe(StartCombatEventArgs.EventId, StartCombatEventHandler);

            // 是否有存档
            if (GameEntry.Archive.Load())
            {
                // 读取存档游戏
                m_Game = new ContinueGame();
                m_Game.Initialize();
            }
            else
            {
                // 初始化新游戏
                m_Game = new NewGame();
                m_Game.Initialize();
            }

            // 打开游戏场景主界面
            GameEntry.UI.OpenUIForm(UIFormId.MainForm, this);
        }

        /// <summary>
        /// 开始战斗事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartCombatEventHandler(object sender, GameEventArgs e)
        {
            m_IsCombat = true;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // 切换到战斗流程
            if (m_IsCombat)
            {
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId, 3);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(StartCombatEventArgs.EventId, StartCombatEventHandler);
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);

        }
    }
}
