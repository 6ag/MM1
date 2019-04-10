using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 人类属性界面
    /// </summary>
    public class HumanAttributeView : ViewBase
    {
        #region 属性字段

        /// <summary>
        /// 名字
        /// </summary>
        [SerializeField] private Text m_NameText;

        /// <summary>
        /// 血量文字
        /// </summary>
        [SerializeField] private Text m_HPText;

        /// <summary>
        /// 血量进度
        /// </summary>
        [SerializeField] private Image m_HPFillImage;

        /// <summary>
        /// 经验文字
        /// </summary>
        [SerializeField] private Text m_ExpText;

        /// <summary>
        /// 经验进度
        /// </summary>
        [SerializeField] private Image m_ExpFillImage;

        /// <summary>
        /// 人物等级
        /// </summary>
        [SerializeField] private Text m_LevelText;

        /// <summary>
        /// 战斗等级
        /// </summary>
        [SerializeField] private Text m_CombatLevelText;

        /// <summary>
        /// 修理等级
        /// </summary>
        [SerializeField] private Text m_RepairLevelText;

        /// <summary>
        /// 驾驶等级
        /// </summary>
        [SerializeField] private Text m_DriveLevelText;

        /// <summary>
        /// 攻击
        /// </summary>
        [SerializeField] private Text m_AttackText;

        /// <summary>
        /// 防御
        /// </summary>
        [SerializeField] private Text m_DefenseText;

        /// <summary>
        /// 强度
        /// </summary>
        [SerializeField] private Text m_StrengthText;

        /// <summary>
        /// 智力
        /// </summary>
        [SerializeField] private Text m_IntelligenceText;

        /// <summary>
        /// 速度
        /// </summary>
        [SerializeField] private Text m_SpeedText;

        /// <summary>
        /// 体力
        /// </summary>
        [SerializeField] private Text m_PhysicalPowerText;

        #endregion

        /// <summary>
        /// 设置角色属性
        /// </summary>
        public void SetupRoleAttribute(PlayerData playerData)
        {
            m_NameText.text = playerData.Name;
            m_HPText.text = string.Format("{0}/{1}", playerData.CurrentAttribute.HP, playerData.TotalAttribute.HP);
            m_HPFillImage.fillAmount = playerData.CurrentAttribute.HP * 1.0f / playerData.TotalAttribute.HP;
            m_LevelText.text = playerData.Level.ToString();
            m_ExpText.text = string.Format("{0}/{1}", playerData.Exp, playerData.NextExp); // 这里经验要去表里查
            m_ExpFillImage.fillAmount = playerData.Exp * 1.0f / playerData.NextExp;
            m_CombatLevelText.text = playerData.CurrentAttribute.CombatLevel.ToString();
            m_AttackText.text = playerData.CurrentAttribute.Attack.ToString();
            m_RepairLevelText.text = playerData.CurrentAttribute.RepairLevel.ToString();
            m_DefenseText.text = playerData.CurrentAttribute.Defence.ToString();
            m_DriveLevelText.text = playerData.CurrentAttribute.DriveLevel.ToString();
            m_StrengthText.text = playerData.CurrentAttribute.Strength.ToString();
            m_IntelligenceText.text = playerData.CurrentAttribute.Intelligence.ToString();
            m_SpeedText.text = playerData.CurrentAttribute.Speed.ToString();
            m_PhysicalPowerText.text = playerData.CurrentAttribute.PhysicalPower.ToString();
        }
    }
}