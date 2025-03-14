using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles {
    public class MiningShirtPantsShimmerCycle : ShimmerCycle {
        internal override int[] BaseItems => [ItemID.MiningShirt, ItemID.MiningPants];
    }
}
