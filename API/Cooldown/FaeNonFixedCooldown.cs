using FaeLibrary.Implementation.Cooldowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeLibrary.API.Cooldown {

    // This one is essentially like using a buff as a cooldown, but not really
    // For simplicity's sake, this cooldown type CANNOT have charges!
    // How to trigger cooldown:
    // cooldown.SetCurrentCooldown(newTicks);
    // cooldown.ConsumeCharge();
    public abstract class FaeNonFixedCooldown : ModType, IFaeCooldown {

        protected sealed override void Register() {

        }

        private int CurrentCooldown = 0;
        private int CooldownTimer = 0;

        public int Charges => 1;
        public int ChargesFilledPerCooldownPeriod => 1;
        public int ChargesFilledWhenEnteringWorld => 1;


        public sealed override void SetupContent() {
            SetStaticDefaults();
        }

        public int CooldownTicks => CurrentCooldown;
        public int DisplayCooldownTicks => CooldownTicks;

        public virtual int GetCooldownTickRate() => 1;
        public virtual void OnGainedCharge() { }

        public bool CanBeUsed() {
            return CooldownTimer <= 0;
        }

        public void SetCurrentCooldown(int cooldown) { 
            CurrentCooldown = cooldown;
        }

        public bool ConsumeCharge() {
            if (!CanBeUsed()) {
                return false;
            }
            CooldownTimer = CurrentCooldown;
            return true;
        }

        public bool IsCoolingDown() {
            return CooldownTimer > 0;
        }

        public void Update() {
            if (!IsCoolingDown()) {
                return;
            }
            CooldownTimer -= GetCooldownTickRate();
            if (CooldownTimer <= 0) {
                OnGainedCharge();
            }
        }

        public int GetCurrentCharges() {
            return CooldownTimer > 0 ? 0 : 1;
        }

        public int GetCurrentCooldownTicks() {
            return CooldownTimer;
        }

        public void CompletelyResetCooldown() {
            CooldownTimer = 0;
        }
    }
}
