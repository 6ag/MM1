using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 战斗流程
    /// </summary>
	public class ProcedureCombat : ProcedureBase 
	{
		protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
		{
			base.OnEnter(procedureOwner);
			
			// 打开战斗系统UI界面
			GameEntry.Controller.Combat.OpenCombatForm();
		}

		protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
		{
			base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
			
		}

		protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
		{
			base.OnLeave(procedureOwner, isShutdown);
		}

		protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
		{
			base.OnDestroy(procedureOwner);
		}
	}
}

