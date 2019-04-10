using GameFramework;

namespace MetalMax
{
    /// <summary>
    /// 资源扩展
    /// </summary>
    public static class AssetUtility
    {
        /// <summary>
        /// 获取配置资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetConfigAsset(string assetName)
        {
            return string.Format("Assets/GameMain/Configs/{0}.txt", assetName);
        }

        /// <summary>
        /// 获取数据表资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetDataTableAsset(string assetName)
        {
            return string.Format("Assets/GameMain/DataTables/{0}.csv", assetName);
        }

        /// <summary>
        /// 获取字典资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetDictionaryAsset(string assetName)
        {
            return string.Format("Assets/GameMain/Localization/{0}/Dictionaries/{1}.xml", GameEntry.Localization.Language.ToString(), assetName);
        }

        /// <summary>
        /// 获取字体资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetFontAsset(string assetName)
        {
            return string.Format("Assets/GameMain/Localization/{0}/Fonts/{1}.ttf", GameEntry.Localization.Language.ToString(), assetName);
        }

        /// <summary>
        /// 获取场景资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetSceneAsset(string assetName)
        {
            return string.Format("Assets/GameMain/Scenes/{0}.unity", assetName);
        }

        /// <summary>
        /// 获取音乐资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetMusicAsset(string assetName)
        {
            return string.Format("Assets/GameMain/Music/{0}.mp3", assetName);
        }

        /// <summary>
        /// 获取游戏音效资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetSoundAsset(string assetName)
        {
            return string.Format("Assets/GameMain/Sounds/{0}.wav", assetName);
        }

        /// <summary>
        /// 获取实体资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetEntityAsset(string assetName)
        {
            return string.Format("Assets/GameMain/Entities/{0}.prefab", assetName);
        }

        /// <summary>
        /// 获取UI界面资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetUIFormAsset(string assetName)
        {
            return string.Format("Assets/GameMain/UI/UIForms/{0}.prefab", assetName);
        }

        /// <summary>
        /// 获取UI音效资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetUISoundAsset(string assetName)
        {
            return string.Format("Assets/GameMain/UI/UISounds/{0}.wav", assetName);
        }

        /// <summary>
        /// 获取道具图标资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetItemSprite(string assetName)
        {
            return string.Format("Assets/GameMain/UI/UISprites/Items/{0}.PNG", assetName);
        }
        
        /// <summary>
        /// 获取怪物精灵图片资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetMonsterSprite(string assetName)
        {
            return string.Format("Assets/GameMain/Textures/Monsters/{0}.png", assetName);
        }
    }
}
