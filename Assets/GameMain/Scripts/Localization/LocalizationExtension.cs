using GameFramework;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 本地化扩展
    /// </summary>
    public static class LocalizationExtension
    {
        /// <summary>
        /// 加载字典
        /// </summary>
        /// <param name="localizationComponent"></param>
        /// <param name="dictionaryName"></param>
        /// <param name="userData"></param>
        public static void LoadDictionary(this LocalizationComponent localizationComponent, string dictionaryName, object userData = null)
        {
            if (string.IsNullOrEmpty(dictionaryName))
            {
                Log.Warning("Dictionary name is invalid.");
                return;
            }

            localizationComponent.LoadDictionary(dictionaryName, AssetUtility.GetDictionaryAsset(dictionaryName), Constant.AssetPriority.DictionaryAsset, userData);
        }
    }
}
