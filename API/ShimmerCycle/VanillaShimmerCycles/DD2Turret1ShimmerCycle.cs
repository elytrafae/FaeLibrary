using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles {
    public class DD2Turret1ShimmerCycle : ShimmerCycle {
        internal override int[] BaseItems => [ItemID.DD2FlameburstTowerT1Popper, ItemID.DD2BallistraTowerT1Popper, ItemID.DD2ExplosiveTrapT1Popper, ItemID.DD2LightningAuraT1Popper];
    }
}
