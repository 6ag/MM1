using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 开始战斗
    /// </summary>
	public class StartCombatEventArgs : GameEventArgs
	{
		public static int EventId = typeof(StartCombatEventArgs).GetHashCode();
				
		public override int Id
		{
			get { return EventId; }
		}

		public override void Clear()
		{
			
		}
		
	}
}

