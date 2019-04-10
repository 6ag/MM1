using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 物品箱子
    /// </summary>
	public class ItemBox : MonoBehaviour
	{
        /// <summary>
        /// 物品ID
        /// </summary>
	    public int ItemId;

	    private bool m_IsOpen = false;

	    /// <summary>
	    /// 箱子打开状态
	    /// </summary>
	    public bool IsOpen
	    {
	        set
	        {
	            if (value)
	            {
	                m_SpriteREnderer.sprite = m_OpenState;
	            }
	            else
	            {
	                m_SpriteREnderer.sprite = m_CloseState;
	            }
                m_IsOpen = value;
	        }
	        get { return m_IsOpen; }
	    }

        /// <summary>
        /// 打开状态的精灵图片
        /// </summary>
        [SerializeField]
	    private Sprite m_OpenState;

        /// <summary>
        /// 关闭状态的精灵图片
        /// </summary>
        [SerializeField]
	    private Sprite m_CloseState;

        /// <summary>
        /// 精灵图片渲染器
        /// </summary>
        [SerializeField]
	    private SpriteRenderer m_SpriteREnderer;

        private void Start()
        {
            if (IsOpen)
            {
                m_SpriteREnderer.sprite = m_OpenState;
            }
            else
            {
                m_SpriteREnderer.sprite = m_CloseState;
            }
        }
        
        
    }
}
