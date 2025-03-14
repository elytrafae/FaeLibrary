using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation
{
    internal class FaeLibProjectile : GlobalProjectile
    {

        public override bool InstancePerEntity => true;

        public float leftoverUpdates = 0; // NOT A STAT!

    }
}
