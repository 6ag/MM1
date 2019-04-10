using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 没有存档的进入游戏后执行一系列初始化操作
    /// </summary>
    public class NewGame : Game
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            // 订阅事件
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, ShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, ShowEntityFailure);

            // 玩家金币
            GameEntry.Archive.Data.AddGold(10000);

            // 加载角色
            GameEntry.Entity.ShowPlayerRebana(new PlayerData(1, PlayerType.Rebana));
            GameEntry.Entity.ShowPlayerCliff(new PlayerData(2, PlayerType.Cliff));
            GameEntry.Entity.ShowPlayerMargaret(new PlayerData(3, PlayerType.Margaret));

            // 加载地图
            GameEntry.Map.ChangeMap(10001, new Vector2(91.5f, 20.5f));
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 显示实体成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowEntitySuccess(object sender, GameEventArgs e)
        {
            var ne = (ShowEntitySuccessEventArgs) e;
        }

        /// <summary>
        /// 显示实体失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowEntityFailure(object sender, GameEventArgs e)
        {
            var ne = (ShowEntityFailureEventArgs) e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }
    }
}