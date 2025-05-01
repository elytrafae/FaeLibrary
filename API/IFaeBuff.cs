using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.API {
    public interface IFaeBuff {

        public void UpdateLifeRegen(Player player, ref int buffIndex) { }
        public void UpdateBadLifeRegen(Player player, ref int buffIndex) { }
        public void UpdateNPCLifeRegen(NPC npc, ref int buffIndex) { }

    }
}
