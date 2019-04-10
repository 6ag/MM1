using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 商店窗口
    /// </summary>
	public class ShopForm : UGuiForm
	{
		/// <summary>
		/// 可滑动的商品列表视图
		/// </summary>
        [SerializeField]
	    private ShopListView m_ShopListView;

        /// <summary>
        /// 商品模型集合
        /// </summary>
	    private List<ShopModel> m_shopModels;

        protected override void OnInit(object userData)
	    {
	        base.OnInit(userData);
	    }

	    protected override void OnOpen(object userData)
	    {
	        base.OnOpen(userData);
		    m_shopModels = (List<ShopModel>) userData;
		    
		    m_ShopListView.SetupData(m_shopModels);
	    }

	    protected override void OnClose(object userData)
	    {
	        base.OnClose(userData);
		    m_ShopListView.Clear();
	    }

	    protected override void OnButtonClick(GameObject go)
	    {
	        base.OnButtonClick(go);
	        switch (go.name)
	        {
                case "CloseButton":
                    GameEntry.UI.CloseUIForm(this);
					GameEntry.Controller.Knapsack.CloseKnapsackForm();
                    break;
	        }
	    }
	}
}
