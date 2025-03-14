using FaeLibrary.API.Cooldown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.Cooldowns
{
    internal class FaeCooldownSystem : ModSystem {

        public override void PostUpdatePlayers() {
            foreach (FaeSimpleCooldown cooldown in ModContent.GetContent<FaeSimpleCooldown>()) {
                cooldown.Update();
            }
            foreach (FaeSimultaniousChargeCooldown cooldown in ModContent.GetContent<FaeSimultaniousChargeCooldown>()) {
                cooldown.Update();
            }
        }

    }
}
