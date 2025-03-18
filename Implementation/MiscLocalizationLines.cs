using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation {
    internal class MiscLocalizationLines : ModSystem {

        public static LocalizedText ChargesText { get; private set; }

        public override void SetStaticDefaults() {
            ChargesText = Mod.GetLocalization(nameof(ChargesText));
        }

    }
}
