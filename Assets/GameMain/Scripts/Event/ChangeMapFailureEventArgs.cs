using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 切换地图失败事件
    /// </summary>
	public class ChangeMapFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ChangeMapFailureEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        public override void Clear()
        {

        }
    }
}
