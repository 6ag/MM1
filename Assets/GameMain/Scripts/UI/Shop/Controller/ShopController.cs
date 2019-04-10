using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 商店控制器
    /// </summary>
    public class ShopController : ControllerBase
    {
        /// <summary>
        /// 商店界面
        /// </summary>
        private ShopForm m_ShopForm;

        public ShopController()
        {
        }

        /// <summary>
        /// 打开商店界面
        /// </summary>
        /// <param name="storeId"></param>
        public void OpenShopForm(int storeId)
        {
            if (GameEntry.UI.HasUIForm(UIFormId.ShopForm))
            {
                return;
            }

            // 根据当前NPC的编号去查商店表，查到商品列表
            var storeTable = GameEntry.DataTable.GetDataTable<DRStore>();
            var storeRow = storeTable.GetDataRow(storeId);

            var shopModels = new List<ShopModel>();
            foreach (var itemId in storeRow.Items)
            {
                shopModels.Add(new ShopModel(itemId));
            }

            // 打开商店界面
            GameEntry.UI.OpenUIForm(UIFormId.ShopForm, shopModels);

            // 打开商店界面同时，也打开背包界面
            GameEntry.Controller.Knapsack.OpenKnapsackForm();
        }

        protected override void OpenUIFormSuccessHandler(object sender, GameEventArgs e)
        {
            var args = (OpenUIFormSuccessEventArgs) e;
            if (args.UIForm.Logic as ShopForm)
            {
                m_ShopForm = (ShopForm) args.UIForm.Logic;
            }
        }

        /// <summary>
        /// 购买道具
        /// </summary>
        /// <param name="shopModel">商品模型</param>
        /// <param name="count">数量</param>
        public void BuyItem(ShopModel shopModel, int count = 1)
        {
            if (!GameEntry.Archive.Data.ReduceGold(shopModel.BuyPrice * count))
            {
                Log.Debug("金币不够 弹出提示");
                return;
            }
            
            GameEntry.Controller.Knapsack.UpdateCoinAndUsedBoxCountUI();
            GameEntry.Controller.Knapsack.AddItem(shopModel.Id, count, GetItemSourceType.Shop);
        }

        public override void Dispose()
        {
            base.Dispose();
            m_ShopForm = null;
        }
    }
}