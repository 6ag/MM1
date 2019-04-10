using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 所有UI视图的基类
    /// </summary>
    public abstract class ViewBase : MonoBehaviour
    {
        protected virtual void Start()
        {
            var buttons = GetComponentsInChildren<Button>(true);
            foreach (var item in buttons)
            {
                var button = item;
                button.onClick.AddListener(delegate { OnButtonClick(button.gameObject); });
            }
        }

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnButtonClick(GameObject sender)
        {
            //PlayUISound(1);
        }
    }
}