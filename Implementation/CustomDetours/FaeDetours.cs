using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.CustomDetours {
    public class FaeDetours : ModSystem {

        private Hook fairySpawnChanceHook = null;

        public override void Load() {
            //fairySpawnChanceHook = new Hook();
        }

        public override void Unload() {
            base.Unload();
        }

    }
}
