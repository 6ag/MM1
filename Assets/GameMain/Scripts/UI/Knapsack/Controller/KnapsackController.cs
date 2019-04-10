using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityScript.Lang;

namespace MetalMax
{
    /// <summary>
    /// 背包控制器
    /// 控制器处理道具的拾取，背包视图只处理道具显示
    /// </summary>
    public class KnapsackController : ControllerBase
    {
        /// <summary>
        /// 背包界面
        /// </summary>
        private KnapsackForm m_KnapsackForm;

        /// <summary>
        /// 背包容量固定25格
        /// </summary>
        public const int KnapsackCapacity = 36;

        public KnapsackController()
        {
            
        }

        #region 打开关闭背包

        /// <summary>
        /// 打开背包界面
        /// </summary>
        public void OpenKnapsackForm()
        {
            if (GameEntry.UI.HasUIForm(UIFormId.KnapsackForm)) return;
            GameEntry.UI.OpenUIForm(UIFormId.KnapsackForm);
        }

        /// <summary>
        /// 关闭背包界面
        /// </summary>
        public void CloseKnapsackForm()
        {
            if (!GameEntry.UI.HasUIForm(UIFormId.KnapsackForm)) return;
            GameEntry.UI.CloseUIForm(UIFormId.KnapsackForm);
        }

        protected override void OpenUIFormSuccessHandler(object sender, GameEventArgs e)
        {
            base.OpenUIFormSuccessHandler(sender, e);
            var args = (OpenUIFormSuccessEventArgs) e;
            if (args.UIForm.Logic as KnapsackForm)
            {
                m_KnapsackForm = (KnapsackForm) args.UIForm.Logic;
            }
        }

        #endregion

        /// <summary>
        /// 更新金币和使用的背包格子数量UI
        /// </summary>
        public void UpdateCoinAndUsedBoxCountUI()
        {
            if (m_KnapsackForm == null) return;
            m_KnapsackForm.UpdateCoinAndUsedBoxCountUI();
        }

        #region 获得道具处理

        /// <summary>
        /// 获得道具
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <param name="itemCount">道具数量</param>
        /// <param name="sourceType">来源类型</param>
        public void AddItem(int itemId, int itemCount, GetItemSourceType sourceType)
        {
            if (!KnapsackCapacityEnough(itemId, itemCount))
            {
                // 弹窗提示用户背包满了
                Log.Debug("背包装不下这么多东西");

                if (sourceType == GetItemSourceType.Shop)
                {
                    // 如果是购买道具则返还金币
                    GameEntry.Archive.Data.AddGold(ItemModel.GetItemRow(itemId).BuyPrice * itemCount);

                    UpdateCoinAndUsedBoxCountUI();
                    return;
                }

                // 如果是普通拾取，就算背包一次性放不下这么多东西，也要放满再说
            }

            AddStackItem(itemId, itemCount, sourceType);
        }

        /// <summary>
        /// 获取道具后先更新堆叠的道具
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <param name="itemCount">道具数量</param>
        /// <param name="itemSourceType">道具来源</param>
        private void AddStackItem(int itemId, int itemCount, GetItemSourceType itemSourceType)
        {
            // 先去找背包中相同的道具，堆叠存放
            for (var i = 0; i < GameEntry.Archive.Data.KnapsackModels.Count; i++)
            {
                if (GameEntry.Archive.Data.KnapsackModels[i].Id == itemId && GameEntry.Archive.Data.KnapsackModels[i].CurrentStackCount < GameEntry.Archive.Data.KnapsackModels[i].StackCount)
                {
                    // 当前格子剩余堆叠数
                    var surplusStackCount = GameEntry.Archive.Data.KnapsackModels[i].StackCount - GameEntry.Archive.Data.KnapsackModels[i].CurrentStackCount;
                    if (surplusStackCount >= itemCount)
                    {
                        // 可以放得下
                        GameEntry.Archive.Data.KnapsackModels[i].CurrentStackCount += itemCount;
                        if (m_KnapsackForm)
                        {
                            m_KnapsackForm.AddItem(GameEntry.Archive.Data.KnapsackModels[i], false);
                        }

                        Log.Debug("放完");
                        return;
                    }

                    // 放满当前格子
                    GameEntry.Archive.Data.KnapsackModels[i].CurrentStackCount += surplusStackCount;
                    itemCount -= surplusStackCount;
                    if (m_KnapsackForm)
                    {
                        m_KnapsackForm.AddItem(GameEntry.Archive.Data.KnapsackModels[i], false);
                    }

                    // 剩下的
                    Log.Debug("放满当前格子后剩下 " + itemCount);
                }
            }

            // 相同道具堆叠满了，开始放新格子
            AddNewItem(itemId, itemCount, itemSourceType);
        }

        /// <summary>
        /// 堆叠道具更新满后，添加新格子
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemCount"></param>
        /// <param name="itemSourceType"></param>
        private void AddNewItem(int itemId, int itemCount, GetItemSourceType itemSourceType)
        {
            while (true)
            {
                // 背包满了
                if (GameEntry.Archive.Data.KnapsackModels.Count == KnapsackCapacity)
                {
                    if (itemSourceType != GetItemSourceType.Shop)
                    {
                        Log.Debug("没有存放进背包的数量 " + itemCount);
                    }

                    break;
                }

                var model = new KnapsackModel(itemId) {Index = GetEmptySoltIndex()};
                GameEntry.Archive.Data.KnapsackModels.Add(model);

                // 可以放得下
                if (model.StackCount >= itemCount)
                {
                    model.CurrentStackCount += itemCount;
                    if (m_KnapsackForm)
                    {
                        m_KnapsackForm.AddItem(model, true);
                    }

                    Log.Debug("放完");
                    break;
                }

                // 放不下
                model.CurrentStackCount = model.StackCount;
                if (m_KnapsackForm)
                {
                    m_KnapsackForm.AddItem(model, true);
                }

                // 剩下的
                itemCount -= model.StackCount;
                Log.Debug("放一个新格子后剩下 " + itemCount);
            }
        }

