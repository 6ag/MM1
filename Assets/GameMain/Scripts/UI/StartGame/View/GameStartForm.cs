using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 游戏开始界面
    /// </summary>
    public class GameStartForm : UGuiForm
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GameEntry.Sound.PlayMusic(20);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(object userData)
        {
            base.OnClose(userData);
        }

        protected override void OnButtonClick(GameObject go)
        {
            base.OnButtonClick(go);
            switch (go.name)
            {
                case "NewGameButton":
                    OnNewGameButtonClick();
                    break;
                case "ContinueGameButton":

                    break;
                case "SettingButton":

                    break;
                case "AboutButton":

                    break;
            }
        }

        /// <summary>
        /// 新的征程
        /// </summary>
        private void OnNewGameButtonClick()
        {
            GameEntry.DataNode.SetData<VarString>(Constant.NodeKey.PlayerName, "雷班纳");
            GameEntry.Event.Fire(this, ReferencePool.Acquire<NewGameEventArgs>());
            GameEntry.UI.CloseUIForm(this);
        }
    }
}