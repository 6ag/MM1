namespace MetalMax
{
    /// <summary>
    /// 构建信息
    /// </summary>
    public class BuildInfo
    {
        /// <summary>
        /// 游戏版本
        /// </summary>
        public string GameVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 游戏内部版本
        /// </summary>
        public int InternalGameVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 检查版本更新的url
        /// </summary>
        public string CheckVersionUrl
        {
            get;
            set;
        }
    }
}
