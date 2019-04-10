namespace MetalMax
{
    /// <summary>
    /// 版本信息
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// 是否强制版本更新
        /// </summary>
        public bool ForceGameUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// 最新版本
        /// </summary>
        public string LatestGameVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 游戏更新url
        /// </summary>
        public string GameUpdateUrl
        {
            get;
            set;
        }
    }
}
