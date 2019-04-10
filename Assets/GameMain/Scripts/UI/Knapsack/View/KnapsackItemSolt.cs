using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 背包格子
    /// </summary>
    public class KnapsackItemSolt : ViewBase
    {
        /// <summary>
        /// 物品数量
        /// </summary>
        [SerializeField] private Text m_ItemCountText;

        /// <summary>
        /// 道具编号
        /// </summary>
        private KnapsackModel m_KnapsackModel;
        
        /// <summary>
        /// 设置背包格子里的道具数据
        /// </summary>
        /// <param name="model">背包道具模型</param>
        public void SetupData(KnapsackModel model)
        {
            m_KnapsackModel = model;
            
            // 设置道具数据
            var item = GetComponentInChildren<DrawableItemUI>();
            if (item == null) return;
            item.SetupData(model.Id);

            UpdateCurrentStackCount();
        }

        /// <summary>
        /// 更新当前格子道具堆叠数量 如果为0就销毁
        /// </summary>
        public void UpdateCurrentStackCount()
        {
            m_ItemCountText.text = m_KnapsackModel.CurrentStackCount > 1 ? m_KnapsackModel.CurrentStackCount.ToString() : string.Empty;
            if (m_KnapsackModel.CurrentStackCount <= 0)
            {
                ClearItemBox();
            }
        }

        /// <summary>
        /// 清空格子里的道具
        /// </summary>
        public void ClearItemBox()
        {
            var item = GetComponentInChildren<DrawableItemUI>();
            if (item == null) return;

            // 如果格子上有道具则销毁
            m_KnapsackModel = null;
            m_ItemCountText.text = string.Empty;
            Destroy(item.gameObject);
        }

        protected override void OnButtonClick(GameObject sender)
        {
            base.OnButtonClick(sender);
            if (m_KnapsackModel == null)
            {
                Log.Debug("当前格子为空");
                return;
            }
            
            GameEntry.DataNode.SetData<VarInt>(Constant.NodeKey.ItemInfoType, 3);
            GameEntry.UI.OpenUIForm(UIFormId.ItemInfoForm, m_KnapsackModel);
        }
    }
}