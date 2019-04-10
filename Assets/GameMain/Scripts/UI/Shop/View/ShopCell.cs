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
    /// 单个商品视图
    /// </summary>
    public class ShopCell : ViewBase
    {
        /// <summary>
        /// 商品图标
        /// </summary>
        [SerializeField] private Image m_IconImage;

        /// <summary>
        /// 商品名称
        /// </summary>
        [SerializeField] private Text m_NameText;

        /// <summary>
        /// 商品介绍
        /// </summary>
        [SerializeField] private Text m_IntroText;

        /// <summary>
        /// 购买价格
        /// </summary>
        [SerializeField] private Text m_BuyPrice;

        /// <summary>
        /// 商品模型
        /// </summary>
        private ShopModel m_ShopModel;

        public void SetUp(ShopModel shopModel)
        {
            m_ShopModel = shopModel;
            GameEntry.Resource.LoadAsset(AssetUtility.GetItemSprite(shopModel.Icon), typeof(Sprite), new LoadAssetCallbacks(LoadAssetSuccessCallback));
            m_NameText.text = shopModel.Name;
            m_BuyPrice.text = shopModel.BuyPrice.ToString();
            m_IntroText.text = shopModel.GetItemDes();
        }

        private void LoadAssetSuccessCallback(string assetName, object asset, float duration, object userData)
        {
            m_IconImage.sprite = (Sprite) asset;
        }

        protected override void OnButtonClick(GameObject go)
        {
            base.OnButtonClick(go);
            switch (go.name)
            {
                case "BuyButton":

                    if (m_ShopModel.StackCount > 1)
                    {
                        GameEntry.DataNode.SetData<VarInt>(Constant.NodeKey.InputCountType, 2);
                        GameEntry.UI.OpenUIForm(UIFormId.InputCountForm, m_ShopModel);
                    }
                    else
                    {
                        GameEntry.Controller.Shop.BuyItem(m_ShopModel);
                    }

                    break;
            }
        }
    }
}