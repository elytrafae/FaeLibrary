using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles {
    public class HerbBagCanOfWormsShimmerCycle : ShimmerCycle {
        internal override int[] BaseItems => [ItemID.HerbBag, ItemID.CanOfWorms];
    }
}
