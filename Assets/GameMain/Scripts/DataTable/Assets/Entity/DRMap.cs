using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 地图数据表
    /// </summary>
	public class DRMap : IDRAssetsRow 
	{	
		public int Id { get; private set; }
		
		/// <summary>
		/// 资源名称
		/// </summary>
		public string AssetName { get; private set; }
		
		/// <summary>
		/// 地图的世界坐标(为了处理拼接地图，不拼接的都是0,0,0)
		/// </summary>
		public Vector3 Position { get; private set; }

        /// <summary>
        /// 地图背景音乐
        /// </summary>
        public int BackgroundMusicId { get; private set; }

		/// <summary>
		/// 地图上的NPC编号列表
		/// </summary>
		public List<int> NPCs { get; private set; }
		
        public void ParseDataRow(string dataRowText)
		{
			string[] text = DataTableExtension.SplitDataRow(dataRowText);
			int index = 0;
			index++;
			Id = int.Parse(text[index++]);
			index++;
			AssetName = text[index++];
			Position = SplitPosition(text[index++]);
		    BackgroundMusicId = int.Parse(text[index++]);
			NPCs = SplitNPCs(text[index++]);
		}
		
		/// <summary>
		/// 分割NPC编号字符串
		/// </summary>
		/// <param name="npcs"></param>
		/// <returns></returns>
		private List<int> SplitNPCs(string npcs)
		{
			string[] text = DataTableExtension.SplitValue(npcs);
			List<int> npcList = new List<int>();
			foreach (var s in text)
			{
				npcList.Add(int.Parse(s));
			}

			return npcList;
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

		private void AvoidJIT()
		{
			new Dictionary<int, DRMusic>();
		}
	}
}

