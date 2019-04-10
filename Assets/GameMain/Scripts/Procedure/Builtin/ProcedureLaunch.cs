﻿using GameFramework;
using GameFramework.Localization;
using System;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace MetalMax
{
    /// <summary>
    /// 游戏启动流程(游戏开始第一个流程，入口流程)
    /// </summary>
    public class ProcedureLaunch : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // 构建信息：发布版本时，把一些数据以 Json 的格式写入 Assets/GameMain/Configs/BuildInfo.txt，供游戏逻辑读取。
            GameEntry.BuiltinData.InitBuildInfo();

            // 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言。
            InitLanguageSettings();

            // 变体配置：根据使用的语言，通知底层加载对应的资源变体。
//            InitCurrentVariant();

            // 画质配置：根据检测到的硬件信息 Assets/Main/Configs/DeviceModelConfig 和用户配置数据，设置即将使用的画质选项。
            InitQualitySettings();

            // 声音配置：根据用户配置数据，设置即将使用的声音选项。
            InitSoundSettings();

            // 默认字典：加载默认字典文件 Assets/GameMain/Configs/DefaultDictionary.xml。
            // 此字典文件记录了资源更新前使用的各种语言的字符串，会随 App 一起发布，故不可更新。
            GameEntry.BuiltinData.InitDefaultDictionary();
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // 切换到闪屏动画流程
            ChangeState<ProcedureSplash>(procedureOwner);
        }

        #region 初始化游戏各种配置

        /// <summary>
        /// 初始化语言设置
        /// </summary>
        private void InitLanguageSettings()
        {
            if (GameEntry.Base.EditorResourceMode && GameEntry.Base.EditorLanguage != GameFramework.Localization.Language.Unspecified)
            {
                // 编辑器资源模式直接使用 Inspector 上设置的语言
                return;
            }

            GameFramework.Localization.Language language = GameEntry.Localization.Language;
            string languageString = GameEntry.Setting.GetString(Constant.Setting.Language);
            if (!string.IsNullOrEmpty(languageString))
            {
                try
                {
                    language = (GameFramework.Localization.Language)Enum.Parse(typeof(GameFramework.Localization.Language), languageString);
                }
                catch
                {

                }
            }

            if (language != GameFramework.Localization.Language.English
                && language != GameFramework.Localization.Language.ChineseSimplified
                && language != GameFramework.Localization.Language.ChineseTraditional
                && language != GameFramework.Localization.Language.Korean)
            {
                // 若是暂不支持的语言，则使用英语
                language = GameFramework.Localization.Language.English;

                GameEntry.Setting.SetString(Constant.Setting.Language, language.ToString());
                GameEntry.Setting.Save();
            }

            GameEntry.Localization.Language = language;

            Log.Info("Init language settings complete, current language is '{0}'.", language.ToString());
        }

        /// <summary>
        /// 初始化变体
        /// </summary>
        private void InitCurrentVariant()
        {
            if (GameEntry.Base.EditorResourceMode)
            {
                // 编辑器资源模式不使用 AssetBundle，也就没有变体了
                return;
            }

            string currentVariant = null;
            switch (GameEntry.Localization.Language)
            {
                case GameFramework.Localization.Language.English:
                    currentVariant = "en-us";
                    break;
                case GameFramework.Localization.Language.ChineseSimplified:
                    currentVariant = "zh-cn";
                    break;
                case GameFramework.Localization.Language.ChineseTraditional:
                    currentVariant = "zh-tw";
                    break;
                case GameFramework.Localization.Language.Korean:
                    currentVariant = "ko-kr";
                    break;
                default:
                    currentVariant = "zh-cn";
                    break;
            }

            GameEntry.Resource.SetCurrentVariant(currentVariant);

            Log.Info("Init current variant complete.");
        }

        /// <summary>
        /// 初始化画质设置
        /// </summary>
        private void InitQualitySettings()
        {
            QualityLevelType defaultQuality = GameEntry.BuiltinData.DeviceModelConfig.GetDefaultQualityLevel();
            int qualityLevel = GameEntry.Setting.GetInt(Constant.Setting.QualityLevel, (int)defaultQuality);
            QualitySettings.SetQualityLevel(qualityLevel, true);

            Log.Info("Init quality settings complete.");
        }

        /// <summary>
        /// 初始化声音设置
        /// </summary>
        private void InitSoundSettings()
        {
            GameEntry.Sound.Mute("Music", GameEntry.Setting.GetBool(Constant.Setting.MusicMuted, false));
            GameEntry.Sound.SetVolume("Music", GameEntry.Setting.GetFloat(Constant.Setting.MusicVolume, 1f));
            GameEntry.Sound.Mute("Sound", GameEntry.Setting.GetBool(Constant.Setting.SoundMuted, false));
            GameEntry.Sound.SetVolume("Sound", GameEntry.Setting.GetFloat(Constant.Setting.SoundVolume, 1f));
            GameEntry.Sound.Mute("UISound", GameEntry.Setting.GetBool(Constant.Setting.UISoundMuted, false));
            GameEntry.Sound.SetVolume("UISound", GameEntry.Setting.GetFloat(Constant.Setting.UISoundVolume, 1f));

            Log.Info("Init sound settings complete.");
        }
    }

    #endregion

}
