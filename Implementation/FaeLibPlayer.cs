using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
