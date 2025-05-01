using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation
{
    internal class FaeLibProjectile : GlobalProjectile {

        public override bool InstancePerEntity => true;

        private int SourceNPC = -1;
        public float leftoverUpdates = 0; // NOT A STAT!

        public override void OnSpawn(Projectile projectile, IEntitySource source) {
            FaeLibProjectile faeProj = projectile.GetGlobalProjectile<FaeLibProjectile>(); 
            if (source is EntitySource_Parent parent) {
                if (parent.Entity is NPC npc) {
                    faeProj.SourceNPC = npc.whoAmI;
                } else if (parent.Entity is Projectile proj) {
                    FaeLibProjectile parentFaeProj = proj.GetGlobalProjectile<FaeLibProjectile>();
                    faeProj.SourceNPC = parentFaeProj.SourceNPC;
                }
            }
        }

        public bool TryGetSourceNPC(out NPC npc) {
            if (SourceNPC > -1) {
                npc = Main.npc[SourceNPC];
                return true;
            }
            npc = null;
            return false;
        }

    }

    public static class FaeLibProjectileExtensions {

        public static bool TryGetSourceNPC(this Projectile projectile, out NPC npc) => projectile.GetGlobalProjectile<FaeLibProjectile>().TryGetSourceNPC(out npc);

    }
}
