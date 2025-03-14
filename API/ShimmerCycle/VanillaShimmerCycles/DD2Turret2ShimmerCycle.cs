using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles {
    public class DD2Turret2ShimmerCycle : ShimmerCycle {
        internal override int[] BaseItems => [ItemID.DD2FlameburstTowerT2Popper, ItemID.DD2BallistraTowerT2Popper, ItemID.DD2ExplosiveTrapT2Popper, ItemID.DD2LightningAuraT2Popper];
    }
}
