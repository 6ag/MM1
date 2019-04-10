using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 游戏场景主界面
    /// </summary>
    public class MainForm : UGuiForm
    {
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            ChangeVehicleState();
        }

        #region 按钮点击事件

        protected override void OnButtonClick(GameObject go)
        {
            base.OnButtonClick(go);
            switch (go.name)
            {
                case "InteractButton":
                    Rebana.GetPlayer().OnConfirmButtonClick();
                    break;
                case "VehicleOnOffButton":
                    OnVehicleOnOffButtonClick();
                    break;
                case "HumanEquipButton":
                    GameEntry.Controller.CharacterInfo.OpenCharacterForm();
                    break;
                case "KnapsackButton":
                    GameEntry.Controller.Knapsack.OpenKnapsackForm();
                    break;
                case "SystemButton":

                    break;
            }
        }

        /// <summary>
        /// 主界面切换战车状态按钮
        /// </summary>
        [SerializeField] private Button m_VehicleOnOffButton;

        /// <summary>
        /// 战车上车图标
        /// </summary>
        [SerializeField] private Sprite m_VehicleGetOnSprite;

        /// <summary>
        /// 战车下车图标
        /// </summary>
        [SerializeField] private Sprite m_VehicleGetOffSprite;

        /// <summary>
        /// 上下车按钮点击
        /// </summary>
        private void OnVehicleOnOffButtonClick()
        {
            ChangeVehicleState(true);
        }

        /// <summary>
        /// 修改战车上下车图标状态 默认是读取上次的状态显示
        /// </summary>
        /// <param name="isSwitch">是否切换</param>
        private void ChangeVehicleState(bool isSwitch = false)
        {
            if (isSwitch)
            {
                GameEntry.Setting.SetBool(Constant.Setting.VehicleState, !GameEntry.Setting.GetBool(Constant.Setting.VehicleState, false));
            }

            var currentState = GameEntry.Setting.GetBool(Constant.Setting.VehicleState, false);
            Log.Debug("当前状态 " + currentState);
            if (currentState)
            {
                // 上车
                // 判断是否脚下有战车
                
                m_VehicleOnOffButton.GetComponent<Image>().sprite = m_VehicleGetOffSprite;
            }
            else
            {
                // 下车
                // 判断是否在战车上
                
                m_VehicleOnOffButton.GetComponent<Image>().sprite = m_VehicleGetOnSprite;
            }
        }

        #endregion
    }
}