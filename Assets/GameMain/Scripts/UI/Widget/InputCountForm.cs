using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 输入数量界面
    /// </summary>
    public class InputCountForm : UGuiForm
    {
        /// <summary>
        /// 输入数量的操作类型
        /// </summary>
        public enum InputCountType
        {
            /// <summary>
            /// 出售
            /// </summary>
            Sell = 1,

            /// <summary>
            /// 购买
            /// </summary>
            Buy = 2,

            /// <summary>
            /// 丢弃
            /// </summary>
            Discard = 3
        }

        /// <summary>
        /// 输入框
        /// </summary>
        [SerializeField] private InputField m_InputField;

        /// <summary>
        /// 道具模型
        /// </summary>
        private KnapsackModel m_KnapsackModel;

        /// <summary>
        /// 商品模型
        /// </summary>
        private ShopModel m_ShopModel;

        /// <summary>
        /// 输入数量的操作类型
        /// </summary>
        private InputCountType m_Type;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_InputField.onValidateInput = OnValidateInput;
            m_InputField.text = "1";

            m_Type = (InputCountType) (int) GameEntry.DataNode.GetData<VarInt>(Constant.NodeKey.InputCountType);
            GameEntry.DataNode.RemoveNode(Constant.NodeKey.InputCountType);
            switch (m_Type)
            {
                case InputCountType.Sell:
                case InputCountType.Discard:
                    m_KnapsackModel = (KnapsackModel) userData;
                    break;
                case InputCountType.Buy:
                    m_ShopModel = (ShopModel) userData;
                    break;
            }
        }

        /// <summary>
        /// 验证文本框输入
        /// </summary>
        /// <param name="text"></param>
        /// <param name="charindex"></param>
        /// <param name="addedchar"></param>
        /// <returns></returns>
        private char OnValidateInput(string text, int charindex, char addedchar)
        {
            if (m_KnapsackModel == null)
            {
                return addedchar;
            }

            var count = int.Parse(text + addedchar);
            switch (m_Type)
            {
                case InputCountType.Sell:
                case InputCountType.Discard:
                    if (count > m_KnapsackModel.CurrentStackCount)
                    {
                        return '\0';
                    }

                    break;
                case InputCountType.Buy:

                    break;
            }

            return addedchar;
        }

        protected override void OnClose(object userData)
        {
            base.OnClose(userData);
            m_InputField.text = string.Empty;
            m_ShopModel = null;
            m_KnapsackModel = null;
        }

        protected override void OnButtonClick(GameObject sender)
        {
            base.OnButtonClick(sender);

            var count = 1;
            if (m_InputField.text.Length > 0)
            {
                count = int.Parse(m_InputField.text);
            }

            switch (sender.name)
            {
                case "FuncButton1": // 确定

                    switch (m_Type)
                    {
                        case InputCountType.Sell:
                        case InputCountType.Discard:
                            if (count > 0 && count <= m_KnapsackModel.CurrentStackCount)
                            {
                                GameEntry.Controller.Knapsack.SellItem(m_KnapsackModel, count);
                                GameEntry.UI.CloseUIForm(this);
                            }
                            else
                            {
                                Log.Debug("身上数量不够，拿命来凑");
                            }

                            break;
                        case InputCountType.Buy:
                            if (count > 0)
                            {
                                GameEntry.Controller.Shop.BuyItem(m_ShopModel, count);
                                GameEntry.UI.CloseUIForm(this);
                            }

                            break;
                    }

                    break;
                case "FuncButton2": // 取消
                    GameEntry.UI.CloseUIForm(this);
                    break;
                case "MinusButton": // -
                    if (count > 1)
                    {
                        m_InputField.text = (count - 1).ToString();
                    }

                    break;
                case "PlusButton": // +
                    switch (m_Type)
                    {
                        case InputCountType.Sell:
                        case InputCountType.Discard:
                            if (count < m_KnapsackModel.CurrentStackCount)
                            {
                                m_InputField.text = (count + 1).ToString();
                            }

                            break;
                        case InputCountType.Buy:
                            if (count < 99)
                            {
                                m_InputField.text = (count + 1).ToString();
                            }

                            break;
                    }

                    break;
            }
        }
    }
}