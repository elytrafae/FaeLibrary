using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation {
    internal partial class FaeLibPlayer {

        private readonly Mount.MountData origMountData = new();
        private int baseMountType = -1;

        public StatModifier MountAcceleration = new();
        public StatModifier MountDashSpeed = new();
        public StatModifier MountRunSpeed = new();
        public StatModifier MountJumpHeight = new();
        public StatModifier MountJumpSpeed = new();
        public StatModifier MountFallDamage = new();

        private void UpdateMountStats() {
            if (Player.mount == null || !Player.mount.Active) {
                return;
            }
            ResetMountStats(); // Just to be sure

            // Make backup of original stats
            baseMountType = Player.mount.Type;
            CopyRelevantMountStats(origMountData, Mount.mounts[baseMountType]);

            Mount.MountData mount = Mount.mounts[baseMountType];

            ApplyTo(ref mount.acceleration, MountAcceleration);
            ApplyTo(ref mount.dashSpeed, MountDashSpeed);
            ApplyTo(ref mount.runSpeed, MountRunSpeed);
            ApplyTo(ref mount.jumpHeight, MountJumpHeight);
            ApplyTo(ref mount.jumpSpeed, MountJumpSpeed);
            ApplyTo(ref mount.fallDamage, MountFallDamage);
        }

        private static void CopyRelevantMountStats(Mount.MountData to, Mount.MountData from) { 
            to.acceleration = from.acceleration;
            to.dashSpeed = from.dashSpeed;
            to.runSpeed = from.runSpeed;
            to.jumpHeight = from.jumpHeight;
            to.jumpSpeed = from.jumpSpeed;
            to.fallDamage = from.fallDamage;
        }

        private void ResetMountStats() {
            if (origMountData == null || baseMountType < 0 || baseMountType >= Mount.mounts.Length) {
                return;
            }

            CopyRelevantMountStats(Mount.mounts[baseMountType], origMountData);
            baseMountType = -1;
        }

        private void ResetMountStatBuffs() {
            MountAcceleration = new();
            MountDashSpeed = new();
            MountRunSpeed = new();
            MountJumpHeight = new();
            MountJumpSpeed = new();
            MountFallDamage = new();
        }

        static private void ApplyTo(ref float var, StatModifier modif) {
            var = modif.ApplyTo(var);
        }

        static private void ApplyTo(ref int var, StatModifier modif) {
            var = (int)modif.ApplyTo(var);
        }

    }
}
