using System;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 特效实体数据
    /// </summary>
    [Serializable]
    public class EffectData : EntityData
    {
        [SerializeField]
        private float m_KeepTime = 0f;

        public EffectData(int id) : base(id)
        {
            m_KeepTime = 3f;
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public float KeepTime
        {
            get
            {
                return m_KeepTime;
            }
        }
    }
}
