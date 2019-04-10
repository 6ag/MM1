using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
	/// <summary>
    /// 控制器组件，管理所有UI控制器
    /// </summary>
	public class ControllerComponent : GameFrameworkComponent 
	{
		/// <summary>
		/// 人类装备控制器
		/// </summary>
		public CharacterInfoController CharacterInfo { get; private set; }
		
		/// <summary>
		/// 背包控制器
		/// </summary>
		public KnapsackController Knapsack { get; private set; }
		
		/// <summary>
		/// 主界面控制器
		/// </summary>
		public MainController Main { get; private set; }
		
		/// <summary>
		/// 商店控制器
		/// </summary>
		public ShopController Shop { get; private set; }
		
		/// <summary>
		/// 开始游戏控制器
		/// </summary>
		public StartGameController StartGame { get; private set; }

		/// <summary>
		/// 战斗控制器
		/// </summary>
		public CombatController Combat { get; private set; }

		private void Start()
		{
			CharacterInfo = new CharacterInfoController();
			Knapsack = new KnapsackController();
			Main = new MainController();
			Shop = new ShopController();
			StartGame = new StartGameController();
			Combat = new CombatController();
		}
	}
}

