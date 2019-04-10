using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 商品列表视图
    /// </summary>
    public class ShopListView : MonoBehaviour
    {
        /// <summary>
        /// 子项预设
        /// </summary>
        [SerializeField] private GameObject m_cell;

        /// <summary>
        /// 内容面板RectTransform组件
        /// </summary>
        [SerializeField] private RectTransform m_contentRectTransform;

        /// <summary>
        /// 子项高度
        /// </summary>
        private float m_cellHeight;

        /// <summary>
        /// 上下左右间距
        /// </summary>
        private float m_cellInterval = 20f;

        /// <summary>
        /// 模型集合
        /// </summary>
        private List<ShopModel> m_shopModels;

        /// <summary>
        /// 清理数据
        /// </summary>
        public void Clear()
        {
            // 销毁所有商品cell
            var allCells = m_contentRectTransform.GetComponentsInChildren<ShopCell>();
            foreach (var cell in allCells)
            {
                Destroy(cell.gameObject);
            }
        }

        /// <summary>
        /// 初始化变量
        /// </summary>
        public void SetupData(List<ShopModel> shopModels)
        {
            m_shopModels = shopModels;

            m_cellHeight = m_cell.GetComponent<RectTransform>().sizeDelta.y;

            // 设置内容面板锚点和中心点
            m_contentRectTransform.anchorMin = new Vector2(0.5f, 1f);
            m_contentRectTransform.anchorMax = new Vector2(0.5f, 1f);
            m_contentRectTransform.pivot = new Vector2(0.5f, 1f);

            // 设置内容面板尺寸
            var contentSize = m_contentRectTransform.sizeDelta;
            contentSize.y = (m_cellHeight + m_cellInterval) * m_shopModels.Count / 2 + m_cellInterval;
            m_contentRectTransform.sizeDelta = contentSize;

            // 设置内容面板坐标
            m_contentRectTransform.anchoredPosition = Vector2.zero;

            // 添加商品cell
            for (var i = 0; i < m_shopModels.Count; i++)
            {
                var cell = Instantiate(m_cell);
                cell.transform.SetParent(m_contentRectTransform);
                cell.transform.localScale = Vector3.one;

                var cellRectTrans = cell.GetComponent<RectTransform>();
                cellRectTrans.anchorMin = new Vector2(0f, 1f);
                cellRectTrans.anchorMax = new Vector2(0f, 1f);
                cellRectTrans.pivot = new Vector2(0.5f, 0.5f);

                var row = i / 2;
                var col = i % 2;

                var posX = col == 0 ? 155f : 405f;
                var posY = row * (m_cellHeight + m_cellInterval) + (m_cellHeight / 2 + m_cellInterval);
                if (posY > 0)
                {
                    posY = -posY;
                }

                cellRectTrans.anchoredPosition3D = new Vector3(posX, posY, 0);

                // 设置商品数据
                var shopCell = cell.GetComponent<ShopCell>();
                shopCell.SetUp(m_shopModels[i]);

                // 在父物体的最后一个顺序，这样保证显示在最前面
                cell.transform.SetAsLastSibling();
            }
        }
    }
}