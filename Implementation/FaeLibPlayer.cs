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

        public override void ResetEffects() {
            RangedVelocity = new();
            SummonSpeed = new();
            SummonTagEffectiveness = new();
            ResetMountStatBuffs();
        }

        public override void UpdateLifeRegen() {
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
            for (int i = 0; i < Player.MaxBuffs; i++) {
                if (Player.buffTime[i] > 0) { // There is a buff here
                    ModBuff modBuff = ModContent.GetModBuff(Player.buffType[i]);
                    if (modBuff != null && modBuff is IFaeBuff faeBuff) {
                        faeBuff.UpdateBadLifeRegen(Player, ref i);
                    }
                }
            }
        }

        public override void PostUpdate() {
            ResetMountStats();
        }


    }

    public static class FaeLibPlayerExtensions {

        public static ref StatModifier GetRangedVelocity(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().RangedVelocity;
        public static ref StatModifier GetSummonSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().SummonSpeed;
        public static ref StatModifier GetSummonTagEffectiveness(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().SummonTagEffectiveness;


        public static ref StatModifier GetMountAcceleration(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountAcceleration;
        public static ref StatModifier GetMountDashSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountDashSpeed;
        public static ref StatModifier GetMountRunSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountRunSpeed;
        public static ref StatModifier GetMountJumpHeight(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountJumpHeight;
        public static ref StatModifier GetMountJumpSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountJumpSpeed;
        public static ref StatModifier GetMountFallDamage(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountFallDamage;

    }
}
