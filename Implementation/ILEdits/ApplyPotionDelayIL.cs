using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API;
using FaeLibrary.API.Enums;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.ILEdits {
    internal class ApplyPotionDelayIL : ModSystem {

        public override void Load() {
            IL_Player.ApplyPotionDelay += IL_Player_ApplyPotionDelay;
        }

        private void IL_Player_ApplyPotionDelay(ILContext il) {
            ILCursor cursor = new ILCursor(il);
            // So, the Strange Brew's code assigns to "potionDelay" twice.
            // In order to make things work, I have decided to just skip the second instance of potionDelay being assigned.
            // There is probably a bigger brained solution, but I don't know...
            int i = 0;
            while (cursor.TryGotoNext(MoveType.After, i => i.MatchStfld<Player>("potionDelay"))) {
                if (i == 1) { // Skip second instance, as mentioned above
                    i++;
                    continue;
                }
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_1);
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Call, GetType().GetMethod(nameof(ModifyPotionDelay), BindingFlags.Public | BindingFlags.Static));
                cursor.Index++;
                i++;
            }
        }

        public static void ModifyPotionDelay(Player player, Item item) {
            if (item.ModItem is IFaeModItem faeModItem) { 
                faeModItem.ModifyPotionDelay(player, ref player.potionDelay);
            }
            foreach (GlobalItem globalItem in item.Globals) {
                if (globalItem is IFaeGlobalItem faeGlobalItem) {
                    faeGlobalItem.ModifyPotionDelay(player, item, ref player.potionDelay);
                }
            }
            foreach (ModPlayer modPlayer in player.ModPlayers) {
                if (modPlayer is IFaeModPlayer faeModPlayer) {
                    faeModPlayer.ModifyPotionDelay(item, ref player.potionDelay);
                }
            }
        }

    }
}
