using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FaeLibrary.API;
using MonoMod.RuntimeDetour;
using System.Reflection;

namespace FaeLibrary.Implementation.Detours {
    internal class SummonTagEffectivenessDetour : ModSystem {

        private static Hook isMinionOrSentryRelatedHook;

        public override void Load() {
            On_Projectile.Damage += On_Projectile_Damage;

            Type projectileType = typeof(Projectile);
            PropertyInfo propInfo = projectileType.GetProperty("IsMinionOrSentryRelated", BindingFlags.Public | BindingFlags.Instance);
            isMinionOrSentryRelatedHook = new Hook(propInfo.GetGetMethod(), IsMinionOrSentryRelated_Detour, applyByDefault: false);
            isMinionOrSentryRelatedHook.Apply();
        }

        public override void Unload() {
            isMinionOrSentryRelatedHook?.Dispose();
            isMinionOrSentryRelatedHook = null;
        }

        private void On_Projectile_Damage(On_Projectile.orig_Damage orig, Projectile self) {
            if (self.IsMinionOrSentryRelated) {
                float oldNr = ProjectileID.Sets.SummonTagDamageMultiplier[self.type];
                StatModifier modifier = new StatModifier();
                foreach (GlobalProjectile gp in self.Globals) {
                    if (gp is IFaeGlobalProjectile faeGlobProj) {
                        faeGlobProj.ModifySummonTagEffectveness(self, ref modifier);
                    }
                }

                ProjectileID.Sets.SummonTagDamageMultiplier[self.type] = modifier.ApplyTo(oldNr);
                try {
                    orig(self);
                } catch (Exception e) {
                    Console.Error.WriteLine("Error while applying summon tag effectiveness changes: {}", e);
                }
                ProjectileID.Sets.SummonTagDamageMultiplier[self.type] = oldNr;
            } else {
                orig(self);
            }
        }

        private bool IsMinionOrSentryRelated_Detour(Func<Projectile, bool> orig, Projectile self) {
            foreach (GlobalProjectile gp in self.Globals) {
                if (gp is IFaeGlobalProjectile faeGlobProj) {
                    bool? whatThisGlobalProjSays = faeGlobProj.CanBenefitFromSummonTagBonuses(self);
                    if (whatThisGlobalProjSays.HasValue) {
                        return whatThisGlobalProjSays.Value;
                    }
                }
            }
            return orig(self);
        }

    }
}