        /// <summary>
        /// 背包容量是否足够 - 拾取的道具是否能够全部存放进背包
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <param name="itemCount">道具数量</param>
        /// <returns></returns>
        public bool KnapsackCapacityEnough(int itemId, int itemCount)
        {
            var count = 0; // 背包中当前道具可存放的数量
            var maxStackCount = 0; // 当前道具最大堆叠数量

            foreach (var model in GameEntry.Archive.Data.KnapsackModels)
            {
                if (model.Id == itemId)
                {
                    maxStackCount = model.StackCount;
                    count += model.StackCount - model.CurrentStackCount;
                    if (count >= itemCount)
                    {
                        return true;
                    }
                }
            }

            // 当前道具的最大堆叠数量
            if (maxStackCount == 0)
            {
                var itemTable = GameEntry.DataTable.GetDataTable<DRItem>();
                var itemRow = itemTable.GetDataRow(itemId);
                maxStackCount = itemRow.StackCount;
            }

            count += (KnapsackCapacity - GameEntry.Archive.Data.KnapsackModels.Count) * maxStackCount;
            if (count >= itemCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取一个空的背包格子索引 -1就是背包满了
        /// </summary>
        /// <returns></returns>
        private int GetEmptySoltIndex()
        {
            // 所有已经有存放物品的格子索引集合
            var indexs = new List<int>();
            for (var j = 0; j < GameEntry.Archive.Data.KnapsackModels.Count; j++)
            {
                indexs.Add(GameEntry.Archive.Data.KnapsackModels[j].Index);
            }

            // 升序排序
            indexs.Sort((a, b) => { return a > b ? 1 : -1; });

            // 获取一个背包中没有被使用的最小索引
            for (var i = 0; i < KnapsackCapacity; i++)
            {
                if (!indexs.Contains(i))
                {
                    return i;
                }
            }

            Log.Debug("背包已经满了2，这里不应该执行。如果执行了说明逻辑有错误");
            return -1;
        }

        #endregion

        #region 失去道具处理

        /// <summary>
        /// 从背包中移除道具 （出售、使用、装备等）
        /// </summary>
        /// <param name="index">背包索引</param>
        /// <param name="count">道具数量</param>
        public void ReduceStackCount(int index, int count)
        {
            for (var i = 0; i < GameEntry.Archive.Data.KnapsackModels.Count; i++)
            {
                if (GameEntry.Archive.Data.KnapsackModels[i].Index == index)
                {
                    if (GameEntry.Archive.Data.KnapsackModels[i].CurrentStackCount < count)
                    {
                        // 这里逻辑还要完善，如果数量超过了当前格子堆叠的数量，应该还去遍历查找其他格子的数量进行操作
                        return;
                    }
                    
                    GameEntry.Archive.Data.KnapsackModels[i].CurrentStackCount -= count;

                    // 如果道具堆叠数量为0，则从道具集合移除
                    if (GameEntry.Archive.Data.KnapsackModels[i].CurrentStackCount == 0)
                    {
                        GameEntry.Archive.Data.KnapsackModels.RemoveAt(i);
                    }

                    break;
                }
            }

            if (m_KnapsackForm != null)
            {
                m_KnapsackForm.UpdateCurrentStackCount(index);
                UpdateCoinAndUsedBoxCountUI();
            }
        }

        #endregion

        #region 操作道具 使用、装备、丢弃、出售

        /// <summary>
        /// 使用道具
        /// </summary>
        /// <param name="model">道具模型</param>
        public void UseItem(KnapsackModel model)
        {
            var goodsRow = model.GetGoodsRow();
            switch (goodsRow.GoodsType)
            {
                case GoodsType.Supply:
                    break;
                case GoodsType.Attack:
                    break;
                case GoodsType.Assist:
                    break;
                case GoodsType.Tool:
                    break;
            }
        }

        /// <summary>
        /// 装备道具
        /// </summary>
        /// <param name="model">道具模型</param>
        public void EquipItem(KnapsackModel model)
        {
            ReduceStackCount(model.Index, 1);
            GameEntry.Controller.CharacterInfo.WornEquipment(model.Id);
        }

        /// <summary>
        /// 丢弃道具
        /// </summary>
        /// <param name="model">道具模型</param>
        /// <param name="count">数量</param>
        public void DiscardItem(KnapsackModel model, int count = 1)
        {
            ReduceStackCount(model.Index, count);
        }

        /// <summary>
        /// 出售道具
        /// </summary>
        /// <param name="model">道具模型</param>
        /// <param name="count">数量</param>
        public void SellItem(KnapsackModel model, int count = 1)
        {
            GameEntry.Archive.Data.AddGold(model.SellPrice);
            ReduceStackCount(model.Index, count);
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            m_KnapsackForm = null;
        }
    }
}