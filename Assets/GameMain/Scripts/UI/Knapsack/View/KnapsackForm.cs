using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using Log = GameFramework.Log;

namespace MetalMax
{
    /// <summary>
    /// 背包界面
    /// </summary>
    public class KnapsackForm : UGuiForm
    {
        /// <summary>
        /// 背包所有格子集合
        /// </summary>
        [SerializeField] private List<KnapsackItemSolt> m_ItemSolts;

        /// <summary>
        /// 背包物品数量
        /// </summary>
        [SerializeField] private Text m_KnapsackCountText;

        /// <summary>
        /// 金币
        /// </summary>
        [SerializeField] private Text m_CoinText;

        /// <summary>
        /// 道具预设
        /// </summary>
        [SerializeField] private GameObject m_ItemPrefab;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            LoadKnapsackItemUI();
            UpdateCoinAndUsedBoxCountUI();
        }

        protected override void OnClose(object userData)
        {
            base.OnClose(userData);
            ClearAllBox();
        }
        
        /// <summary>
        /// 加载所有背包道具UI
        /// </summary>
        public void LoadKnapsackItemUI()
        {
            for (var i = 0; i < GameEntry.Archive.Data.KnapsackModels.Count; i++)
            {
                var model = GameEntry.Archive.Data.KnapsackModels[i];
                InstantiateItem(model);
            }
        }

        /// <summary>
        /// 清理背包中显示的道具UI
        /// </summary>
        public void ClearAllBox()
        {
            for (var i = 0; i < m_ItemSolts.Count; i++)
            {
                var itemBox = m_ItemSolts[i];
                itemBox.ClearItemBox();
            }
        }

        /// <summary>
        /// 更新金币和使用的背包格子数量UI
        /// </summary>
        public void UpdateCoinAndUsedBoxCountUI()
        {
            m_CoinText.text = GameEntry.Archive.Data.Gold.ToString();
            m_KnapsackCountText.text = string.Format("{0}/{1}", GameEntry.Archive.Data.KnapsackModels.Count, m_ItemSolts.Count);
        }

        /// <summary>
        /// 添加物品进背包
        /// </summary>
        /// <param name="model">道具模型</param>
        /// <param name="isNewItem">是否需要新格子</param>
        /// <returns></returns>
        public void AddItem(KnapsackModel model, bool isNewItem)
        {
            // 更新背包格子数量和金币显示
            UpdateCoinAndUsedBoxCountUI();

            if (isNewItem)
            {
                // 需要新格子则实例化
                InstantiateItem(model);
            }
            else
            {
                UpdateCurrentStackCount(model.Index);
            }
        }

        /// <summary>
        /// 实例化一个道具UI
        /// </summary>
        /// <param name="model"></param>
        private void InstantiateItem(KnapsackModel model)
        {
            var itemGo = Instantiate(m_ItemPrefab);
            itemGo.transform.SetParent(m_ItemSolts[model.Index].transform, false);
            itemGo.transform.SetAsFirstSibling();
            m_ItemSolts[model.Index].SetupData(model);
        }

        /// <summary>
        /// 更新指定格子道具堆叠数量 如果为0就销毁
        /// </summary>
        /// <param name="index">背包索引</param>
        public void UpdateCurrentStackCount(int index)
        {
            m_ItemSolts[index].UpdateCurrentStackCount();
        }

        protected override void OnButtonClick(GameObject go)
        {
            base.OnButtonClick(go);
            switch (go.name)
            {
                case "CloseButton":
                    GameEntry.UI.CloseUIForm(this);
                    break;
            }
        }
    }
}