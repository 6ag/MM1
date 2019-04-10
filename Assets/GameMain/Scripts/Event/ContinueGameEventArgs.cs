using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 继续游戏事件(有进度的情况)
    /// </summary>
    public class ContinueGameEventArgs : GameEventArgs {

        public static readonly int EventId = typeof(ContinueGameEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        public override void Clear()
        {

        }

    }
}


