using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 切换地图成功事件
    /// </summary>
	public class ChangeMapSuccessEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ChangeMapSuccessEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        public override void Clear()
        {

        }
    }
}
