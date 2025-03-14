using FaeLibrary.API.Enums;
using FaeLibrary.API;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.ILEdits
{
    internal class ProjectileSpeedIL : ModSystem {

        public override void Load() {
            IL_Projectile.Update += IL_Projectile_Update; ;
        }

        private void IL_Projectile_Update(ILContext il) {
            try {
                // Start the Cursor at the start
                var c = new ILCursor(il);
                // Try to find where the minion slot array is read
                c.GotoNext(i => i.MatchLdfld<Projectile>("extraUpdates"));

                // Move the cursor after it
                c.Index++;
                // Push the Projectile instance onto the stack
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0);
                // Call a delegate using the int and Item from the stack.
                c.EmitDelegate<Func<int, Projectile, int>>((numUpdates, projectile) => {
                    // First, we do this:
                    foreach (GlobalProjectile gp in projectile.Globals) {
                        if (gp is IFaeGlobalProjectile faeGlobProj) {
                            faeGlobProj.IgnoreExtraUpdatesBeforeAI(projectile);
                        }
                    }


                    // Player stats!
                    float rangedSpeed = 1f;
                    float summonSpeed = 1f;
                    if (projectile.TryGetOwner(out Player player)) {
                        if (projectile.DamageType.CountsAsClass(DamageClass.Ranged)) {
                            rangedSpeed = player.GetModPlayer<FaeLibPlayer>().RangedVelocity.ApplyTo(1f);
                        }
                        if (projectile.minion || projectile.sentry) {
                            rangedSpeed = player.GetModPlayer<FaeLibPlayer>().SummonSpeed.ApplyTo(1f);
                        }
                    }
                    ////////////////

                    // Actual number of updates is numUpdates + 1!
                    FaeLibProjectile globProj = projectile.GetGlobalProjectile<FaeLibProjectile>();
                    float projectileUpdateRate = summonSpeed * rangedSpeed;
                    float updates = (numUpdates + 1) * projectileUpdateRate;

                    StatModifier modifier = new StatModifier();
                    foreach (GlobalProjectile gp in projectile.Globals) {
                        if (gp is IFaeGlobalProjectile faeGlobProj) {
                            faeGlobProj.ModifyUpdateRate(projectile, ref modifier);
                        }
                    }
                    updates = modifier.ApplyTo(updates);

                    int actualUpdates = (int)updates;
                    globProj.leftoverUpdates += (updates - actualUpdates);
                    int usedExcessUpdates = (int)globProj.leftoverUpdates;
                    globProj.leftoverUpdates -= usedExcessUpdates;
                    return actualUpdates + usedExcessUpdates - 1; // -1 because we added +1 at the beginning!
                });
                // After the delegate, the stack will once again have a float and the ret instruction will return from this method
            } catch (Exception e) {
                // If there are any failures with the IL editing, this method will dump the IL to Logs/ILDumps/{Mod Name}/{Method Name}.txt
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeLibrary>(), il);

                // If the mod cannot run without the IL hook, throw an exception instead. The exception will call DumpIL internally
                // throw new ILPatchFailureException(ModContent.GetInstance<ExampleMod>(), il, e);
            }
        }

    }
}
