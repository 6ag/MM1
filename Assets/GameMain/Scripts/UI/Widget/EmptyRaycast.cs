using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI
{
	/// <summary>
    /// 用于界面遮挡事件
    /// </summary>
	public class EmptyRaycast : MaskableGraphic
	{
		protected EmptyRaycast()
		{
			useLegacyMeshGeneration = false;
		}
 
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			toFill.Clear();
		}
	}
}

