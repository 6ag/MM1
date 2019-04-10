using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MetalMax
{
	/// <summary>
    /// 虚拟摇杆
    /// </summary>
	public class UIjoystick : MonoBehaviour, IDragHandler, IEndDragHandler
	{
		/// <summary>
		/// 最大半径
		/// </summary>
		public float MaxRadius = 100;

		/// <summary>
		/// 摇杆背景图标位置
		/// </summary>
		private Vector2 m_BgPos;

		private float m_Horizontal = 0f;
		private float m_Vertical = 0f;

        /// <summary>
        /// 水平方向 范围 -1 到 1
        /// </summary>
        public float Horizontal
        {
            get
            {
                return m_Horizontal;
            }
        }

        /// <summary>
        /// 垂直方向 范围 -1 到 1
        /// </summary>
        public float Vertical
        {
            get
            {
                return m_Vertical;
            }
        }

        private void Start()
		{
			m_BgPos = transform.parent.transform.position;
		}

		private void Update()
		{
			if (Mathf.Abs(transform.localPosition.x) > 5 || Mathf.Abs(transform.localPosition.y) > 5)
			{
				m_Horizontal = transform.localPosition.x / MaxRadius;
				m_Vertical = transform.localPosition.y / MaxRadius;
			}
			else
			{
				m_Horizontal = 0;
				m_Vertical = 0;
			}
		}

		/// <summary>
		/// 拖拽中 （要去补下向量计算的知识点）
		/// </summary>
		/// <param name="eventData"></param>
		public void OnDrag(PointerEventData eventData)
		{
			// 获取鼠标位置与初始位置之间的向量
			Vector2 oppsitionVec = eventData.position - m_BgPos;

			// 获取向量的长度
			float distance = Vector3.Magnitude(oppsitionVec);

			// 限制向量长度 最小0 最大是 最大半径
			float radius = Mathf.Clamp(distance, 0, MaxRadius);
			
			// 限制半径长度
			transform.position = m_BgPos + oppsitionVec.normalized * radius;
		}

		/// <summary>
		/// 结束拖拽
		/// </summary>
		/// <param name="eventData"></param>
		public void OnEndDrag(PointerEventData eventData)
		{
			transform.position = m_BgPos;
			transform.localPosition = Vector3.zero;
		}
	}
}

