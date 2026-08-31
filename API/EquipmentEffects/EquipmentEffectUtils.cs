using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeLibrary.API.EquipmentEffects {
    public static class EquipmentEffectUtils {

        public static void RegisterStatModifierEquipmentLines(string modName, string statKey, string englishStatName) {
            string prefix = $"Mods.{modName}.EquipmentEffects.{statKey}";
            Language.GetOrRegister($"{prefix}.BasePositive", () => "Increases base " + englishStatName + " by {0}");
            Language.GetOrRegister($"{prefix}.BaseNegative", () => "Decreases base " + englishStatName + " by {0}");
            Language.GetOrRegister($"{prefix}.AdditivePositive", () => "{0}% increased " + englishStatName);
            Language.GetOrRegister($"{prefix}.AdditiveNegative", () => "{0}% decreased " + englishStatName);
            Language.GetOrRegister($"{prefix}.Multiplicative", () => "Multiplies " + englishStatName + " by {0}x");
            Language.GetOrRegister($"{prefix}.FlatPositive", () => "Increases " + englishStatName + " by a flat {0}");
            Language.GetOrRegister($"{prefix}.FlatNegative", () => "Decreases " + englishStatName + " by a flat {0}");
        }

        public static void RegisterDamageClassEquipmentLines(DamageClass dmgClass, string englishDamageTypeName) { 
            string modName = dmgClass.Mod == null ? "FaeLibrary" : dmgClass.Mod.Name;
            RegisterStatModifierEquipmentLines(modName, $"{dmgClass.Name}.Damage", $"{englishDamageTypeName} damage".Trim());
            RegisterStatModifierEquipmentLines(modName, $"{dmgClass.Name}.Knockback", $"{englishDamageTypeName} knockback".Trim());

            string prefix = $"Mods.{modName}.EquipmentEffects.{dmgClass.Name}";
            Language.GetOrRegister($"{prefix}.AttackSpeedPositive", () => "{0}% increased " + $"{englishDamageTypeName} attack speed".Trim());
            Language.GetOrRegister($"{prefix}.AttackSpeedNegative", () => "{0}% decreased " + $"{englishDamageTypeName} attack speed".Trim());
            Language.GetOrRegister($"{prefix}.ArmorPenetrationPositive", () => "{0}% increased " + $"{englishDamageTypeName} armor penetration".Trim());
            Language.GetOrRegister($"{prefix}.ArmorPenetrationNegative", () => "{0}% decreased " + $"{englishDamageTypeName} armor penetration".Trim());
            Language.GetOrRegister($"{prefix}.CritChancePositive", () => "{0}% increased " + $"{englishDamageTypeName} crit chance".Trim());
            Language.GetOrRegister($"{prefix}.CritChanceNegative", () => "{0}% decreased " + $"{englishDamageTypeName} crit chance".Trim());
        }

    }
}
