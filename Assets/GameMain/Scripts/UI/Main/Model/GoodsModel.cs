using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 物品模型
    /// </summary>
    [Serializable]
    public class GoodsModel : ItemModel
    {
        /// <summary>
        /// 物品类型 1补充 2攻击 3辅助 4工具 5杂物
        /// </summary>
        public GoodsType GoodsType;

        /// <summary>
        /// 使用类型 1人类 2战车
        /// </summary>
        public int UseType;

        /// <summary>
        /// 是否能在战斗中使用
        /// </summary>
        public bool CanCombat;

        /// <summary>
        /// 消耗类型 1一次性 2无限次
        /// </summary>
        public GoodsConsumeType ConsumeType;

        /// <summary>
        /// 恢复人类HP
        /// </summary>
        public int HP;

        /// <summary>
        /// 恢复战车装甲片
        /// </summary>
        public int PAG;

        /// <summary>
        /// 攻击力
        /// </summary>
        public int Attack;

        /// <summary>
        /// 作用范围 1单体 2一组 3全体
        /// </summary>
        public InfluenceRangeType RangeType;

        public GoodsModel(int id) : base(id)
        {
            var row = GetGoodsRow();
            GoodsType = row.GoodsType;
            UseType = row.UseType;
            CanCombat = row.CanCombat;
            ConsumeType = row.ConsumeType;
            HP = row.HP;
            PAG = row.PAG;
            Attack = row.Attack;
        }
    }
}