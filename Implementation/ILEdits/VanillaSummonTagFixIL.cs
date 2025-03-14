using FaeLibrary.API;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.ILEdits {
    internal class VanillaSummonTagFixIL : ModSystem {

        public override void Load() {
            IL_Projectile.Damage += IL_Projectile_Damage;
        }

        private void IL_Projectile_Damage(ILContext il) {
            try {
                // Start the Cursor at the start
                var c = new ILCursor(il);

                // Jump to the first reference to minions AFTER SummonTagDamageMultiplier!
                c.GotoNext(i => i.MatchLdsfld(typeof(ProjectileID.Sets), "SummonTagDamageMultiplier"));
                c.GotoNext(i => i.MatchLdfld<Projectile>("minion"));
                c.EmitDelegate<Func<Projectile, bool>>((proj) => proj.IsMinionOrSentryRelated);
                // Now for the jump instructions . . .
                // I need labels!

                ILLabel trueLabel = null;
                ILLabel falseLabel = null;
                var c2 = new ILCursor(il);
                c2.GotoNext(i => i.MatchLdsfld(typeof(ProjectileID.Sets), "SummonTagDamageMultiplier"));
                c2.GotoNext(i => {
                    bool isBrFalse = i.MatchBrfalse(out ILLabel label);
                    falseLabel = label;
                    return isBrFalse;
                });
                c2.GotoNext(i => {
                    bool isBrTrue = i.MatchBrtrue(out ILLabel label);
                    trueLabel = label;
                    return isBrTrue;
                });
                if (trueLabel != null && falseLabel != null) {
                    c.EmitBrtrue(trueLabel);
                    c.EmitBr(falseLabel);
                }

            } catch (Exception e) {
                // If there are any failures with the IL editing, this method will dump the IL to Logs/ILDumps/{Mod Name}/{Method Name}.txt
                //MonoModHooks.DumpIL(ModContent.GetInstance<FaeLibrary>(), il);

                // If the mod cannot run without the IL hook, throw an exception instead. The exception will call DumpIL internally
                throw new ILPatchFailureException(ModContent.GetInstance<FaeLibrary>(), il, e);
            }
        }

    }
}
