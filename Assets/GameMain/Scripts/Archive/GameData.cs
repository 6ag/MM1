using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 游戏存档数据
    /// </summary>
    [Serializable]
    public class GameData
    {
        #region 秘钥

        /// <summary>
        /// 秘钥，防止拷贝存档
        /// </summary>
        [SerializeField] private string m_Key;

        /// <summary>
        /// 秘钥，防止拷贝存档
        /// </summary>
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        #endregion

        #region 队伍所有玩家信息

        /// <summary>
        /// 队伍所有玩家信息(装备信息也在PlayerData里面)
        /// </summary>
        [SerializeField] private List<PlayerData> m_PlayerDatas;

        /// <summary>
        /// 队伍所有玩家信息(装备信息也在PlayerData里面)
        /// </summary>
        public List<PlayerData> PlayerDatas
        {
            get { return m_PlayerDatas; }
            private set { m_PlayerDatas = value; }
        }

        #endregion

        #region 玩家背包中道具数据

        /// <summary>
        /// 玩家背包中道具数据 背包格子数量固定 集合每个元素
        /// </summary>
        [SerializeField] private List<KnapsackModel> m_KnapsackModels;

        /// <summary>
        /// 玩家背包中道具数据 背包格子数量固定 集合每个元素
        /// </summary>
        public List<KnapsackModel> KnapsackModels
        {
            get { return m_KnapsackModels; }
            private set { m_KnapsackModels = value; }
        }

        #endregion

        #region 玩家金币

        /// <summary>
        /// 玩家金币
        /// </summary>
        [SerializeField] private int m_Gold;

        /// <summary>
        /// 玩家金币
        /// </summary>
        public int Gold
        {
            get { return m_Gold; }
            private set { m_Gold = value; }
        }

        #endregion

        public GameData()
        {
            PlayerDatas = new List<PlayerData>();
            KnapsackModels = new List<KnapsackModel>();
        }

        #region 玩家数据操作

        /// <summary>
        /// 设置玩家数据
        /// </summary>
        /// <param name="playerData"></param>
        public void SetPlayerData(PlayerData playerData)
        {
            for (var i = 0; i < PlayerDatas.Count; i++)
            {
                if (PlayerDatas[i].PlayerType == playerData.PlayerType)
                {
                    PlayerDatas.RemoveAt(i);
                    break;
                }
            }

            PlayerDatas.Add(playerData);
        }

        /// <summary>
        /// 获取玩家数据
        /// </summary>
        /// <param name="playerType">玩家类型</param>
        /// <returns></returns>
        public PlayerData GetPlayerData(PlayerType playerType)
        {
            foreach (var data in PlayerDatas)
            {
                if (data.PlayerType == playerType)
                {
                    return data;
                }
            }

            return null;
        }

        #endregion

        #region 金币操作

        /// <summary>
        /// 增加金币
        /// </summary>
        /// <param name="count"></param>
        public void AddGold(int count)
        {
            Gold += count;
        }

        /// <summary>
        /// 减少金币
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool ReduceGold(int count)
        {
            if (Gold >= count)
            {
                Gold -= count;
                return true;
            }

            return false;
        }

        #endregion
    }
}