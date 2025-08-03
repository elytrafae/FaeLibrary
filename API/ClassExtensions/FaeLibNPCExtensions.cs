using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.API.ClassExtensions {
    public static class FaeLibNPCExtensions {

        public static void AddBuff<T>(this NPC npc, int duration, bool silent = false) where T : ModBuff => npc.AddBuff(ModContent.BuffType<T>(), duration, silent);

    }
}
