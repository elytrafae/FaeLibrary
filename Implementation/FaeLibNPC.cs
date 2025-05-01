using FaeLibrary.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation {
    internal class FaeLibNPC : GlobalNPC {

        public override void UpdateLifeRegen(NPC npc, ref int damage) {
            for (int i = 0; i < NPC.maxBuffs; i++) {
                if (npc.buffTime[i] > 0) { // There is a buff here
                    ModBuff modBuff = ModContent.GetModBuff(npc.buffType[i]);
                    if (modBuff != null && modBuff is IFaeBuff faeBuff) {
                        faeBuff.UpdateNPCLifeRegen(npc, ref i);
                    }
                }
            }
        }

    }
}
