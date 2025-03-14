using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles {
    public class DD2Turret3ShimmerCycle : ShimmerCycle {
        internal override int[] BaseItems => [ItemID.DD2FlameburstTowerT3Popper, ItemID.DD2BallistraTowerT3Popper, ItemID.DD2ExplosiveTrapT3Popper, ItemID.DD2LightningAuraT3Popper];
    }
}
