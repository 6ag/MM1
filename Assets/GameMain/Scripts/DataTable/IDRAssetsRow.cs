using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 资源表父接口
    /// </summary>
	public interface IDRAssetsRow : IDataRow
	{	
		/// <summary>
		/// 资源名称
		/// </summary>
		string AssetName { get; }
	}
}

