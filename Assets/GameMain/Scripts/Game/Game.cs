using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 游戏基类
    /// </summary>
	public abstract class Game
	{
		/// <summary>
		/// 初始化
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// 轮询
		/// </summary>
		/// <param name="elapseSeconds"></param>
		/// <param name="realElapseSeconds"></param>
		public abstract void Update(float elapseSeconds, float realElapseSeconds);

	}
}

