using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 人类装备模型
    /// </summary>
    [Serializable]
    public class HumanEquipModel : ItemModel
    {
        /// <summary>
        /// 部位
        /// </summary>
        public HumanPartType PartType;

        /// <summary>
        /// 可装备的玩家类型集合（相当于道具可装备的职业类型）
        /// </summary>
        public List<PlayerType> UsablePlayerTypes;

        /// <summary>
        /// 攻击力
        /// </summary>
        public int Attack;

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defence;

        /// <summary>
        /// 作用范围 1单体 2一组 3全体
        /// </summary>
        public InfluenceRangeType RangeType;

        /// <summary>
        /// 当前装备所属者（自己加的属性，不是表里的哦）
        /// </summary>
        public PlayerType OwerPlayerType;

        public HumanEquipModel(int id) : base(id)
        {
            var row = GetHumanEquipRow();
            PartType = row.PartType;
            UsablePlayerTypes = row.UsablePlayerTypes;
            Attack = row.Attack;
            Defence = row.Defence;
            RangeType = row.RangeType;
        }
    }
}