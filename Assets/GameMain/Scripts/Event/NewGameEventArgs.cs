using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 开始新游戏事件(没有进度的情况)
    /// </summary>
    public class NewGameEventArgs : GameEventArgs {

        public static readonly int EventId = typeof(NewGameEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }
        
        /// <summary>
        /// 玩家姓名
        /// </summary>
        public string PlayerName { get; set; }

        public override void Clear()
        {
            
        }
    }
}


