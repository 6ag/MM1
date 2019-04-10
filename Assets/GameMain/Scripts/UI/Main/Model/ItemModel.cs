using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 道具模型基类
    /// </summary>
    [Serializable]
    public class ItemModel
    {
        /// <summary>
        /// 物品编号
        /// </summary>
        public int Id;

        /// <summary>
        /// 道具名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 类型 1物品 2人类装备 3战车装备
        /// </summary>
        public ItemType ItemType;

        /// <summary>
        /// 最大堆叠数量
        /// </summary>
        public int StackCount;

        /// <summary>
        /// 是否可丢弃
        /// </summary>
        public bool Discard;

        /// <summary>
        /// 购买价格
        /// </summary>
        public int BuyPrice;

        /// <summary>
        /// 出售价格
        /// </summary>
        public int SellPrice;

        public ItemModel(int id)
        {
            Id = id;

            var itemRow = GetItemRow();
            Name = itemRow.Name;
            Icon = itemRow.Icon;
            Description = itemRow.Description;
            ItemType = itemRow.ItemType;
            StackCount = itemRow.StackCount;
            Discard = itemRow.Discard;
            BuyPrice = itemRow.BuyPrice;
            SellPrice = itemRow.SellPrice;
        }

        #region 数据表操作

        /// <summary>
        /// 获取一行数据表中的道具数据
        /// </summary>
        /// <returns></returns>
        public DRItem GetItemRow()
        {
            return ItemModel.GetItemRow(Id);
        }

        /// <summary>
        /// 获取一行数据表中的道具数据
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <returns></returns>
        public static DRItem GetItemRow(int itemId)
        {
            var table = GameEntry.DataTable.GetDataTable<DRItem>();
            return table.GetDataRow(itemId);
        }

        /// <summary>
        /// 获取一行数据表中的人类装备数据
        /// </summary>
        /// <returns></returns>
        public DRHumanEquip GetHumanEquipRow()
        {
            return ItemModel.GetHumanEquipRow(Id);
        }

        /// <summary>
        /// 获取一行数据表中的人类装备数据
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <returns></returns>
        public static DRHumanEquip GetHumanEquipRow(int itemId)
        {
            var table = GameEntry.DataTable.GetDataTable<DRHumanEquip>();
            return table.GetDataRow(itemId);
        }

        /// <summary>
        /// 获取一行数据表中的战车装备数据 
        /// </summary>
        /// <returns></returns>
        public DRVehicleEquip GetVehicleEquipRow()
        {
            return ItemModel.GetVehicleEquipRow(Id);
        }

        /// <summary>
        /// 获取一行数据表中的战车装备数据
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <returns></returns>
        public static DRVehicleEquip GetVehicleEquipRow(int itemId)
        {
            var table = GameEntry.DataTable.GetDataTable<DRVehicleEquip>();
            return table.GetDataRow(itemId);
        }

        /// <summary>
        /// 获取一行数据表中的物品数据
        /// </summary>
        /// <returns></returns>
        public DRGoods GetGoodsRow()
        {
            return ItemModel.GetGoodsRow(Id);
        }

        /// <summary>
        /// 获取一行数据表中的物品数据
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <returns></returns>
        public static DRGoods GetGoodsRow(int itemId)
        {
            var table = GameEntry.DataTable.GetDataTable<DRGoods>();
            return table.GetDataRow(itemId);
        }

        #endregion

        /// <summary>
        /// 是否可装备
        /// </summary>
        /// <returns></returns>
        public bool CanEquip()
        {
            switch (ItemType)
            {
                case ItemType.HumanEquip:
                case ItemType.VehicleEquip:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 道具是否可使用
        /// </summary>
        /// <returns></returns>
        public bool CanUse()
        {
            switch (ItemType)
            {
                case ItemType.Goods:
                    switch (GetGoodsRow().GoodsType)
                    {
                        case GoodsType.Supply:
                        case GoodsType.Attack:
                        case GoodsType.Assist:
                        case GoodsType.Tool:
                            return true;
                    }

                    break;
            }

            return false;
        }

        /// <summary>
        /// 是否可出售
        /// </summary>
        /// <returns></returns>
        public bool CanSell()
        {
            return SellPrice > 0;
        }

        /// <summary>
        /// 是否可丢弃
        /// </summary>
        /// <returns></returns>
        public bool CanDiscard()
        {
            return Discard;
        }

        /// <summary>
        /// 获取道具的描述信息 如果是装备，则拼接属性
        /// </summary>
        /// <returns></returns>
        public string GetItemDes()
        {
            var des = string.Empty;

            // 根据道具类型显示对应的描述信息
            switch (ItemType)
            {
                case ItemType.Goods:
                    des = Description;
                    break;
                case ItemType.HumanEquip:
                    var humanEquipRow = GetHumanEquipRow();
                    switch (GetHumanEquipRow().PartType)
                    {
                        case HumanPartType.Head:
                            des = "防御+ " + humanEquipRow.Defence;
                            break;
                        case HumanPartType.Body:
                            des = "防御+ " + humanEquipRow.Defence;
                            break;
                        case HumanPartType.Hand:
                            des = "防御+ " + humanEquipRow.Defence;
                            break;
                        case HumanPartType.Foot:
                            des = "防御+ " + humanEquipRow.Defence;
                            break;
                        case HumanPartType.Weapon:
                            des = "攻击+ " + humanEquipRow.Attack;
                            break;
                    }

                    break;
                case ItemType.VehicleEquip:
                    var vehicleEquipRow = GetVehicleEquipRow();
                    var range = "";
                    switch (vehicleEquipRow.RangeType)
                    {
                        case InfluenceRangeType.One:
                            range = "单目标";
                            break;
                        case InfluenceRangeType.Group:
                            range = "一组";
                            break;
                        case InfluenceRangeType.All:
                            range = "全体";
                            break;
                    }

                    switch (vehicleEquipRow.PartType)
                    {
                        case VehiclePartType.MainCannon:
                            des = string.Format("攻击+ {0} 防御+ {1} 攻击范围 {2}", vehicleEquipRow.Attack,
                                vehicleEquipRow.Defence, range);
                            break;
                        case VehiclePartType.SubCannon:
                            des = string.Format("攻击+ {0} 防御+ {1} 攻击范围 {2}", vehicleEquipRow.Attack,
                                vehicleEquipRow.Defence, range);
                            break;
                        case VehiclePartType.SE:
                            des = string.Format("攻击+ {0} 防御+ {1} 攻击范围 {2}", vehicleEquipRow.Attack,
                                vehicleEquipRow.Defence, range);
                            break;
                        case VehiclePartType.CDevice:
                            des = string.Format("防御+ {0}", vehicleEquipRow.Defence);
                            break;
                        case VehiclePartType.Engine:
                            des = string.Format("防御+ {0} 增加载重 {1}吨", vehicleEquipRow.Defence, vehicleEquipRow.Load);
                            break;
                    }

                    break;
            }

            return des;
        }

        /// <summary>
        /// 获取道具类别
        /// </summary>
        /// <returns></returns>
        public string GetItemCategory()
        {
            var category = string.Empty;

            switch (ItemType)
            {
                case ItemType.Goods:
                    var goodsRow = GetGoodsRow();
                    switch (goodsRow.GoodsType)
                    {
                        case GoodsType.Supply:
                            category = "补给";
                            break;
                        case GoodsType.Attack:
                            category = "攻击";
                            break;
                        case GoodsType.Assist:
                            category = "辅助";
                            break;
                        case GoodsType.Tool:
                            category = "工具";
                            break;
                        case GoodsType.Other:
                            category = "杂货";
                            break;
                    }

                    break;
                case ItemType.HumanEquip:
                    var humanEquipRow = GetHumanEquipRow();
                    switch (humanEquipRow.PartType)
                    {
                        case HumanPartType.Head:
                            category = "帽子";
                            break;
                        case HumanPartType.Body:
                            category = "衣服";
                            break;
                        case HumanPartType.Hand:
                            category = "手套";
                            break;
                        case HumanPartType.Foot:
                            category = "鞋子";
                            break;
                        case HumanPartType.Weapon:
                            category = "武器";
                            break;
                    }

                    break;
                case ItemType.VehicleEquip:
                    var vehicleEquipRow = GetVehicleEquipRow();
                    switch (vehicleEquipRow.PartType)
                    {
                        case VehiclePartType.MainCannon:
                            category = "主炮";
                            break;
                        case VehiclePartType.SubCannon:
                            category = "副炮";
                            break;
                        case VehiclePartType.SE:
                            category = "S-E";
                            break;
                        case VehiclePartType.CDevice:
                            category = "C装置";
                            break;
                        case VehiclePartType.Engine:
                            category = "引擎";
                            break;
                    }

                    break;
            }

            return category;
        }
    }
}