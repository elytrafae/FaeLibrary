using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeLibrary.API.EquipmentEffects {
    public abstract partial class EquipmentEffect {

        public abstract class AddableIntEquipmentEffect : EquipmentEffect {

            public abstract ref int GetStat(Player player);
            public abstract string GetStatKeyModName();
            public abstract string GetStatKeyName();

            private readonly int value;
            public AddableIntEquipmentEffect(int value) {
                this.value = value;
            }
            public override void EffectUpdate(Player player) {
                GetStat(player) += value;
            }

            public override LocalizedText GetTooltip() {
                string prefix = $"Mods.{GetStatKeyModName()}.EquipmentEffects.{GetStatKeyName()}";
                if (value < 0) {
                    return Language.GetText($"{prefix}Negative").WithFormatArgs(-value);
                } else {
                    return Language.GetText($"{prefix}Positive").WithFormatArgs(value);
                }
            }
        }

        public abstract class AddableFloatEquipmentEffect : EquipmentEffect {

            public abstract ref float GetStat(Player player);
            public abstract string GetStatKeyModName();
            public abstract string GetStatKeyName();

            private readonly float value;
            public AddableFloatEquipmentEffect(float value) {
                this.value = value;
            }
            public override void EffectUpdate(Player player) {
                GetStat(player) += value;
            }

            public override LocalizedText GetTooltip() {
                string prefix = $"Mods.{GetStatKeyModName()}.EquipmentEffects.{GetStatKeyName()}";
                if (value < 0) {
                    return Language.GetText($"{prefix}Negative").WithFormatArgs(-value * 100);
                } else {
                    return Language.GetText($"{prefix}Positive").WithFormatArgs(value * 100);
                }
            }
        }

        public abstract class StatModifierEquipmentEffect : EquipmentEffect {

            public enum Operation {
                BASE,
                ADDITIVE,
                MULTIPLICATIVE,
                FLAT
            }

            public abstract ref StatModifier GetStat(Player player);
            public abstract string GetStatKeyModName();
            public abstract string GetStatKeyName();

            private readonly float value;
            private readonly Operation op;
            public StatModifierEquipmentEffect(float value, Operation op) {
                this.value = value;
                this.op = op;
            }
            public override void EffectUpdate(Player player) {
                switch (op) {
                    case Operation.BASE:
                        GetStat(player).Base += value;
                        break;
                    case Operation.ADDITIVE:
                        GetStat(player) += value;
                        break;
                    case Operation.MULTIPLICATIVE:
                        GetStat(player) *= value;
                        break;
                    case Operation.FLAT:
                        GetStat(player).Flat += value;
                        break;
                }
            }

            public override LocalizedText GetTooltip() {
                string prefix = $"Mods.{GetStatKeyModName()}.EquipmentEffects.{GetStatKeyName()}";
                switch (op) {
                    case Operation.BASE:
                        if (value < 0) {
                            return Language.GetText($"{prefix}.BaseNegative").WithFormatArgs(-value);
                        } else {
                            return Language.GetText($"{prefix}.BasePositive").WithFormatArgs(value);
                        }
                    case Operation.ADDITIVE:
                        if (value < 0) {
                            return Language.GetText($"{prefix}.AdditiveNegative").WithFormatArgs(-value * 100f);
                        } else {
                            return Language.GetText($"{prefix}.AdditivePositive").WithFormatArgs(value * 100f);
                        }
                    case Operation.MULTIPLICATIVE:
                        return Language.GetText($"{prefix}.Multiplicative").WithFormatArgs(value);
                    case Operation.FLAT:
                        if (value < 0) {
                            return Language.GetText($"{prefix}.FlatNegative").WithFormatArgs(-value);
                        } else {
                            return Language.GetText($"{prefix}.FlatPositive").WithFormatArgs(value);
                        }
                }
                return LocalizedText.Empty;
            }
        }

    }
}
