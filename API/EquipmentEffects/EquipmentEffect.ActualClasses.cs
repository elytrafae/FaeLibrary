using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API.ClassExtensions;
using FaeLibrary.Implementation;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeLibrary.API.EquipmentEffects {
    public abstract partial class EquipmentEffect {

        #region DAMAGE
        public class Damage : StatModifierEquipmentEffect {
            private readonly DamageClass dmgClass;

            public Damage(float value, Operation op, DamageClass dmgClass) : base(value, op) {
                this.dmgClass = dmgClass;
            }

            public override ref StatModifier GetStat(Player player) {
                return ref player.GetDamage(dmgClass);
            }

            public override string GetStatKeyModName() {
                return dmgClass.Mod == null ? nameof(FaeLibrary) : dmgClass.Mod.Name;
            }

            public override string GetStatKeyName() {
                return $"{dmgClass.Name}.Damage";
            }
        }

        public class Knockback : StatModifierEquipmentEffect {
            private readonly DamageClass dmgClass;

            public Knockback(float value, Operation op, DamageClass dmgClass) : base(value, op) {
                this.dmgClass = dmgClass;
            }

            public override ref StatModifier GetStat(Player player) {
                return ref player.GetKnockback(dmgClass);
            }

            public override string GetStatKeyModName() {
                return dmgClass.Mod == null ? nameof(FaeLibrary) : dmgClass.Mod.Name;
            }

            public override string GetStatKeyName() {
                return $"{dmgClass.Name}.Knockback";
            }
        }

        public class CritChance : AddableFloatEquipmentEffect {
            private readonly DamageClass dmgClass;

            public CritChance(float value, DamageClass dmgClass) : base(value) {
                this.dmgClass = dmgClass;
            }

            public override ref float GetStat(Player player) {
                return ref player.GetCritChance(dmgClass);
            }

            public override string GetStatKeyModName() {
                return dmgClass.Mod == null ? nameof(FaeLibrary) : dmgClass.Mod.Name;
            }

            public override string GetStatKeyName() {
                return $"{dmgClass.Name}.CritChance";
            }
        }

        public class AttackSpeed : AddableFloatEquipmentEffect {
            private readonly DamageClass dmgClass;

            public AttackSpeed(float value, DamageClass dmgClass) : base(value) {
                this.dmgClass = dmgClass;
            }

            public override ref float GetStat(Player player) {
                return ref player.GetAttackSpeed(dmgClass);
            }

            public override string GetStatKeyModName() {
                return dmgClass.Mod == null ? nameof(FaeLibrary) : dmgClass.Mod.Name;
            }

            public override string GetStatKeyName() {
                return $"{dmgClass.Name}.AttackSpeed";
            }
        }

        public class ArmorPenetration : AddableFloatEquipmentEffect {
            private readonly DamageClass dmgClass;

            public ArmorPenetration(int value, DamageClass dmgClass) : base(value) {
                this.dmgClass = dmgClass;
            }

            public override ref float GetStat(Player player) {
                return ref player.GetArmorPenetration(dmgClass);
            }

            public override string GetStatKeyModName() {
                return dmgClass.Mod == null ? nameof(FaeLibrary) : dmgClass.Mod.Name;
            }

            public override string GetStatKeyName() {
                return $"{dmgClass.Name}.ArmorPenetration";
            }
        }
        #endregion

        #region SURVIVAL
        public class MaxHealth : StatModifierEquipmentEffect {
            public MaxHealth(float value, Operation op) : base(value, op) {
            }

            public override ref StatModifier GetStat(Player player) {
                return ref player.GetMaxHealthStat();
            }

            public override string GetStatKeyModName() {
                return "FaeLibrary";
            }

            public override string GetStatKeyName() {
                return "MaxHealth";
            }
        }
        #endregion

        #region CLASS_GIMMICKS
        public class MaxMana : StatModifierEquipmentEffect {
            public MaxMana(float value, Operation op) : base(value, op) {
            }

            public override ref StatModifier GetStat(Player player) {
                return ref player.GetMaxManaStat();
            }

            public override string GetStatKeyModName() {
                return "FaeLibrary";
            }

            public override string GetStatKeyName() {
                return "MaxMana";
            }
        }
        #endregion

        #region MOVEMENT
        public class MovementSpeed : AddableFloatEquipmentEffect {
            public MovementSpeed(float value) : base(value) {
            }

            public override ref float GetStat(Player player) {
                return ref player.moveSpeed;
            }

            public override string GetStatKeyModName() {
                return "FaeLibrary";
            }

            public override string GetStatKeyName() {
                return "MovementSpeed";
            }
        }
        #endregion




    }
}
