using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles {
    public class GreedyRingIngredientShimmerCycle : ShimmerCycle {
        internal override int[] BaseItems => [ItemID.LuckyCoin, ItemID.GoldRing, ItemID.DiscountCard];
    }
}
