using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 角色信息装备控制器
    /// </summary>
    public class CharacterInfoController : ControllerBase
    {
        /// <summary>
        /// 角色界面
        /// </summary>
        private CharacterForm m_CharacterForm;

        public CharacterInfoController()
        {
        }

        #region 打开角色界面

        /// <summary>
        /// 打开角色界面
        /// </summary>
        public void OpenCharacterForm()
        {
            if (GameEntry.UI.HasUIForm(UIFormId.CharacterForm)) return;
            GameEntry.UI.OpenUIForm(UIFormId.CharacterForm);

            GameEntry.Controller.Knapsack.OpenKnapsackForm();
        }

        protected override void OpenUIFormSuccessHandler(object sender, GameEventArgs e)
        {
            var args = (OpenUIFormSuccessEventArgs) e;
            if (args.UIForm.Logic as CharacterForm)
            {
                m_CharacterForm = (CharacterForm) args.UIForm.Logic;
            }
        }

        #endregion

        #region 装备穿戴卸下操作

        /// <summary>
        /// 佩戴装备 - 人物和战车公用
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <returns></returns>
        public void WornEquipment(int itemId)
        {
            var itemRow = ItemModel.GetItemRow(itemId);

            switch (itemRow.ItemType)
            {
                case ItemType.HumanEquip:
                    WornHumanEquipment(itemId);
                    break;
                case ItemType.VehicleEquip:
                    WornVehicleEquipment(itemId);
                    break;
            }
        }

        /// <summary>
        /// 脱掉人类装备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool TakeOffHumanEquipment(HumanEquipModel model)
        {
            // 先判断背包能不能放的下
            if (!GameEntry.Controller.Knapsack.KnapsackCapacityEnough(model.Id, 1))
            {
                Log.Debug("背包满了 弹出提示框");
                return false;
            }

            // 卸下身上的装备数据处理
            GameEntry.Archive.Data.GetPlayerData(model.OwerPlayerType).Unequip(model);

            // 卸下身上的装备更新UI
            if (m_CharacterForm != null)
            {
                m_CharacterForm.TakeOffHumanEquipment(model);
            }

            // 卸下的装备放到背包里
            GameEntry.Controller.Knapsack.AddItem(model.Id, 1, GetItemSourceType.Exchange);

            return true;
        }

        /// <summary>
        /// 脱掉战车装备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void TakeOffVehicleEquipment(VehicleEquipModel model)
        {
            Log.Debug(model.Name);
            // 先判断背包能不能放的下
            if (!GameEntry.Controller.Knapsack.KnapsackCapacityEnough(model.Id, 1))
            {
                Log.Debug("背包满了");
                return;
            }

            // 能放下才能卸下

            // 最后卸下装备
        }

        /// <summary>
        /// 佩戴人类装备
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <returns></returns>
        private void WornHumanEquipment(int itemId)
        {
            // 默认操作主角的装备，如果有打开装备界面，就根据当前显示的角色进行操作
            var playerType = PlayerType.Rebana;
            if (m_CharacterForm != null)
            {
                playerType = m_CharacterForm.GetCurrentPlayerType();
            }

            var model = new HumanEquipModel(itemId) {OwerPlayerType = playerType};

            // 佩戴装备更新属性
            var previousModel = GameEntry.Archive.Data.GetPlayerData(playerType).Equip(model);

            // 原先有装备，则把卸下的装备放到背包里
            if (previousModel != null)
            {
                GameEntry.Controller.Knapsack.AddItem(previousModel.Id, 1, GetItemSourceType.Exchange);
            }

            // 更新角色装备界面UI
            if (m_CharacterForm != null)
            {
                m_CharacterForm.WornHumanEquipment(model);
            }
        }

        /// <summary>
        /// 佩戴战车装备
        /// </summary>
        /// <param name="itemId">道具编号</param>
        /// <returns></returns>
        private bool WornVehicleEquipment(int itemId)
        {
            var vehicleEquipRow = ItemModel.GetVehicleEquipRow(itemId);

            return true;
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            m_CharacterForm = null;
        }
    }
}