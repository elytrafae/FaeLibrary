using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API;
using FaeLibrary.API.EquipmentEffects;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.EquipmentAttributes {
    internal class FaeEquipmentEffectGlobalItem : GlobalItem {

        private bool alreadyFetchedHolders = false;
        private EquipmentEffectHolder? accessoryHolder = null;
        private EquipmentEffectHolder? armorHolder = null;
        internal EquipmentEffectHolder? setHolder = null;

        private static LocalizedText IsWornAsAccessory;
        private static LocalizedText IsWornAsArmor;

        public override void SetStaticDefaults() {
            IsWornAsAccessory = Mod.GetLocalization("EquipmentEffectSystem.IsAccessory");
            IsWornAsArmor = Mod.GetLocalization("EquipmentEffectSystem.IsArmor");
        }

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
            return entity.ModItem is IFaeAccessory or IFaeArmor or IFaeSetBonus;
        }

        internal void CheckCachedHolders(ModItem item) {
            if (alreadyFetchedHolders && (!Debugger.IsAttached)) {
                return;
            }
            if (item is IFaeAccessory accessory) {
                accessoryHolder = accessory.AccessoryEffects;
            }
            if (item is IFaeArmor armor) {
                armorHolder = armor.ArmorEffects;
            }
            if (item is IFaeSetBonus set) {
                setHolder = set.SetBonusEffects;
            }
            alreadyFetchedHolders = true;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual) {
            CheckCachedHolders(item.ModItem);
            if (accessoryHolder != null) {
                foreach (var effect in accessoryHolder.GetAll()) {
                    effect.EffectUpdate(player);
                }
            }
        }

        public override void UpdateEquip(Item item, Player player) {
            CheckCachedHolders(item.ModItem);
            if (armorHolder != null) {
                foreach (var effect in armorHolder.GetAll()) {
                    effect.EffectUpdate(player);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            CheckCachedHolders(item.ModItem);
            string tooltip = "";
            if (armorHolder != null && accessoryHolder != null) {
                tooltip += armorHolder.GetTooltip() + "\n";
                tooltip += IsWornAsAccessory.Value + "\n";
                tooltip += accessoryHolder.GetTooltip();
            } else if (armorHolder != null) {
                tooltip = armorHolder.GetTooltip();
            } else if (accessoryHolder != null) { 
                tooltip = accessoryHolder.GetTooltip();
            }

            TooltipLine line = new TooltipLine(Mod, "EquipmentEffectTooltip", tooltip);
            FaeUtils.AddTooltipLine(tooltips, line, VanillaTooltip.Tooltip, true);
        }

    }
}
