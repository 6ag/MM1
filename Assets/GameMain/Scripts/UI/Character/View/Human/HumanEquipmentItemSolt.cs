using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetalMax
{
	/// <summary>
    /// 人类装备道具格子
    /// </summary>
	public class HumanEquipmentItemSolt : ViewBase
	{
		/// <summary>
		/// 装备部位类型
		/// </summary>
		public HumanPartType PartType;
		
	    /// <summary>
	    /// 装备格子占位图
	    /// </summary>
	    [SerializeField] private Image m_PlaceholderImage;

	    /// <summary>
	    /// 道具编号
	    /// </summary>
	    private HumanEquipModel m_HumanEquipModel;

	    /// <summary>
	    /// 设置人类装备格子里的道具数据
	    /// </summary>
	    /// <param name="model">人类装备模型</param>
	    public void SetupData(HumanEquipModel model)
	    {
	        m_HumanEquipModel = model;

	        m_PlaceholderImage.gameObject.SetActive(false);

            // 设置道具数据
            var item = GetComponentInChildren<DrawableItemUI>();
	        if (item == null) return;
	        item.SetupData(model.Id);
	    }
        
	    /// <summary>
	    /// 清空格子里的道具
	    /// </summary>
	    public void ClearItemBox()
	    {
		    m_PlaceholderImage.gameObject.SetActive(true);
		    
	        var item = GetComponentInChildren<DrawableItemUI>();
	        if (item == null) return;

            // 如果格子上有道具则销毁
	        m_HumanEquipModel = null;
		    DestroyImmediate(item.gameObject);
	    }

	    protected override void OnButtonClick(GameObject sender)
	    {
	        base.OnButtonClick(sender);
	        if (m_HumanEquipModel == null)
	        {
	            Log.Debug("当前格子为空");
	            return;
	        }
            
            GameEntry.DataNode.SetData<VarInt>(Constant.NodeKey.ItemInfoType, 1);
	        GameEntry.UI.OpenUIForm(UIFormId.ItemInfoForm, m_HumanEquipModel);
	    }
    }
}
