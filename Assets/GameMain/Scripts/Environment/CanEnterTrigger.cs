using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace MetalMax
{
	/// <summary>
    /// 可以切换地图的触发器脚本
    /// </summary>
	public class CanEnterTrigger : MonoBehaviour
	{
        /// <summary>
        /// 触发后进入的地图编号
        /// </summary>
	    public int MapId;

        /// <summary>
        /// 触发后进入的地图角色初始位置
        /// </summary>
	    public Vector2 BornPos;

	    private void OnTriggerEnter2D(Collider2D collider)
	    {
            // 除了判断是不是玩家触发，还需要判断是玩家移动触发还是代码设置到触发器上面了
	        if (collider.CompareTag("Player"))
	        {
	            Log.Debug(string.Format("玩家触发进入 name = {0} MapId = {1} Born = {2}", collider.name, MapId, BornPos));
                // 在制作过程中经常有等于0的
                if (MapId != 0)
	            {
	                GameEntry.Map.ChangeMap(MapId, BornPos);
                }
            }
	    }

    }
}
