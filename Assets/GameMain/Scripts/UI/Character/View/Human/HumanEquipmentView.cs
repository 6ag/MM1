using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 玩家装备界面
    /// </summary>
    public class HumanEquipmentView : ViewBase
    {
        /// <summary>
        /// 装备格子集合
        /// </summary>
        [SerializeField] private List<HumanEquipmentItemSolt> m_EquipSolts;

        /// <summary>
        /// 当前装备界面显示的角色信息是哪个的
        /// </summary>
        public PlayerType CurrentPlayerType { get; private set; }

        /// <summary>
        /// 头像列表
        /// </summary>
        [SerializeField] private HumanAvatarListView m_AvatarListView;

        /// <summary>
        /// 属性界面
        /// </summary>
        [SerializeField] private HumanAttributeView m_AttributeView;

        /// <summary>
        /// 道具预设
        /// </summary>
        [SerializeField] private GameObject m_ItemPrefab;

        protected override void Start()
        {
            base.Start();

            if (m_AvatarListView != null)
            {
                m_AvatarListView.OnAvatarClick = OnAvatarClick;
            }

            // 默认是主角
            CurrentPlayerType = PlayerType.Rebana;

            SetupItems();
            SetupRoleAttribute();
        }

        /// <summary>
        /// 点击了角色头像
        /// </summary>
        /// <param name="playerType"></param>
        private void OnAvatarClick(PlayerType playerType)
        {
            if (playerType == CurrentPlayerType) return;
            CurrentPlayerType = playerType;
            
            // 清理装备格子
            ClearItems();
            // 设置装备格子
            SetupItems();
            // 设置角色属性
            SetupRoleAttribute();
        }

        /// <summary>
        /// 设置角色属性
        /// </summary>
        private void SetupRoleAttribute()
        {
            var playerData = GameEntry.Archive.Data.GetPlayerData(CurrentPlayerType);
            m_AttributeView.SetupRoleAttribute(playerData);
        }

        /// <summary>
        /// 设置所有格子里的装备
        /// </summary>
        private void SetupItems()
        {
            var playerData = GameEntry.Archive.Data.GetPlayerData(CurrentPlayerType);
            if (playerData.HeadModel != null)
            {
                InstantiateItem(playerData.HeadModel);
            }

            if (playerData.BodyModel != null)
            {
                InstantiateItem(playerData.BodyModel);
            }

            if (playerData.HandModel != null)
            {
                InstantiateItem(playerData.HandModel);
            }

            if (playerData.FootModel != null)
            {
                InstantiateItem(playerData.FootModel);
            }

            if (playerData.WeaponModel != null)
            {
                InstantiateItem(playerData.WeaponModel);
            }
        }

        /// <summary>
        /// 清理所有格子里的装备
        /// </summary>
        private void ClearItems()
        {
            foreach (var equipSolt in m_EquipSolts)
            {
                equipSolt.ClearItemBox();
            }
        }

        /// <summary>
        /// 实例化一个装备UI
        /// </summary>
        /// <param name="model"></param>
        private void InstantiateItem(HumanEquipModel model)
        {
            foreach (var equipSolt in m_EquipSolts)
            {
                if (equipSolt.PartType == model.PartType)
                {
                    // 原先已经有装备UI，则直接替换数据。没有才实例化
                    var itemUI = equipSolt.GetComponentInChildren<DrawableItemUI>();
                    if (itemUI == null)
                    {
                        Log.Debug("实例化 "+ model.Name);
                        var itemGo = Instantiate(m_ItemPrefab);
                        itemGo.transform.SetParent(equipSolt.transform, false);
                        itemGo.transform.SetAsFirstSibling();
                    }
                    Log.Debug("来了");
                    equipSolt.SetupData(model);
                    break;
                }
            }
        }

        /// <summary>
        /// 脱下指定角色的装备
        /// </summary>
        /// <param name="model"></param>
        public void TakeOffHumanEquipment(HumanEquipModel model)
        {
            foreach (var equipSolt in m_EquipSolts)
            {
                if (equipSolt.PartType == model.PartType)
                {
                    equipSolt.ClearItemBox();
                    break;
                }
            }

            SetupRoleAttribute();
        }

        /// <summary>
        /// 佩戴指定角色的装备
        /// </summary>
        /// <param name="model"></param>
        public void WornHumanEquipment(HumanEquipModel model)
        {
            InstantiateItem(model);
            SetupRoleAttribute();
        }
    }
}