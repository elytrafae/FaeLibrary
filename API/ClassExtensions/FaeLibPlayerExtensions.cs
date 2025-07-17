using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.Implementation;
using Terraria.ModLoader;
using Terraria;

namespace FaeLibrary.API.ClassExtensions {
    public static class FaeLibPlayerExtensions {

        public static ref StatModifier GetRangedVelocity(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().RangedVelocity;
        public static ref StatModifier GetSummonSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().SummonSpeed;
        public static ref StatModifier GetSummonTagEffectiveness(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().SummonTagEffectiveness;
        public static ref StatModifier GetWingTimeStat(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().WingTime;
        public static ref StatModifier GetPotionHealingStat(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().PotionHealEffectiveness;
        public static ref StatModifier GetPotionManaStat(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().PotionManaEffectiveness;
        public static ref int GetCommonPositiveRegenStat(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().PositiveRegen;
        public static ref int GetCommonNegativeRegenStat(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().NegativeRegen;
        public static ref bool GetCommonDisabledPositiveRegenStat(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().DisabledPositiveRegen;

        public static ref StatModifier GetItemSizeStat<T>(this Player player) where T : DamageClass => ref player.GetItemSizeStat(ModContent.GetInstance<T>());
        public static ref StatModifier GetItemSizeStat(this Player player, DamageClass dmgClass) => ref player.GetModPlayer<FaeLibPlayer>().ItemSizes[dmgClass.Type];


        public static ref StatModifier GetMountAcceleration(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountAcceleration;
        public static ref StatModifier GetMountDashSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountDashSpeed;
        public static ref StatModifier GetMountRunSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountRunSpeed;
        public static ref StatModifier GetMountJumpHeight(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountJumpHeight;
        public static ref StatModifier GetMountJumpSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountJumpSpeed;
        public static ref StatModifier GetMountFallDamage(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().MountFallDamage;


        // TODO: Continue with other methods
        public static void AddBuff<T>(this Player player, int duration, bool silent = true, bool foodHack = false) where T : ModBuff => player.AddBuff(ModContent.BuffType<T>(), duration, silent, foodHack);

    }
}
