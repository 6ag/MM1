using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 战车装备
    /// </summary>
    [Serializable]
    public class VehicleEquipModel : ItemModel
    {
        /// <summary>
        /// 装备部位 1主炮 2副炮 3S-E 4C装置 5引擎 6底盘
        /// </summary>
        public VehiclePartType PartType;

        /// <summary>
        /// 攻击力
        /// </summary>
        public int Attack;

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defence;

        /// <summary>
        /// 攻击范围 1单体 2一组 3全体
        /// </summary>
        public InfluenceRangeType RangeType;

        /// <summary>
        /// 主炮弹仓数量
        /// </summary>
        public int Magazine;

        /// <summary>
        /// 自身重量
        /// </summary>
        public float Weight;

        /// <summary>
        /// 增加负载重量
        /// </summary>
        public float Load;

        #region 底盘改造

        /// <summary>
        /// 改造底部最大防御力
        /// </summary>
        public int MaxDefence;

        /// <summary>
        /// 改造底部最大主炮弹仓数量
        /// </summary>
        public int MaxMagazine;

        /// <summary>
        /// 改造底部最大自身重量
        /// </summary>
        public float MaxWeight;

        /// <summary>
        /// 底盘初始主炮是否有洞
        /// </summary>
        public bool MainHole;

        /// <summary>
        /// 底盘初始副炮是否有洞
        /// </summary>
        public bool SubHole;

        /// <summary>
        /// 底盘初始S-E是否有洞
        /// </summary>
        public bool SEHole;

        /// <summary>
        /// 可装备到主炮位置
        /// </summary>
        public bool MainGunLocation;

        /// <summary>
        /// 可装备到副炮位置
        /// </summary>
        public bool SubGunLocation;

        /// <summary>
        /// 可装备到S-E位置
        /// </summary>
        public bool SEGunLocation;

        #endregion

        #region 引擎改造

        /// <summary>
        /// 是否可改造 引擎才可改造
        /// </summary>
        public bool CanTrans;

        /// <summary>
        /// 改造价格
        /// </summary>
        public int TransPrice;

        /// <summary>
        /// 改造后的道具编号
        /// </summary>
        public int TransId;

        #endregion

        public VehicleEquipModel(int id) : base(id)
        {
            var row = GetVehicleEquipRow();
            PartType = row.PartType;
            Attack = row.Attack;
            Defence = row.Defence;
            RangeType = row.RangeType;
            Magazine = row.Magazine;
            Weight = row.Weight;
            Load = row.Load;
            MaxDefence = row.MaxDefence;
            MaxMagazine = row.MaxMagazine;
            MaxWeight = row.MaxWeight;

            MainHole = row.MainHole;
            SubHole = row.SubHole;
            SEHole = row.SEHole;

            MainGunLocation = row.MainGunLocation;
            SubGunLocation = row.SubGunLocation;
            SEGunLocation = row.SEGunLocation;

            CanTrans = row.CanTrans;
            TransPrice = row.TransPrice;
            TransId = row.TransId;
        }
    }
}