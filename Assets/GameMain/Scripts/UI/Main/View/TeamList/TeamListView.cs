using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 主界面左上角队伍视图
    /// </summary>
    public class TeamListView : ViewBase
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
        /// 玩家队伍列表视图
        /// </summary>
        [SerializeField] private TeamPlayerListView m_PlayerListView;

        /// <summary>
        /// 战车队伍列表视图
        /// </summary>
        [SerializeField] private TeamVehicleListView m_VehicleListView;

        /// <summary>
        /// 切换队伍视图收缩按钮
        /// </summary>
        [SerializeField] private Button m_TelescopicButton;

        /// <summary>
        /// 当前队伍列表状态
        /// </summary>
        private bool m_CurrentTeamListState = false;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnButtonClick(GameObject sender)
        {
            base.OnButtonClick(sender);
            switch (sender.name)
            {
                case "TelescopicButton":
                    OnTelescopicButtonClick();
                    break;
                case "SwitchPlayerButton":
                    SwitchTeamListView(true);
                    break;
                case "SwitchVehicleButton":
                    SwitchTeamListView(false);
                    break;
            }
        }

        /// <summary>
        /// 切换队伍列表
        /// </summary>
        /// <param name="isPlayerList">是否是玩家列表</param>
        private void SwitchTeamListView(bool isPlayerList)
        {
            if (isPlayerList)
            {
                m_PlayerListView.gameObject.SetActive(true);
                m_SwitchPlayerLine.gameObject.SetActive(true);
                m_VehicleListView.gameObject.SetActive(false);
                m_SwitchVehicleLine.gameObject.SetActive(false);
            }
            else
            {
                m_PlayerListView.gameObject.SetActive(false);
                m_SwitchPlayerLine.gameObject.SetActive(false);
                m_VehicleListView.gameObject.SetActive(true);
                m_SwitchVehicleLine.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 点击了收缩团队视图按钮
        /// </summary>
        private void OnTelescopicButtonClick()
        {
            m_CurrentTeamListState = !m_CurrentTeamListState;
            if (m_CurrentTeamListState)
            {
                GetComponent<RectTransform>().DOLocalMoveX(0, 0.25f);
                m_TelescopicButton.GetComponentInChildren<Text>().text = "关闭";
            }
            else
            {
                GetComponent<RectTransform>().DOLocalMoveX(-320, 0.25f);
                m_TelescopicButton.GetComponentInChildren<Text>().text = "队伍";
            }
        }
    }
}