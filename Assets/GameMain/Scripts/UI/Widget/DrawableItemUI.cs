using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 可拖拽的道具UI
    /// </summary>
    public class DrawableItemUI : ViewBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        /// <summary>
        /// 物品图标
        /// </summary>
        [SerializeField] private Image m_ItemIcon;

        /// <summary>
        /// 道具编号
        /// </summary>
        private int m_ItemId;

        /// <summary>
        /// 设置道具数据
        /// </summary>
        /// <param name="itemId">道具编号</param>
        public void SetupData(int itemId)
        {
            m_ItemId = itemId;

            // 图标
            GameEntry.Resource.LoadAsset(AssetUtility.GetItemSprite(ItemModel.GetItemRow(m_ItemId).Icon), typeof(Sprite),
                new LoadAssetCallbacks(LoadAssetSuccessCallback));
        }

        /// <summary>
        /// 加载资源回调
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

        #region 拖拽道具

        public void OnBeginDrag(PointerEventData eventData)
        {
            Log.Debug("开始拖拽");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Log.Debug("拖拽中");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Log.Debug("结束拖拽");
        }

        #endregion
    }
}