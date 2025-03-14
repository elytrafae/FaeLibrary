using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using FaeLibrary.API;
using FaeLibrary.API.Enums;
using System.Reflection;
using System.Numerics;

namespace FaeLibrary.Implementation.ILEdits {
    internal class OnDodgeIL : ModSystem {

        public override void Load() {
            IL_Player.Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float += IL_Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float;
        }

        private void IL_Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float(ILContext il) {
            ILCursor cursor = new ILCursor(il);
            cursor.GotoNext(i => i.MatchCall(typeof(PlayerLoader).GetMethod("FreeDodge")));
            DodgeType type = 0; // This will be used to iterate through the Dodge Types. The Dodge Types enum is always made in order of the dodges occuring in the code!
            while (cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR8(0), i => i.MatchRet())) {
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);
                //cursor.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_S, (byte)4);// "info");
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Ldarg, 4);
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, (int)type);
                cursor.Emit(Mono.Cecil.Cil.OpCodes.Call, GetType().GetMethod(nameof(OnDodge), BindingFlags.Public | BindingFlags.Static));
                cursor.Index++;
                type++;
            }

        }
        
        public static void OnDodge(Player player, ref Player.HurtInfo hurtInfo, int dodgeType) {
            foreach (ModPlayer modPlayer in player.ModPlayers) {
                if (modPlayer is IFaeModPlayer faeModPlayer) {
                    faeModPlayer.OnDodge(hurtInfo, (DodgeType)dodgeType);
                }
            }
        }
        
    }
}
