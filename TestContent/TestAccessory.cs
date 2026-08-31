using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API.EquipmentEffects;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static FaeLibrary.API.EquipmentEffects.EquipmentEffect.StatModifierEquipmentEffect;

namespace FaeLibrary.TestContent {
    internal class TestAccessory : ModItem, IFaeAccessory, IFaeArmor, IFaeSetBonus {
        public EquipmentEffectHolder AccessoryEffects => new EquipmentEffectHolder()
            .Add(new EquipmentEffect.MaxHealth(30, Operation.BASE))
            .Add(new EquipmentEffect.Damage(10f, Operation.BASE, DamageClass.Melee))
            .Add(new EquipmentEffect.Damage(-10f, Operation.BASE, DamageClass.MeleeNoSpeed))
            .Add(new EquipmentEffect.Damage(1f, Operation.ADDITIVE, DamageClass.Generic))
            .Add(new EquipmentEffect.Damage(-1f, Operation.ADDITIVE, DamageClass.Ranged))
            .Add(new EquipmentEffect.Damage(2.5f, Operation.MULTIPLICATIVE, DamageClass.Magic))
            .Add(new EquipmentEffect.Damage(30f, Operation.FLAT, DamageClass.Summon))
            .Add(new EquipmentEffect.Damage(-30f, Operation.FLAT, DamageClass.SummonMeleeSpeed));

        public EquipmentEffectHolder ArmorEffects => new EquipmentEffectHolder()
            .Add(new EquipmentEffect.MaxHealth(0.5f, Operation.ADDITIVE));

        public EquipmentEffectHolder SetBonusEffects => new EquipmentEffectHolder()
            .Add(new EquipmentEffect.MaxMana(0f, Operation.MULTIPLICATIVE))
            .Add(new EquipmentEffect.MovementSpeed(2.23456f));

        bool IFaeSetBonus.IsArmorSet(Item head, Item body, Item legs) {
            return body.type == ItemID.TikiShirt && legs.type == ItemID.TikiPants;
        }

        public override void SetDefaults() {
            Item.DefaultToAccessory();
            Item.headSlot = ArmorIDs.Head.AncientArmor;
        }
    }
}
