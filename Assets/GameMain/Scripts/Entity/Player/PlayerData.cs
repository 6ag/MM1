using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 玩家数据
    /// </summary>
    [Serializable]
    public class PlayerData : EntityData
    {
        /// <summary>
        /// 玩家身份 - 这个要去掉
        /// </summary>
        public PlayerType PlayerType;

        /// <summary>
        /// 角色昵称
        /// </summary>
        public string Name;

        /// <summary>
        /// 人物等级
        /// </summary>
        public int Level;

        /// <summary>
        /// 当前经验值（人物等级）
        /// </summary>
        public int Exp;

        /// <summary>
        /// 下一级所需经验值（人物等级）
        /// </summary>
        public int NextExp;

        #region 装备模型

        /// <summary>
        /// 武器装备模型
        /// </summary>
        public HumanEquipModel WeaponModel;

        /// <summary>
        /// 头部装备模型
        /// </summary>
        public HumanEquipModel HeadModel;

        /// <summary>
        /// 胸部装备模型
        /// </summary>
        public HumanEquipModel BodyModel;

        /// <summary>
        /// 手部装备模型
        /// </summary>
        public HumanEquipModel HandModel;

        /// <summary>
        /// 脚部装备模型
        /// </summary>
        public HumanEquipModel FootModel;

        #endregion

        #region 角色属性

        /// <summary>
        /// 当前属性
        /// </summary>
        public PlayerAttribute CurrentAttribute;

        /// <summary>
        /// 总属性 = 角色基础属性 + 升级成长属性 + 装备属性 + 附加属性
        /// </summary>
        public PlayerAttribute TotalAttribute;

        /// <summary>
        /// 角色基础属性
        /// </summary>
        public PlayerAttribute BaseAttribute;

        /// <summary>
        /// 升级成长属性
        /// </summary>
        public PlayerAttribute GrowthAttribute;

        /// <summary>
        /// 装备属性
        /// </summary>
        public PlayerAttribute EquipAttribute;

        /// <summary>
        /// 附加属性（buff叠加等）
        /// </summary>
        public PlayerAttribute AdditionAttribute;

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id">角色表中的编号</param>
        /// <param name="playerType">玩家类型 固定3种，就是3个队友</param>
        public PlayerData(int id, PlayerType playerType) : base(id)
        {
            var row = GameEntry.DataTable.GetDataRow<DRPlayer>(id);

            PlayerType = playerType;
            switch (playerType)
            {
                case PlayerType.Rebana:
                    Name = "雷班纳";
                    break;
                case PlayerType.Cliff:
                    Name = "克里夫";
                    break;
                case PlayerType.Margaret:
                    Name = "玛格丽特";
                    break;
            }

            Level = row.Level;
            Exp = row.Exp;
            // 下一级所需经验
            var table = GameEntry.DataTable.GetDataTable<DRLevel>();
            var levelRow = table.GetDataRow(Level + 1);
            if (levelRow != null)
            {
                NextExp = levelRow.Exp;
            }

            TotalAttribute = new PlayerAttribute();
            CurrentAttribute = new PlayerAttribute();
            BaseAttribute = new PlayerAttribute(row.CombatLevel, row.RepairLevel, row.DriveLevel, row.HP, row.Attack, row.Defence, row.Strength, row.Intelligence, row.Speed, row.PhysicalPower);
            GrowthAttribute = new PlayerAttribute();
            EquipAttribute = new PlayerAttribute();
            AdditionAttribute = new PlayerAttribute();

            // 初始装备
            foreach (var equipId in row.InitEquipIds)
            {
                Equip(new HumanEquipModel(equipId) {OwerPlayerType = PlayerType});
            }

            // 更新属性
            UpdateTotalAttribute(true);

            // 初始道具
            foreach (var itemId in row.InitItemIds)
            {
                GameEntry.Controller.Knapsack.AddItem(itemId, 1, GetItemSourceType.Monster);
            }
            
            // 保存角色数据到归档组件
            GameEntry.Archive.Data.SetPlayerData(this);
        }

        #region 计算属性

        /// <summary>
        /// 更新总属性
        /// </summary>
        /// <param name="isCoverCurrent">总属性是否覆盖当前属性 初始化/角色升级都应该覆盖</param>
        private void UpdateTotalAttribute(bool isCoverCurrent = false)
        {
            TotalAttribute.Clear();

            // 累加基础属性
            TotalAttribute.CombatLevel += BaseAttribute.CombatLevel;
            TotalAttribute.RepairLevel += BaseAttribute.RepairLevel;
            TotalAttribute.DriveLevel += BaseAttribute.DriveLevel;
            TotalAttribute.HP += BaseAttribute.HP;
            TotalAttribute.Attack += BaseAttribute.Attack;
            TotalAttribute.Defence += BaseAttribute.Defence;
            TotalAttribute.Strength += BaseAttribute.Strength;
            TotalAttribute.Intelligence += BaseAttribute.Intelligence;
            TotalAttribute.Speed += BaseAttribute.Speed;
            TotalAttribute.PhysicalPower += BaseAttribute.PhysicalPower;

            // 累加升级成长属性
            TotalAttribute.CombatLevel += GrowthAttribute.CombatLevel;
            TotalAttribute.RepairLevel += GrowthAttribute.RepairLevel;
            TotalAttribute.DriveLevel += GrowthAttribute.DriveLevel;
            TotalAttribute.HP += GrowthAttribute.HP;
            TotalAttribute.Attack += GrowthAttribute.Attack;
            TotalAttribute.Defence += GrowthAttribute.Defence;
            TotalAttribute.Strength += GrowthAttribute.Strength;
            TotalAttribute.Intelligence += GrowthAttribute.Intelligence;
            TotalAttribute.Speed += GrowthAttribute.Speed;
            TotalAttribute.PhysicalPower += GrowthAttribute.PhysicalPower;

            // 累加装备属性
            TotalAttribute.CombatLevel += EquipAttribute.CombatLevel;
            TotalAttribute.RepairLevel += EquipAttribute.RepairLevel;
            TotalAttribute.DriveLevel += EquipAttribute.DriveLevel;
            TotalAttribute.HP += EquipAttribute.HP;
            TotalAttribute.Attack += EquipAttribute.Attack;
            TotalAttribute.Defence += EquipAttribute.Defence;
            TotalAttribute.Strength += EquipAttribute.Strength;
            TotalAttribute.Intelligence += EquipAttribute.Intelligence;
            TotalAttribute.Speed += EquipAttribute.Speed;
            TotalAttribute.PhysicalPower += EquipAttribute.PhysicalPower;

            // 累加附加属性
            TotalAttribute.CombatLevel += AdditionAttribute.CombatLevel;
            TotalAttribute.RepairLevel += AdditionAttribute.RepairLevel;
            TotalAttribute.DriveLevel += AdditionAttribute.DriveLevel;
            TotalAttribute.HP += AdditionAttribute.HP;
            TotalAttribute.Attack += AdditionAttribute.Attack;
            TotalAttribute.Defence += AdditionAttribute.Defence;
            TotalAttribute.Strength += AdditionAttribute.Strength;
            TotalAttribute.Intelligence += AdditionAttribute.Intelligence;
            TotalAttribute.Speed += AdditionAttribute.Speed;
            TotalAttribute.PhysicalPower += AdditionAttribute.PhysicalPower;

            if (!isCoverCurrent) return;
            // 总属性覆盖当前属性
            CurrentAttribute.CombatLevel = TotalAttribute.CombatLevel;
            CurrentAttribute.RepairLevel = TotalAttribute.RepairLevel;
            CurrentAttribute.DriveLevel = TotalAttribute.DriveLevel;
            CurrentAttribute.HP = TotalAttribute.HP;
            CurrentAttribute.Attack = TotalAttribute.Attack;
            CurrentAttribute.Defence = TotalAttribute.Defence;
            CurrentAttribute.Strength = TotalAttribute.Strength;
            CurrentAttribute.Intelligence = TotalAttribute.Intelligence;
            CurrentAttribute.Speed = TotalAttribute.Speed;
            CurrentAttribute.PhysicalPower = TotalAttribute.PhysicalPower;
        }

        /// <summary>
        /// 计算升级成长属性
        /// </summary>
        private void CalculateGrowthAttribute()
        {
            GrowthAttribute.Clear();

            var table = GameEntry.DataTable.GetDataTable<DRLevelGrowth>();
            foreach (var levelGrowth in table)
            {
                if (Level < levelGrowth.Level)
                {
                    break;
                }

                GrowthAttribute.CombatLevel += levelGrowth.CombatLevel;
                GrowthAttribute.RepairLevel += levelGrowth.RepairLevel;
                GrowthAttribute.DriveLevel += levelGrowth.DriveLevel;
                GrowthAttribute.HP += levelGrowth.HP;
                GrowthAttribute.Strength += levelGrowth.Strength;
                GrowthAttribute.Intelligence += levelGrowth.Intelligence;
                GrowthAttribute.Speed += levelGrowth.Speed;
                GrowthAttribute.PhysicalPower += levelGrowth.PhysicalPower;
            }
        }

        /// <summary>
        /// 计算装备属性
        /// </summary>
        private void CalculateEquipAttribute()
        {
            EquipAttribute.Clear();

            if (WeaponModel != null && WeaponModel.Id != 0)
            {
                EquipAttribute.Attack += WeaponModel.Attack;
            }

            if (HeadModel != null && HeadModel.Id != 0)
            {
                EquipAttribute.Defence += HeadModel.Defence;
            }

            if (BodyModel != null && BodyModel.Id != 0)
            {
                EquipAttribute.Defence += BodyModel.Defence;
            }

            if (HandModel != null && HandModel.Id != 0)
            {
                EquipAttribute.Defence += HandModel.Defence;
            }

            if (FootModel != null && FootModel.Id != 0)
            {
                EquipAttribute.Defence += FootModel.Defence;
            }
        }

        /// <summary>
        /// 计算附加属性
        /// </summary>
        private void CalculateAdditionAttribute()
        {
            AdditionAttribute.Clear();
        }

        #endregion

        #region 装备属性

        /// <summary>
        /// 穿戴装备 如果原先有装备则会脱掉
        /// </summary>
        /// <param name="model">装备模型</param>
        /// <returns>被替换的原装备</returns>
        public HumanEquipModel Equip(HumanEquipModel model)
        {
            if (model == null || model.Id == 0) return null;

            // 原先装备
            HumanEquipModel previousModel = null;

            switch (model.PartType)
            {
                case HumanPartType.Head:
                    previousModel = HeadModel;
                    break;
                case HumanPartType.Body:
                    previousModel = BodyModel;
                    break;
                case HumanPartType.Hand:
                    previousModel = HandModel;
                    break;
                case HumanPartType.Foot:
                    previousModel = FootModel;
                    break;
                case HumanPartType.Weapon:
                    previousModel = WeaponModel;
                    break;
            }

            // 脱下原先装备
            Unequip(previousModel);

            // 佩戴新装备
            switch (model.PartType)
            {
                case HumanPartType.Head:
                    HeadModel = model;
                    break;
                case HumanPartType.Body:
                    BodyModel = model;
                    break;
                case HumanPartType.Hand:
                    HandModel = model;
                    break;
                case HumanPartType.Foot:
                    FootModel = model;
                    break;
                case HumanPartType.Weapon:
                    WeaponModel = model;
                    break;
            }

            // 更新装备属性和当前属性
            if (model.Attack != 0)
            {
                EquipAttribute.Attack += model.Attack;
                CurrentAttribute.Attack += model.Attack;
            }

            if (model.Defence != 0)
            {
                EquipAttribute.Defence += model.Defence;
                CurrentAttribute.Defence += model.Defence;
            }

            // 更新总属性
            UpdateTotalAttribute();

            // 因为序列化了，所以要判断id
            if (previousModel == null || previousModel.Id == 0)
            {
                return null;
            }

            return previousModel;
        }

        /// <summary>
        /// 脱下装备
        /// </summary>
        /// <param name="model">装备模型</param>
        public void Unequip(HumanEquipModel model)
        {
            if (model == null || model.Id == 0) return;

            switch (model.PartType)
            {
                case HumanPartType.Head:
                    HeadModel = null;
                    break;
                case HumanPartType.Body:
                    BodyModel = null;
                    break;
                case HumanPartType.Hand:
                    HandModel = null;
                    break;
                case HumanPartType.Foot:
                    FootModel = null;
                    break;
                case HumanPartType.Weapon:
                    WeaponModel = null;
                    break;
            }

            // 更新装备属性和当前属性
            if (model.Attack != 0)
            {
                EquipAttribute.Attack -= model.Attack;
                CurrentAttribute.Attack -= model.Attack;
            }

            if (model.Defence != 0)
            {
                EquipAttribute.Defence -= model.Defence;
                CurrentAttribute.Defence -= model.Defence;
            }

            // 更新总属性
            UpdateTotalAttribute();
        }

        #endregion

        #region 附加属性

        /// <summary>
        /// 获得Debuff 减少对应属性
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="value"></param>
        public void GetDebuff(AddAttributeType attributeType, float value)
        {
            switch (attributeType)
            {
                case AddAttributeType.HP:
                    break;
                case AddAttributeType.Attack:
                    break;
                case AddAttributeType.Defence:
                    break;
            }
        }

        /// <summary>
        /// Debuff结束 恢复对应属性
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="value"></param>
        public void DebuffFinished(AddAttributeType attributeType, float value)
        {
            switch (attributeType)
            {
                case AddAttributeType.HP:
                    break;
                case AddAttributeType.Attack:
                    break;
                case AddAttributeType.Defence:
                    break;
            }
        }

        #endregion
    }
}