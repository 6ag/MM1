using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 玛格丽特实体类
    /// </summary>
    public class Margaret : Entity
    {
        /// <summary>
        /// 是否在繁忙状态
        /// </summary>
        public bool IsBusy = false;

        /// <summary>
        /// 角色实体数据
        /// </summary>
        public PlayerData PlayerStats;

        /// <summary>
        /// 角色移动控制脚本
        /// </summary>
        private MargaretMovement m_Movement;

        /// <summary>
        /// 获取玩家实体类
        /// </summary>
        /// <returns></returns>
        public static Margaret GetPlayer()
        {
            return ((Margaret) GameEntry.Entity.GetEntity(3).Logic);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            PlayerStats = (PlayerData) userData;
        }
        
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_Movement = GetComponent<MargaretMovement>();
            
        }
        
    }
}