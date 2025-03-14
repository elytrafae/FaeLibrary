using FaeLibrary.API.ShimmerCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation {
    internal class ShimmerCycleActivationSystem : ModSystem {

        internal static readonly List<ShimmerCycle> ShimmerCycles = new List<ShimmerCycle>();
        internal static readonly Dictionary<int, ShimmerCycle> WhichShimmerCycleThisItemBelongsTo = new Dictionary<int, ShimmerCycle>();

        MethodInfo ItemGetShimmerEquivalentType = typeof(Item).GetMethod("GetShimmerEquivalentType", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void Load() {
            On_Item.CanShimmer += On_Item_CanShimmer;
        }

        private bool On_Item_CanShimmer(On_Item.orig_CanShimmer orig, Item self) {
            int equivalentType = (int)ItemGetShimmerEquivalentType.Invoke(self, []);//GetShimmerEquivalentType(self.type);
            if (WhichShimmerCycleThisItemBelongsTo.TryGetValue(equivalentType, out ShimmerCycle cycle)) {
                foreach (Condition condition in cycle.Conditions) {
                    if (!condition.Predicate.Invoke()) { // If any of the conditions is false, we deny the shimmer!
                        return false;
                    }
                }
                return true;
            } else {
                return orig(self);
            }
        }

        public override void Unload() {
            ShimmerCycles.Clear();
            WhichShimmerCycleThisItemBelongsTo.Clear();
        }

        public override void PostAddRecipes() {
            for (int i = 0; i < ShimmerCycles.Count; i++) {
                ShimmerCycles[i].ProcessShimmerCycle();
            }
        }

    }
}
