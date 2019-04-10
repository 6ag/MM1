using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 地图组件（目前还不太会利用组件，当做管理类吧）
    /// </summary>
    public class MapComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 当前显示的地图实体
        /// </summary>
        public Map CurrentMap = null;

        /// <summary>
        /// 地图玩家出生位置
        /// </summary>
        private Vector2 m_BornPos = Vector2.one;

        /// <summary>
        /// 地图背景音乐
        /// </summary>
        private int m_BackgroundMusicId = 0;

        /// <summary>
        /// 屏幕遮罩，做地图切换的时候遮挡屏幕
        /// </summary>
        [SerializeField] private Image m_ScreenShade;

        /// <summary>
        /// 地图在世界坐标的位置
        /// </summary>
        private Vector2 m_MapPos;

        /// <summary>
        /// 是否正在切换地图
        /// </summary>
        private bool m_IsChangingMap = false;

        /// <summary>
        /// 切换地图
        /// </summary>
        /// <param name="mapId">新地图ID</param>
        /// <param name="bornPos"></param>
        /// <returns></returns>
        public void ChangeMap(int mapId, Vector2 bornPos)
        {
            if (m_IsChangingMap)
            {
                Log.Debug("正在切换地图，请稍等");
                return;
            }

            m_IsChangingMap = true;

            // 订阅事件
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, ShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, ShowEntityFailure);

            // 根据场景编号获取场景数据表中行数据
            IDataTable<DRMap> dtMap = GameEntry.DataTable.GetDataTable<DRMap>();
            DRMap drMap = dtMap.GetDataRow(mapId);
            if (drMap == null)
            {
                Log.Warning("Can not load map '{0}' from data table.", mapId.ToString());
                return;
            }

            m_BornPos = bornPos;
            m_BackgroundMusicId = drMap.BackgroundMusicId;
            m_MapPos = drMap.Position;

            // 音乐如果不是和当前正在播放的相同才停止
            if (!GameEntry.Sound.CheckPlaying(m_BackgroundMusicId))
            {
                GameEntry.Sound.StopAllLoadingSounds();
                GameEntry.Sound.StopAllLoadedSounds();
            }

            // 隐藏旧地图
            if (CurrentMap != null && CurrentMap.gameObject.activeInHierarchy)
            {
                GameEntry.Entity.HideEntity(CurrentMap);
            }

            // 屏幕变暗，然后屏幕变白
            m_ScreenShade.gameObject.SetActive(true);
            GameEntry.Sound.PlaySound(16);
            m_ScreenShade.DOFade(1f, 0.6f).OnComplete(() =>
            {
                // 加载新地图
                GameEntry.Entity.ShowMap(new MapData(drMap.Id, drMap.NPCs));
                m_ScreenShade.DOFade(0f, 0.6f).OnComplete(() =>
                {
                    m_ScreenShade.gameObject.SetActive(false);
                    m_IsChangingMap = false;
                });
            });
        }

        /// <summary>
        /// 加载实体成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gameEventArgs"></param>
        private void ShowEntitySuccess(object sender, GameEventArgs gameEventArgs)
        {
            var ne = (ShowEntitySuccessEventArgs) gameEventArgs;
            var mapData = ne.UserData as MapData;
            if (mapData == null) return;

            CurrentMap = (Map) ne.Entity.Logic;
            CurrentMap.transform.position = m_MapPos;
            Log.Debug("加载地图成功 mapId" + mapData.Id);

            if (!GameEntry.Sound.CheckPlaying(m_BackgroundMusicId))
            {
                // 播放地图背景音乐
                GameEntry.Sound.PlayMusic(m_BackgroundMusicId);
            }

            // 设置地图事件 比如初始化角色位置 剧情等
            Rebana.GetPlayer().GetComponent<RebanaMovement>().Born(m_BornPos);
            Cliff.GetPlayer().GetComponent<CliffMovement>().Born(m_BornPos);
            Margaret.GetPlayer().GetComponent<MargaretMovement>().Born(m_BornPos);

            Cliff.GetPlayer().GetComponent<CliffMovement>().IsFollowing = true;
            Margaret.GetPlayer().GetComponent<MargaretMovement>().IsFollowing = true;

            // 取消订阅事件
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, ShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, ShowEntityFailure);

            // 派发事件
            GameEntry.Event.Fire(this, ReferencePool.Acquire<ChangeMapSuccessEventArgs>());
        }

        /// <summary>
        /// 加载实体失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="gameEventArgs"></param>
        private void ShowEntityFailure(object sender, GameEventArgs gameEventArgs)
        {
            var ne = (ShowEntityFailureEventArgs) gameEventArgs;
            var mapData = ne.UserData as MapData;
            if (mapData == null) return;

            Log.Error("加载地图失败 mapId" + mapData.Id);

            // 取消订阅事件
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, ShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, ShowEntityFailure);

            // 派发事件
            GameEntry.Event.Fire(this, ReferencePool.Acquire<ChangeMapFailureEventArgs>());
        }
    }
}