using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 等级升级属性成长表
    /// </summary>
    public class DRLevelGrowth : IDataRow
    {
        /// <summary>
        /// 升级编号
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 所升等级
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 战斗等级
        /// </summary>
        public int CombatLevel { get; private set; }

        /// <summary>
        /// 修理等级
        /// </summary>
        public int RepairLevel { get; private set; }

        /// <summary>
        /// 驾驶等级
        /// </summary>
        public int DriveLevel { get; private set; }

        /// <summary>
        /// 生命值
        /// </summary>
        public int HP { get; private set; }

        /// <summary>
        /// 强度
        /// </summary>
        public int Strength { get; private set; }

        /// <summary>
        /// 智力
        /// </summary>
        public int Intelligence { get; private set; }

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed { get; private set; }

        /// <summary>
        /// 体力
        /// </summary>
        public int PhysicalPower { get; private set; }

        public void ParseDataRow(string dataRowText)
        {
            string[] text = DataTableExtension.SplitDataRow(dataRowText);
            int index = 0;
            index++;
            Id = int.Parse(text[index++]);
            Level = int.Parse(text[index++]);
            CombatLevel = int.Parse(text[index++]);
            RepairLevel = int.Parse(text[index++]);
            DriveLevel = int.Parse(text[index++]);
            HP = int.Parse(text[index++]);
            Strength = int.Parse(text[index++]);
            Intelligence = int.Parse(text[index++]);
            Speed = int.Parse(text[index++]);
            PhysicalPower = int.Parse(text[index++]);
        }
    }
}