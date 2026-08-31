using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API.EquipmentEffects;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.EquipmentAttributes {
    internal class FaeEquipmentHelperSystem : ModSystem {

        public override void SetStaticDefaults() {
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.Default, "default");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.Generic, "");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.Melee, "melee");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.MeleeNoSpeed, "melee");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.Ranged, "ranged");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.Magic, "magic");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.Summon, "summon");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.SummonMeleeSpeed, "whip");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.MagicSummonHybrid, "magic-summon");
            EquipmentEffectUtils.RegisterDamageClassEquipmentLines(DamageClass.Throwing, "throwing");
        }

    }
}
