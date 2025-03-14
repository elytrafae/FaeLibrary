using FaeLibrary.API.ShimmerCycle;
using FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeLibrary.CrossModCompat {
    internal class Calamity : ModSystem {

        public override bool IsLoadingEnabled(Mod mod) {
            return ModLoader.HasMod("CalamityMod");
        }

        public override void PostSetupRecipes() {
            Mod calamity = ModLoader.GetMod("Calamity");
            if (calamity.TryFind("RogueEmblem", out ModItem rogueEmblem)) {
                ShimmerCycle.GetInstance<ClassEmblemShimmerCycle>().TryInsertAfter(ItemID.SummonerEmblem, rogueEmblem.Type);
            }

            // Calamity's Pyramid Loot Edits.
            // The "Mask" cycle will be the one for the Sandstorm in a Bottle, Flying Carpet and Amber Hook.
            // The "Robe" cycle will be the one for the Pharao's Set
            ShimmerCycle maskCycle = ShimmerCycle.GetInstance<SandstormInABottleShimmerCycle>();
            maskCycle.DisableBaseItem(ItemID.PharaohsMask);

            ShimmerCycle robeCycle = ShimmerCycle.GetInstance<FlyingCarpetShimmerCycle>();
            robeCycle.DisableBaseItem(ItemID.FlyingCarpet);
            robeCycle.TryInsertAfter(ItemID.FlyingCarpet, ItemID.PharaohsMask);

            maskCycle.TryInsertAfter(ItemID.PharaohsMask, ItemID.FlyingCarpet);
            maskCycle.TryInsertBefore(ItemID.SandstorminaBottle, ItemID.AmberHook);
        }

    }
}
