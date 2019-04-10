using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 获取道具的来源类型
    /// </summary>
    public enum GetItemSourceType
    {
        /// <summary>
        /// 商店购买
        /// </summary>
        Shop = 1,

        /// <summary>
        /// 怪物掉落
        /// </summary>
        Monster = 2,

        /// <summary>
        /// 装备卸下、仓库取出、锻造孔取出（本来就是自己的道具，只是换了个位置存储）
        /// </summary>
        Exchange = 3
    }
}