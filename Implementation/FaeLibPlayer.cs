using FaeLibrary.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation {
    // One shouldn't have to reference this directly!
    // Everything this ModPlayer does will be exposed through the regular Player class!
    internal partial class FaeLibPlayer : ModPlayer {

        public StatModifier RangedVelocity = new();
        public StatModifier SummonSpeed = new();
        public StatModifier SummonTagEffectiveness = new();
        public StatModifier WingTime = new();
        public StatModifier PotionHealEffectiveness = new();
        public StatModifier PotionManaEffectiveness = new();
        public int PositiveRegen = 0;
        public int NegativeRegen = 0;
        public bool DisabledPositiveRegen = false;

        public StatModifier[] ItemSizes = [];
        

        public override void ResetEffects() {
            RangedVelocity = new();
            SummonSpeed = new();
            SummonTagEffectiveness = new();
            WingTime = new();
            PotionHealEffectiveness = new();
            PotionManaEffectiveness = new();
            PositiveRegen = 0;
            NegativeRegen = 0;
            DisabledPositiveRegen = false;
            ResetDamageClassRelatedEffects();
            ResetMountStatBuffs();
        }

        private void ResetDamageClassRelatedEffects() {
            ItemSizes = new StatModifier[DamageClassLoader.DamageClassCount];
            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++) {
                ItemSizes[i] = new StatModifier();
            }
        }

        public override void PostUpdateMiscEffects() {
            Player.wingTimeMax = (int)WingTime.ApplyTo(Player.wingTimeMax);

            // Rocket flight time, for some reason, is not actually reset, so I cannot do this.
            // Rocket time stacking with flight time is also kinda weird the moment you try to add flat/base time...
            //Player.rocketTimeMax = (int)FlightTime.ApplyTo(Player.rocketTimeMax);
        }

        public override void UpdateLifeRegen() {
            Player.lifeRegen += PositiveRegen;
            for (int i = 0; i < Player.MaxBuffs; i++) {
                if (Player.buffTime[i] > 0) { // There is a buff here
                    ModBuff modBuff = ModContent.GetModBuff(Player.buffType[i]);
                    if (modBuff != null && modBuff is IFaeBuff faeBuff) {
                        faeBuff.UpdateLifeRegen(Player, ref i);
                    }
                }
            }

            // Since this is called after update misc effects, I think this is the best spot for this...
            UpdateMountStats();
        }

        public override void UpdateBadLifeRegen() {
            if (DisabledPositiveRegen && Player.lifeRegen > 0) {
                Player.lifeRegen = 0;
            }
            Player.lifeRegen -= NegativeRegen;
            for (int i = 0; i < Player.MaxBuffs; i++) {
                if (Player.buffTime[i] > 0) { // There is a buff here
                    ModBuff modBuff = ModContent.GetModBuff(Player.buffType[i]);
                    if (modBuff != null && modBuff is IFaeBuff faeBuff) {
                        faeBuff.UpdateBadLifeRegen(Player, ref i);
                    }
                }
            }
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue) {
            healValue = (int)PotionHealEffectiveness.ApplyTo(healValue);
        }

        public override void GetHealMana(Item item, bool quickHeal, ref int healValue) {
            healValue = (int)PotionManaEffectiveness.ApplyTo(healValue);
        }

        public override void PostUpdate() {
            ResetMountStats();
        }


    }

}
