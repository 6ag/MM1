using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace MetalMax
{
	/// <summary>
    /// 控制器基类
    /// </summary>
	public class ControllerBase : IDisposable
	{
		public ControllerBase()
		{
			// 监听事件
			GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OpenUIFormSuccessHandler);
			GameEntry.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OpenUIFormFailureHandler);
		}
		
		/// <summary>
		/// 打开UI界面成功
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void OpenUIFormSuccessHandler(object sender, GameEventArgs e)
		{
			
		}
		
		/// <summary>
		/// 打开UI界面失败
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void OpenUIFormFailureHandler(object sender, GameEventArgs e)
		{
			
		}
		
		public virtual void Dispose()
		{
			// 取消监听事件
			GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OpenUIFormSuccessHandler);
			GameEntry.Event.Unsubscribe(OpenUIFormFailureEventArgs.EventId, OpenUIFormFailureHandler);
		}
		
	}
}

