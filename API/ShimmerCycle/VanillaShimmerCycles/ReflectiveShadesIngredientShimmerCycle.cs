using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeLibrary.API.ShimmerCycle.VanillaShimmerCycles {
    public class ReflectiveShadesIngredientShimmerCycle : ShimmerCycle {
        internal override int[] BaseItems => [ItemID.Blindfold, ItemID.PocketMirror];
    }
}
