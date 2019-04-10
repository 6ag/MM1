using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 背包中的物品模型基类
    /// </summary>
    [Serializable]
    public class KnapsackModel : ItemModel
    {
        /// <summary>
        /// 道具在背包中的索引
        /// </summary>
        public int Index;

        /// <summary>
        /// 当前堆叠数量
        /// </summary>
        public int CurrentStackCount;

        public KnapsackModel(int id) : base(id)
        {
        }

        public KnapsackModel(int id, int index, int currentStackCount) : base(id)
        {
            Index = index;
            CurrentStackCount = currentStackCount;
        }
        
    }
}