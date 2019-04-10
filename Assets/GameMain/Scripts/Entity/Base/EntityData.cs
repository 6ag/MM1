using System;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 实体数据基类
    /// </summary>
    [Serializable]
    public abstract class EntityData
    {
        [SerializeField]
        private int m_Id = 0;

        public EntityData(int id)
        {
            m_Id = id;
        }

        /// <summary>
        /// 实体编号-读数据表获取到的
        /// </summary>
        public int Id
        {
            get
            {
                return m_Id;
            }
        }

    }
}
