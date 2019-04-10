using GameFramework;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    public static class ConfigExtension
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="configComponent">配置组件</param>
        /// <param name="configName">配置名称</param>
        /// <param name="userData"></param>
        public static void LoadConfig(this ConfigComponent configComponent, string configName, object userData = null)
        {
            if (string.IsNullOrEmpty(configName))
            {
                Log.Warning("Config name is invalid.");
                return;
            }

            configComponent.LoadConfig(configName, AssetUtility.GetConfigAsset(configName), Constant.AssetPriority.ConfigAsset, userData);
        }
    }
}
