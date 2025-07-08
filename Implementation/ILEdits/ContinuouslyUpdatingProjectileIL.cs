using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoMod.Cil;
using Terraria.ModLoader;
using Terraria;
using FaeLibrary.API;
using Terraria.WorldBuilding;

namespace FaeLibrary.Implementation.ILEdits {
    internal class ContinuouslyUpdatingProjectileIL : ModSystem {

        public override void Load() {
            IL_Projectile.Update += IL_Projectile_Update;
        }

        private void IL_Projectile_Update(MonoMod.Cil.ILContext il) {
            try {
                // Start the Cursor at the start
                var c = new ILCursor(il);
                // Find where ContinuouslyUpdateDamageStats is read
                c.GotoNext(i => i.MatchCall<Projectile>("get_ContinuouslyUpdateDamageStats"));

                // Jump to where we get the damage stat modifier
                c.GotoNext(i => i.MatchCallvirt<Player>("GetTotalDamage"));
                c.Index++;

                // Add player to the stack
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc_S, (byte)4);
                // Add the projectile to the stack
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);

                c.EmitDelegate((StatModifier stat, Player player, Projectile proj) => {
                    foreach (GlobalProjectile gp in proj.Globals) {
                        if (gp is IFaeGlobalProjectile faeGlobProj) {
                            faeGlobProj.ModifyContinuouslyUpdatingDamage(proj, player, ref stat);
                        }
                    }
                    return stat;
                });


                // Jump to where we get the crit chance
                c.GotoNext(i => i.MatchCallvirt<Player>("GetTotalCritChance"));
                c.Index++;

                // Add player to the stack
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc_S, (byte)4);
                // Add the projectile to the stack
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);

                c.EmitDelegate((float crit, Player player, Projectile proj) => {
                    foreach (GlobalProjectile gp in proj.Globals) {
                        if (gp is IFaeGlobalProjectile faeGlobProj) {
                            faeGlobProj.ModifyContinuouslyUpdatingCritChance(proj, player, ref crit);
                        }
                    }
                    return crit;
                });


                // Jump to where we get the armor penetration
                c.GotoNext(i => i.MatchCallvirt<Player>("GetTotalArmorPenetration"));
                c.Index++;

                // Add player to the stack
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc_S, (byte)4);
                // Add the projectile to the stack
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);

                c.EmitDelegate((float armorpen, Player player, Projectile proj) => {
                    foreach (GlobalProjectile gp in proj.Globals) {
                        if (gp is IFaeGlobalProjectile faeGlobProj) {
                            faeGlobProj.ModifyContinuouslyUpdatingArmorPenetration(proj, player, ref armorpen);
                        }
                    }
                    return armorpen;
                });

            } catch (Exception e) {
                // If there are any failures with the IL editing, this method will dump the IL to Logs/ILDumps/{Mod Name}/{Method Name}.txt
                //MonoModHooks.DumpIL(ModContent.GetInstance<GLACIER>(), il);

                // If the mod cannot run without the IL hook, throw an exception instead. The exception will call DumpIL internally
                throw new ILPatchFailureException(ModContent.GetInstance<FaeLibrary>(), il, e);
            }
        }
    }
}
