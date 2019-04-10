using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 战车装备表
    /// </summary>
    public class DRVehicleEquip : IDataRow
    {
        /// <summary>
        /// 道具编号
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 装备部位 1主炮 2副炮 3S-E 4C装置 5引擎 6底盘
        /// </summary>
        public VehiclePartType PartType { get; private set; }

        /// <summary>
        /// 攻击力
        /// </summary>
        public int Attack { get; private set; }

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defence { get; private set; }

        /// <summary>
        /// 攻击范围 1单体 2一组 3全体
        /// </summary>
        public InfluenceRangeType RangeType { get; private set; }

        /// <summary>
        /// 主炮弹仓数量
        /// </summary>
        public int Magazine { get; private set; }

        /// <summary>
        /// 自身重量
        /// </summary>
        public float Weight { get; private set; }

        /// <summary>
        /// 增加负载重量
        /// </summary>
        public float Load { get; private set; }

        #region 底盘改造

        /// <summary>
        /// 改造底部最大防御力
        /// </summary>
        public int MaxDefence { get; private set; }

        /// <summary>
        /// 改造底部最大主炮弹仓数量
        /// </summary>
        public int MaxMagazine { get; private set; }

        /// <summary>
        /// 改造底部最大自身重量
        /// </summary>
        public float MaxWeight { get; private set; }

        /// <summary>
        /// 底盘初始主炮是否有洞
        /// </summary>
        public bool MainHole { get; private set; }

        /// <summary>
        /// 底盘初始副炮是否有洞
        /// </summary>
        public bool SubHole { get; private set; }

        /// <summary>
        /// 底盘初始S-E是否有洞
        /// </summary>
        public bool SEHole { get; private set; }

        /// <summary>
        /// 可装备到主炮位置
        /// </summary>
        public bool MainGunLocation { get; private set; }

        /// <summary>
        /// 可装备到副炮位置
        /// </summary>
        public bool SubGunLocation { get; private set; }

        /// <summary>
        /// 可装备到S-E位置
        /// </summary>
        public bool SEGunLocation { get; private set; }

        #endregion

        #region 引擎改造

        /// <summary>
        /// 是否可改造 引擎才可改造
        /// </summary>
        public bool CanTrans { get; private set; }

        /// <summary>
        /// 改造价格
        /// </summary>
        public int TransPrice { get; private set; }

        /// <summary>
        /// 改造后的道具编号
        /// </summary>
        public int TransId { get; private set; }

        #endregion

        public void ParseDataRow(string dataRowText)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowText);
            int index = 0;
            index++;
            Id = int.Parse(text[index++]);
            PartType = (VehiclePartType) int.Parse(text[index++]);
            Attack = int.Parse(text[index++]);
            Defence = int.Parse(text[index++]);
            RangeType = (InfluenceRangeType) int.Parse(text[index++]);
            Magazine = int.Parse(text[index++]);
            Weight = float.Parse(text[index++]);
            Load = float.Parse(text[index++]);
            MaxDefence = int.Parse(text[index++]);
            MaxMagazine = int.Parse(text[index++]);
            MaxWeight = float.Parse(text[index++]);

            int[] hole = Split(text[index++]);
            MainHole = hole[0] == 1;
            SubHole = hole[1] == 1;
            SEHole = hole[2] == 1;

            int[] gunLocation = Split(text[index++]);
            MainGunLocation = gunLocation[0] == 1;
            SubGunLocation = gunLocation[1] == 1;
            SEGunLocation = gunLocation[2] == 1;

            CanTrans = bool.Parse(text[index++]);
            TransPrice = int.Parse(text[index++]);
            TransId = int.Parse(text[index++]);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int[] Split(string str)
        {
            string[] text = DataTableExtension.SplitValue(str);
            return new[] {int.Parse(text[0]), int.Parse(text[1]), int.Parse(text[2])};
        }
    }
}