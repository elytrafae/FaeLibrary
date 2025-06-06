using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.Implementation;
using Terraria;

namespace FaeLibrary.API.ClassExtensions {
    public static class FaeLibProjectileExtensions {

        public static bool TryGetSourceNPC(this Projectile projectile, out NPC npc) => projectile.GetGlobalProjectile<FaeLibProjectile>().TryGetSourceNPC(out npc);

    }
}
