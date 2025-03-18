using FaeLibrary.API.Bars.BarExtraDatas;
using FaeLibrary.Implementation;
using FaeLibrary.Implementation.Cooldowns;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FaeLibrary.API.Bars {
    public abstract class FaeSimpleCooldownBar : FaeResourceBar {

        public abstract IFaeCooldown Cooldown { get; }

        public override bool Visible => Cooldown.IsCoolingDown();
        public override float Fullness => Cooldown.IsCoolingDown() ? ((float)Cooldown.GetCurrentCooldownTicks()) / Cooldown.CooldownTicks : 1f;
        public override BarExtraData ExtraData => Cooldown.Charges <= 1 ? null : new TextBarExtraData(MiscLocalizationLines.ChargesText.Format(Cooldown.GetCurrentCharges(), Cooldown.Charges), Cooldown.GetCurrentCharges() < 0 ? Color.Red : Color.Green);
    }
}
