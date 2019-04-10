using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 道具信息详情界面
    /// </summary>
    public class ItemInfoForm : UGuiForm
    {
        /// <summary>
        /// 道具信息类型 装备界面、背包界面 就是说是从哪个界面点击的道具，显示出道具信息详情界面的
        /// </summary>
        public enum ItemInfoType
        {
            /// <summary>
            /// 人类装备
            /// </summary>
            HumanEquipment = 1,

            /// <summary>
            /// 战车装备
            /// </summary>
            VehiclenEquipment = 2,

            /// <summary>
            /// 背包
            /// </summary>
            Knapsack = 3
        }

        /// <summary>
        /// 道具名称
        /// </summary>
        [SerializeField] private Text m_ItemNameText;

        /// <summary>
        /// 道具类型
        /// </summary>
        [SerializeField] private Text m_ItemTypeText;

        /// <summary>
        /// 道具图标
        /// </summary>
        [SerializeField] private Image m_ItemIcon;

        /// <summary>
        /// 道具描述
        /// </summary>
        [SerializeField] private Text m_ItemDesText;

        /// <summary>
        /// 按钮1
        /// </summary>
        [SerializeField] private Button m_FuncButton1;

        /// <summary>
        /// 按钮2
        /// </summary>
        [SerializeField] private Button m_FuncButton2;

        /// <summary>
        /// 道具信息类型
        /// </summary>
        private ItemInfoType m_ItemInfoType;

        /// <summary>
        /// 背包道具模型
        /// </summary>
        private KnapsackModel m_KnapsackModel;

        /// <summary>
        /// 人类装备模型
        /// </summary>
        private HumanEquipModel m_HumanEquipModel;

        /// <summary>
        /// 战车装备模型
        /// </summary>
        private VehicleEquipModel m_VehicleEquipModel;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ItemInfoType = (ItemInfoType)(int)GameEntry.DataNode.GetData<VarInt>(Constant.NodeKey.ItemInfoType);
            GameEntry.DataNode.RemoveNode(Constant.NodeKey.ItemInfoType);
            SetupItemData((ItemModel) userData);
        }

        protected override void OnClose(object userData)
        {
            base.OnClose(userData);
            m_KnapsackModel = null;
            m_HumanEquipModel = null;
            m_VehicleEquipModel = null;
            m_ItemIcon.enabled = false;
        }

        #region 设置道具信息

        /// <summary>
        /// 设置道具详情信息
        /// </summary>
        /// <param name="model"></param>
        private void SetupItemData(ItemModel model)
        {
            m_ItemNameText.text = model.Name;
            m_ItemDesText.text = model.GetItemDes();
            m_ItemTypeText.text = model.GetItemCategory();

            // 图标
            GameEntry.Resource.LoadAsset(AssetUtility.GetItemSprite(model.Icon), typeof(Sprite),
                new LoadAssetCallbacks(LoadAssetSuccessCallback));

            switch (m_ItemInfoType)
            {
                case ItemInfoType.HumanEquipment:
                    m_HumanEquipModel = (HumanEquipModel) model;
                    SetupHumanEquipmentItemData(m_HumanEquipModel);
                    break;
                case ItemInfoType.VehiclenEquipment:
                    m_VehicleEquipModel = (VehicleEquipModel) model;
                    SetupVehicleEquipmentItemData(m_VehicleEquipModel);
                    break;
                case ItemInfoType.Knapsack:
                    m_KnapsackModel = (KnapsackModel) model;
                    SetupKnapsackItemData(m_KnapsackModel);
                    break;
            }
        }
       
        /// <summary>
        /// 设置人类装备界面道具信息
        /// </summary>
        /// <param name="model"></param>
        private void SetupHumanEquipmentItemData(HumanEquipModel model)
        {
            m_FuncButton1.gameObject.SetActive(true);
            m_FuncButton1.GetComponentInChildren<Text>().text = "卸下";
            m_FuncButton2.gameObject.SetActive(false);
        }

        /// <summary>
        /// 设置战车装备界面道具信息
        /// </summary>
        /// <param name="model"></param>
        private void SetupVehicleEquipmentItemData(VehicleEquipModel model)
        {
            m_FuncButton1.gameObject.SetActive(true);
            m_FuncButton1.GetComponentInChildren<Text>().text = "卸下";
            m_FuncButton2.gameObject.SetActive(false);
        }

        /// <summary>
        /// 设置背包界面道具信息
        /// </summary>
        /// <param name="model"></param>
        private void SetupKnapsackItemData(KnapsackModel model)
        {
            // 是否打开了商店
            if (GameEntry.UI.HasUIForm(UIFormId.ShopForm))
            {
                if (model.CanSell())
                {
                    m_FuncButton1.gameObject.SetActive(false);
                    m_FuncButton2.gameObject.SetActive(true);
                    m_FuncButton2.GetComponentInChildren<Text>().text = "出售";
                }
                else
                {
                    m_FuncButton1.gameObject.SetActive(false);
                    m_FuncButton2.gameObject.SetActive(false);
                }
            }
            else
            {
                if (model.CanUse())
                {
                    m_FuncButton1.gameObject.SetActive(true);
                    m_FuncButton1.GetComponentInChildren<Text>().text = "使用";
                }
                else if (model.CanEquip())
                {
                    m_FuncButton1.gameObject.SetActive(true);
                    m_FuncButton1.GetComponentInChildren<Text>().text = "装备";
                }

                if (model.CanDiscard())
                {
                    m_FuncButton2.gameObject.SetActive(true);
                    m_FuncButton2.GetComponentInChildren<Text>().text = "丢弃";
                }
                else
                {
                    m_FuncButton2.gameObject.SetActive(false);
                }
            }
        }

        #endregion

        /// <summary>
        /// 异步加载资源成功回调
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="asset"></param>
        /// <param name="duration"></param>
        /// <param name="userData"></param>
        private void LoadAssetSuccessCallback(string assetName, object asset, float duration, object userData)
        {
            m_ItemIcon.enabled = true;
            m_ItemIcon.sprite = (Sprite) asset;
        }

        #region 点击事件

        protected override void OnButtonClick(GameObject sender)
        {
            base.OnButtonClick(sender);
            switch (sender.name)
            {
                case "FuncButton1":
                    switch (m_ItemInfoType)
                    {
                        case ItemInfoType.HumanEquipment:
                        case ItemInfoType.VehiclenEquipment:
                            OnFuncButton1ClickOfEquipment();
                            break;
                        case ItemInfoType.Knapsack:
                            OnFuncButton1ClickOfKnapsack();
                            break;
                    }
                    break;
                case "FuncButton2":
                    if (m_ItemInfoType == ItemInfoType.Knapsack)
                    {
                        OnFuncButton2ClickOfKnapsack();
                    }
                    break;
                case "CloseButton":
                    OnCloseButtonClick();
                    break;
            }
        }

        #region 装备界面类型按钮处理

        /// <summary>
        /// 按钮1 卸下装备
        /// </summary>
        private void OnFuncButton1ClickOfEquipment()
        {
            // 卸下装备
            switch (m_ItemInfoType)
            {
                case ItemInfoType.HumanEquipment:
                    GameEntry.Controller.CharacterInfo.TakeOffHumanEquipment(m_HumanEquipModel);
                    break;
                case ItemInfoType.VehiclenEquipment:
                    GameEntry.Controller.CharacterInfo.TakeOffVehicleEquipment(m_VehicleEquipModel);
                    break;
            }
            OnCloseButtonClick();
        }

        #endregion

        #region 背包界面类型按钮处理

        /// <summary>
        /// 按钮1 使用、装备
        /// </summary>
        private void OnFuncButton1ClickOfKnapsack()
        {
            if (m_KnapsackModel.CanUse())
            {
                // 使用
                GameEntry.Controller.Knapsack.UseItem(m_KnapsackModel);
            }
            else if (m_KnapsackModel.CanEquip())
            {
                // 装备
                GameEntry.Controller.Knapsack.EquipItem(m_KnapsackModel);
            }

            OnCloseButtonClick();
        }

        /// <summary>
        /// 按钮2 出售、丢弃
        /// </summary>
        private void OnFuncButton2ClickOfKnapsack()
        {
            if (GameEntry.UI.HasUIForm(UIFormId.ShopForm))
            {
                if (m_KnapsackModel.CanSell())
                {
                    if (m_KnapsackModel.CurrentStackCount > 1)
                    {
                        GameEntry.DataNode.SetData<VarInt>(Constant.NodeKey.InputCountType, 1);
                        GameEntry.UI.OpenUIForm(UIFormId.InputCountForm, m_KnapsackModel);
                    }
                    else
                    {
                        // 出售一个
                        GameEntry.Controller.Knapsack.SellItem(m_KnapsackModel);
                    }
                }
            }
            else
            {
                if (m_KnapsackModel.CanDiscard())
                {
                    if (m_KnapsackModel.CurrentStackCount > 1)
                    {
                        GameEntry.DataNode.SetData<VarInt>(Constant.NodeKey.InputCountType, 3);
                        GameEntry.UI.OpenUIForm(UIFormId.InputCountForm, m_KnapsackModel);
                    }
                    else
                    {
                        // 丢弃一个
                        GameEntry.Controller.Knapsack.DiscardItem(m_KnapsackModel);
                    }
                }
            }

            OnCloseButtonClick();
        }

        #endregion

        /// <summary>
        /// 关闭界面
        /// </summary>
        private void OnCloseButtonClick()
        {
            GameEntry.UI.CloseUIForm(this);
        }

        #endregion
        
    }
}