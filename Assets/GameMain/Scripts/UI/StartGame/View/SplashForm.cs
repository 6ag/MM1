using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 启动闪屏动画界面
    /// </summary>
    public class SplashForm : UGuiForm
    {
        [SerializeField] private RectTransform m_LogoImage;

        [SerializeField] private RectTransform m_LogoName;

        [SerializeField] private RectTransform m_TextInfo;

        private Text[] m_Texts;

        /// <summary>
        /// 流程
        /// </summary>
        private ProcedureBase m_Procedure;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Procedure = (ProcedureBase) userData;

            // 隐藏Logo名称
            m_LogoName.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            // 隐藏Logo图片
            m_LogoImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            // 隐藏文字信息
            m_Texts = m_TextInfo.GetComponentsInChildren<Text>();
            foreach (Text text in m_Texts)
            {
                text.color = new Color(1, 1, 1, 0);
            }

            // 播放背景音乐
            GameEntry.Sound.PlayMusic(22);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            // 设置Logo名称初始位置
            m_LogoName.Translate(new Vector2(m_LogoName.localPosition.x, m_LogoName.localPosition.y - 180));

            // 渐变出现Logo名称
            m_LogoName.GetComponent<Image>().DOFade(1f, 2f).SetDelay(1f);
            // Logo名称移动到最终位置
            m_LogoName.DOLocalMoveY(99f, 3f).SetDelay(2f).OnComplete(() =>
            {
                // 渐变出现Logo图片
                m_LogoImage.GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() =>
                {
                    // 渐变出现文字版权信息
                    foreach (Text text in m_Texts)
                    {
                        text.DOFade(1f, 2f).SetDelay(1f);
                    }
                });
            });
        }

        protected override void OnButtonClick(GameObject go)
        {
            base.OnButtonClick(go);
            switch (go.name)
            {
                case "BackgroundButton":
                    GameEntry.UI.CloseUIForm(this);
                    GameEntry.UI.OpenUIForm(UIFormId.GameStartForm, m_Procedure);
                    break;
            }
        }
    }
}