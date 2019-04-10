using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// NPC数据表
    /// </summary>
	public class DRNPC : IDRAssetsRow 
	{
		public int Id { get; private set; }
		
		/// <summary>
		/// 资源名称
		/// </summary>
		public string AssetName { get; private set; }
		
		/// <summary>
		/// NPC类型
		/// </summary>
		public NPCType NpcType { get; private set; }
		
		/// <summary>
		/// 商店编号
		/// </summary>
		public int StoreId { get; private set; }
		
		/// <summary>
		/// 初始位置
		/// </summary>
		public Vector3 BornPosition { get; private set; } 
		
		/// <summary>
		/// 初始动画名称
		/// </summary>
		public string BornAnimation { get; private set; }
		
		/// <summary>
		/// 对话名称
		/// </summary>
		public string Conversation { get; private set; }
		
		/// <summary>
		/// 是否可以巡逻
		/// </summary>
		public bool Patrol { get; private set; }
		
		public void ParseDataRow(string dataRowText)
		{
			string[] text = DataTableExtension.SplitDataRow(dataRowText);
			int index = 0;
			index++;
			Id = int.Parse(text[index++]);
			index++;
			AssetName = text[index++];
			NpcType = (NPCType) int.Parse(text[index++]);
			StoreId = int.Parse(text[index++]);
			BornPosition = SplitPosition(text[index++]);
			BornAnimation = text[index++];
			Conversation = text[index++];
			Patrol = bool.Parse(text[index++]);
		}
		
		/// <summary>
		/// 分割位置信息字符串
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private Vector3 SplitPosition(string position)
		{
			string[] text = DataTableExtension.SplitValue(position);
			return new Vector3(float.Parse(text[0]), float.Parse(text[1]), float.Parse(text[2]));
		}
		
	}
}

