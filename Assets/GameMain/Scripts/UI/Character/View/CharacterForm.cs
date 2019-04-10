using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 装备界面
    /// </summary>
    public class CharacterForm : UGuiForm
    {
        /// <summary>
        /// 切换玩家按钮下划线图片
        /// </summary>
        [SerializeField] private Image m_SwitchPlayerLine;

        /// <summary>
        /// 切换战车按钮下划线图片
        /// </summary>
        [SerializeField] private Image m_SwitchVehicleLine;

        /// <summary>
        /// 玩家装备视图
        /// </summary>
        [SerializeField] private HumanEquipmentView m_HumanEquipmentView;

        /// <summary>
        /// 战车装备视图
        /// </summary>
        [SerializeField] private VehicleEquipmentView m_VehicleEquipmentView;

        protected override void OnButtonClick(GameObject go)
        {
            base.OnButtonClick(go);
            switch (go.name)
            {
                case "CloseButton":
                    GameEntry.UI.CloseUIForm(this);
                    GameEntry.Controller.Knapsack.CloseKnapsackForm();
                    break;
                case "SwitchPlayerButton":
                    SwitchEquipmentView(true);
                    break;
                case "SwitchVehicleButton":
                    SwitchEquipmentView(false);
                    break;
            }
        }

        /// <summary>
        /// 切换装备界面
        /// </summary>
        /// <param name="isPlayerView">是否是玩家装备界面</param>
        private void SwitchEquipmentView(bool isPlayerView)
        {
            if (isPlayerView)
            {
                m_HumanEquipmentView.gameObject.SetActive(true);
                m_SwitchPlayerLine.gameObject.SetActive(true);
                m_VehicleEquipmentView.gameObject.SetActive(false);
                m_SwitchVehicleLine.gameObject.SetActive(false);
            }
            else
            {
                m_HumanEquipmentView.gameObject.SetActive(false);
                m_SwitchPlayerLine.gameObject.SetActive(false);
                m_VehicleEquipmentView.gameObject.SetActive(true);
                m_SwitchVehicleLine.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 脱下指定角色的装备
        /// </summary>
        /// <param name="model"></param>
        public void TakeOffHumanEquipment(HumanEquipModel model)
        {
            m_HumanEquipmentView.TakeOffHumanEquipment(model);
        }

        /// <summary>
        /// 佩戴指定角色的装备
        /// </summary>
        /// <param name="model"></param>
        public void WornHumanEquipment(HumanEquipModel model)
        {
            m_HumanEquipmentView.WornHumanEquipment(model);
        }

        /// <summary>
        /// 获取当前的玩家
        /// </summary>
        /// <returns></returns>
        public PlayerType GetCurrentPlayerType()
        {
            return m_HumanEquipmentView.CurrentPlayerType;
        }

    }
}