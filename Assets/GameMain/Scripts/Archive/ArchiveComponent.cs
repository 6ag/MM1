using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using iBoxDB.LocalServer;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 游戏存档组件
    /// </summary>
    public class ArchiveComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 游戏存档文件
        /// </summary>
        private string m_GameDataFileName;

        #region 需要存档的游戏数据

        /// <summary>
        /// 需要存档的游戏数据
        /// </summary>
        [SerializeField]
        private GameData m_Data;

        /// <summary>
        /// 需要存档的游戏数据
        /// </summary>
        public GameData Data
        {
            get { return m_Data; }
            private set { m_Data = Data; }
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            Data = new GameData();
            Data.Key = SystemInfo.deviceUniqueIdentifier;

            m_GameDataFileName = Application.persistentDataPath + "/GameData.dat";
        }

        /// <summary>
        /// 存档时调用
        /// </summary>
        public void Save()
        {
            IOHelper.SetData(m_GameDataFileName, Data);
            Log.Debug("更新存档");
        }

        /// <summary>
        /// 读档时调用
        /// </summary>
        /// <returns>是否读取存档成功</returns>
        public bool Load()
        {
            if (IOHelper.IsFileExists(m_GameDataFileName))
            {
                var gameData = IOHelper.GetData(m_GameDataFileName, typeof(GameData)) as GameData;
                if (gameData != null && gameData.Key.Equals(Data.Key))
                {
                    // 合法存档
                    Data = gameData;
                    Log.Debug("读取存档成功，合法存档");
                    return true;
                }
                else
                {
                    // 非法存档
                    Log.Debug("读取存档失败，非法存档");
                    return false;
                }
            }
            else
            {
                Log.Debug("读取存档失败，存档不存在");
                return false;
            }
        }
    }
}