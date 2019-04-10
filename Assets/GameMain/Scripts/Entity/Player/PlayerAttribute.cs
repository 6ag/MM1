using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 玩家属性
    /// </summary>
    [Serializable]
    public class PlayerAttribute
    {
        /// <summary>
        /// 战斗等级
        /// </summary>
        public int CombatLevel;

        /// <summary>
        /// 修理等级
        /// </summary>
        public int RepairLevel;

        /// <summary>
        /// 驾驶等级
        /// </summary>
        public int DriveLevel;
        
        /// <summary>
        /// 最大血量
        /// </summary>
        public int HP;

        /// <summary>
        /// 攻击
        /// </summary>
        public int Attack;

        /// <summary>
        /// 防御
        /// </summary>
        public int Defence;

        /// <summary>
        /// 强度
        /// </summary>
        public int Strength;

        /// <summary>
        /// 智力
        /// </summary>
        public int Intelligence;

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed;

        /// <summary>
        /// 体力
        /// </summary>
        public int PhysicalPower;

        public PlayerAttribute()
        {
        }

        public PlayerAttribute(int combatLevel, int repairLevel, int driveLevel, int hp, int attack, int defence, int strength, int intelligence, int speed, int physicalPower)
        {
            CombatLevel = combatLevel;
            RepairLevel = repairLevel;
            DriveLevel = driveLevel;
            HP = hp;
            Attack = attack;
            Defence = defence;
            Strength = strength;
            Intelligence = intelligence;
            Speed = speed;
            PhysicalPower = physicalPower;
        }

        /// <summary>
        /// 属性清空
        /// </summary>
        public void Clear()
        {
            CombatLevel = 0;
            RepairLevel = 0;
            DriveLevel = 0;
            HP = 0;
            Attack = 0;
            Defence = 0;
            Strength = 0;
            Intelligence = 0;
            Speed = 0;
            PhysicalPower = 0;
        }
    }
}