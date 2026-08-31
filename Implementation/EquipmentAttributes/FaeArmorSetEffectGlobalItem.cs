using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API.EquipmentEffects;
using Steamworks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.EquipmentAttributes {
    internal class FaeArmorSetEffectGlobalItem : GlobalItem {

        private static EquipmentEffectHolder headHolder = null;
        private static EquipmentEffectHolder bodyHolder = null;
        private static EquipmentEffectHolder legsHolder = null;

        public override string IsArmorSet(Item head, Item body, Item legs) {
            headHolder = IsArmorSetHelper(head, head, body, legs);
            bodyHolder = IsArmorSetHelper(body, head, body, legs);
            legsHolder = IsArmorSetHelper(legs, head, body, legs);
            return "hello haiiii :3";
        }

        public override void UpdateArmorSet(Player player, string set) {
            string armorSetText = "";
            armorSetText += headHolder?.GetTooltip();
            armorSetText += bodyHolder?.GetTooltip();
            armorSetText += legsHolder?.GetTooltip();
            if (player.setBonus != "") {
                player.setBonus = "\n" + player.setBonus;
            }
            player.setBonus = armorSetText + player.setBonus;

            headHolder?.UpdateAll(player);
            bodyHolder?.UpdateAll(player);
            legsHolder?.UpdateAll(player);
        }

        private EquipmentEffectHolder IsArmorSetHelper(Item current, Item head, Item body, Item legs) {
            if (current.ModItem is IFaeSetBonus setBonus && setBonus.IsArmorSet(head, body, legs) && current.TryGetGlobalItem(out FaeEquipmentEffectGlobalItem effect)) {
                effect.CheckCachedHolders(current.ModItem);
                return effect.setHolder;
            }
            return null;
        }

    }
}
