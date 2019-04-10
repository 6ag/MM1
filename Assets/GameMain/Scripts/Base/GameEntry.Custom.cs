using GameFramework;

namespace MetalMax
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry
    {
        /// <summary>
        /// 内建数据组件
        /// </summary>
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        public static HPBarComponent HPBar
        {
            get;
            private set;
        }

        /// <summary>
        /// 存档组件
        /// </summary>
        public static ArchiveComponent Archive
        {
            get;
            private set;
        }

        /// <summary>
        /// 地图组件
        /// </summary>
        public static MapComponent Map
        {
            get;
            private set;
        }

        /// <summary>
        /// 控制器组件
        /// </summary>
        public static ControllerComponent Controller
        {
            get; 
            private set;
        }

        /// <summary>
        /// 战斗系统组件
        /// </summary>
        public static CombatSystemComponent CombatSystem
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始化自定义组件
        /// </summary>
        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            HPBar = UnityGameFramework.Runtime.GameEntry.GetComponent<HPBarComponent>();
            Archive = UnityGameFramework.Runtime.GameEntry.GetComponent<ArchiveComponent>();
            Map = UnityGameFramework.Runtime.GameEntry.GetComponent<MapComponent>();
            Controller = UnityGameFramework.Runtime.GameEntry.GetComponent<ControllerComponent>();
            CombatSystem = UnityGameFramework.Runtime.GameEntry.GetComponent<CombatSystemComponent>();
        }
    }
}
