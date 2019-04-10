using GameFramework;
using GameFramework.DataTable;
using GameFramework.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// UI界面扩展
    /// </summary>
    public static class UIExtension
    {
        public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup, float alpha, float duration)
        {
            float time = 0f;
            float originalAlpha = canvasGroup.alpha;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
                yield return new WaitForEndOfFrame();
            }

            canvasGroup.alpha = alpha;
        }

        public static IEnumerator SmoothValue(this Slider slider, float value, float duration)
        {
            float time = 0f;
            float originalValue = slider.value;
            while (time < duration)
            {
                time += Time.deltaTime;
                slider.value = Mathf.Lerp(originalValue, value, time / duration);
                yield return new WaitForEndOfFrame();
            }

            slider.value = value;
        }

        #region 打开UI界面

        /// <summary>
        /// 打开UI界面
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="uiFormId">界面枚举值</param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static int? OpenUIForm(this UIComponent uiComponent, UIFormId uiFormId, object userData = null)
        {
            return uiComponent.OpenUIForm((int)uiFormId, userData);
        }

        /// <summary>
        /// 打开UI界面（使用带枚举参数的方法）
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="uiFormId">界面编号</param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static int? OpenUIForm(this UIComponent uiComponent, int uiFormId, object userData = null)
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
            {
                Log.Warning("Can not load UI form '{0}' from data table.", uiFormId.ToString());
                return null;
            }

            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            if (!drUIForm.AllowMultiInstance)
            {
                if (uiComponent.IsLoadingUIForm(assetName))
                {
                    return null;
                }

                if (uiComponent.HasUIForm(assetName))
                {
                    return null;
                }
            }

            return uiComponent.OpenUIForm(assetName, drUIForm.UIGroupName, Constant.AssetPriority.UIFormAsset, drUIForm.PauseCoveredUIForm, userData);
        }

        #endregion
        
        #region 关闭UI界面

        /// <summary>
        /// 关闭UI界面
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="uiForm">UI界面对象</param>
        public static void CloseUIForm(this UIComponent uiComponent, UGuiForm uiForm)
        {
            uiComponent.CloseUIForm(uiForm.UIForm);
        }

        /// <summary>
        /// 关闭UI界面
        /// </summary>
        /// <param name="uiComponent"></param>
        /// <param name="uiFormId">UI界面枚举</param>
        /// <param name="uiGroupName">UI界面组</param>
        public static void CloseUIForm(this UIComponent uiComponent, UIFormId uiFormId, string uiGroupName = null)
        {
            var uguiForm = uiComponent.GetUIForm(uiFormId, uiGroupName);
            uiComponent.CloseUIForm(uguiForm);
        }

        #endregion

        #region 判断UI界面是否存在

        /// <summary>
        /// 是否存在指定UI界面
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="uiFormId">UI界面枚举值</param>
        /// <param name="uiGroupName">UI界面组名称</param>
        /// <returns></returns>
        public static bool HasUIForm(this UIComponent uiComponent, UIFormId uiFormId, string uiGroupName = null)
        {
            return uiComponent.HasUIForm((int)uiFormId, uiGroupName);
        }

        /// <summary>
        /// 是否存在指定UI界面（使用带枚举参数的方法）
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="uiFormId">UI界面编号</param>
        /// <param name="uiGroupName">UI界面组名称</param>
        /// <returns></returns>
        public static bool HasUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
            {
                return false;
            }

            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            if (string.IsNullOrEmpty(uiGroupName))
            {
                return uiComponent.HasUIForm(assetName);
            }

            IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
            if (uiGroup == null)
            {
                return false;
            }

            return uiGroup.HasUIForm(assetName);
        }

        #endregion

        #region 获取UI界面

        /// <summary>
        /// 获取UI界面对象
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="uiFormId">UI界面枚举值</param>
        /// <param name="uiGroupName">UI界面组名称</param>
        /// <returns></returns>
        public static UGuiForm GetUIForm(this UIComponent uiComponent, UIFormId uiFormId, string uiGroupName = null)
        {
            return uiComponent.GetUIForm((int)uiFormId, uiGroupName);
        }

        /// <summary>
        /// 获取UI界面对象（使用带枚举参数的方法）
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="uiFormId">UI界面编号</param>
        /// <param name="uiGroupName">UI界面组名称</param>
        /// <returns></returns>
        public static UGuiForm GetUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
            {
                return null;
            }

            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            UIForm uiForm = null;
            if (string.IsNullOrEmpty(uiGroupName))
            {
                uiForm = uiComponent.GetUIForm(assetName);
                if (uiForm == null)
                {
                    return null;
                }

                return (UGuiForm)uiForm.Logic;
            }

            IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
            if (uiGroup == null)
            {
                return null;
            }

            uiForm = (UIForm)uiGroup.GetUIForm(assetName);
            if (uiForm == null)
            {
                return null;
            }

            return (UGuiForm)uiForm.Logic;
        }

        #endregion
        
        /// <summary>
        /// 打开提示框
        /// </summary>
        /// <param name="uiComponent">UI组件</param>
        /// <param name="dialogParams">提示框参数</param>
        public static void OpenDialog(this UIComponent uiComponent, DialogParams dialogParams)
        {
            //uiComponent.OpenUIForm(UIFormId.DialogForm, dialogParams);
        }
    }
}
