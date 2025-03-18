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
    internal class FaeLibPlayer : ModPlayer {

        public StatModifier RangedVelocity = new();
        public StatModifier SummonSpeed = new();
        public StatModifier SummonTagEffectiveness = new();

        public override void ResetEffects() {
            RangedVelocity = new();
            SummonSpeed = new();
            SummonTagEffectiveness = new();
        }

    }

    public static class FaeLibPlayerExtensions {

        public static ref StatModifier GetRangedVelocity(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().RangedVelocity;
        public static ref StatModifier GetSummonSpeed(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().SummonSpeed;
        public static ref StatModifier GetSummonTagEffectiveness(this Player player) => ref player.GetModPlayer<FaeLibPlayer>().SummonTagEffectiveness;

    }
}
