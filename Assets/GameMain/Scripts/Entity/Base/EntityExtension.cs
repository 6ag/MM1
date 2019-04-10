using GameFramework;
using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    public static class EntityExtension
    {
        /// <summary>
        /// 获取游戏实体
        /// </summary>
        /// <param name="entityComponent"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity)entity.Logic;
        }

        /// <summary>
        /// 隐藏实体
        /// </summary>
        /// <param name="entityComponent"></param>
        /// <param name="entity"></param>
        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        /// <summary>
        /// 依附实体
        /// </summary>
        /// <param name="entityComponent"></param>
        /// <param name="entity"></param>
        /// <param name="ownerId"></param>
        /// <param name="parentTransformPath"></param>
        /// <param name="userData"></param>
        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

        /// <summary>
        /// 显示实体
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <param name="logicType">实体类型</param>
        /// <param name="entityGroup">实体组</param>
        /// <param name="priority">优先级</param>
        /// <param name="data">携带的实体数据创建实体后会传给实体</param>
        /// <typeparam name="T">数据表类型</typeparam>
        private static void ShowEntity<T>(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data) where T : IDRAssetsRow
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<T> dtEntity = GameEntry.DataTable.GetDataTable<T>();
            T drEntity = dtEntity.GetDataRow(data.Id);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.Id.ToString());
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority, data);
        }

        /// <summary>
        /// 显示地图实体
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <param name="data">地图实体数据</param>
        public static void ShowMap(this EntityComponent entityComponent, MapData data)
        {
            entityComponent.ShowEntity<DRMap>(typeof(Map), "Map", Constant.AssetPriority.MapAsset, data);
        }
        
        /// <summary>
        /// 显示雷班纳角色实体
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <param name="data">角色实体数据</param>
        public static void ShowPlayerRebana(this EntityComponent entityComponent, PlayerData data)
        {
            entityComponent.ShowEntity<DRPlayer>(typeof(Rebana), "Character", Constant.AssetPriority.CharacterAsset, data);
        }

        /// <summary>
        /// 显示克里夫角色实体
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <param name="data">角色实体数据</param>
        public static void ShowPlayerCliff(this EntityComponent entityComponent, PlayerData data)
        {
            entityComponent.ShowEntity<DRPlayer>(typeof(Cliff), "Character", Constant.AssetPriority.CharacterAsset, data);
        }

        /// <summary>
        /// 显示玛格丽特角色实体
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <param name="data">角色实体数据</param>
        public static void ShowPlayerMargaret(this EntityComponent entityComponent, PlayerData data)
        {
            entityComponent.ShowEntity<DRPlayer>(typeof(Margaret), "Character", Constant.AssetPriority.CharacterAsset, data);
        }

        /// <summary>
        /// 显示NPC实体
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <param name="data">NPC实体数据</param>
        public static void ShowNPC(this EntityComponent entityComponent, NPCData data)
        {
            entityComponent.ShowEntity<DRNPC>(typeof(NPC), "Character", Constant.AssetPriority.CharacterAsset, data);
        }
        
        /// <summary>
        /// 显示怪物实体
        /// </summary>
        /// <param name="entityComponent">实体组件</param>
        /// <param name="data">怪物实体数据</param>
        public static void ShowMonster(this EntityComponent entityComponent, MonsterData data)
        {
            entityComponent.ShowEntity<DRMonster>(typeof(Monster), "Character", Constant.AssetPriority.CharacterAsset, data);
        }
        
 //        /// <summary>
//        /// 显示特效实体
//        /// </summary>
//        /// <param name="entityComponent"></param>
//        /// <param name="data"></param>
//        public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
//        {
//            entityComponent.ShowEntity<>(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset, data);
//        }

    }
}
