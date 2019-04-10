using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
	/// <summary>
    /// 战斗控制器
    /// </summary>
	public class CombatController : ControllerBase
	{
		/// <summary>
		/// 战斗界面
		/// </summary>
		private CombatForm m_CombatForm;

		public CombatController()
		{
		}

		/// <summary>
		/// 打开战斗界面
		/// </summary>
		public void OpenCombatForm()
		{
			if (GameEntry.UI.HasUIForm(UIFormId.CombatForm)) return;
			GameEntry.UI.OpenUIForm(UIFormId.CombatForm);
		}

		protected override void OpenUIFormSuccessHandler(object sender, GameEventArgs e)
		{
			var args = (OpenUIFormSuccessEventArgs) e;
			if (args.UIForm.Logic as CombatForm)
			{
				m_CombatForm = (CombatForm) args.UIForm.Logic;
			}
		}
	}
}

