using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MetalMax
{
    /// <summary>
    /// 人类头像列表集合视图
    /// </summary>
    public class HumanAvatarListView : ViewBase
    {
        /// <summary>
        /// 点击了角色头像
        /// </summary>
        public UnityAction<PlayerType> OnAvatarClick;

        /// <summary>
        /// 头像格子集合
        /// </summary>
        public List<HumanAvatarSolt> AvatarSolts;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnButtonClick(GameObject sender)
        {
            base.OnButtonClick(sender);

            foreach (var avatarSolt in AvatarSolts)
            {
                avatarSolt.GetComponent<Image>().enabled = false;
            }

            sender.GetComponent<Image>().enabled = true;

            if (OnAvatarClick != null)
            {
                OnAvatarClick(sender.GetComponent<HumanAvatarSolt>().PlayerType);
            }
        }
    }
}