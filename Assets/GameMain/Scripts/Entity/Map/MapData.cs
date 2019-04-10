using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 
    /// </summary>
	public class MapData : EntityData 
	{
		/// <summary>
		/// 地图上的NPC编号列表
		/// </summary>
		public List<int> NPCs { get; private set; }

		public MapData(int id, List<int> npCs) : base(id)
		{
			NPCs = npCs;
		}
	}
}

