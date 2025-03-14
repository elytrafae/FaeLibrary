using FaeLibrary.API.Enums;
using FaeLibrary.Implementation.Cooldowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeLibrary.API.Cooldown
{
    public abstract class FaeSimpleCooldown : ModType, IFaeCooldown {

        protected sealed override void Register() {

        }

        private int CooldownTimer = 0;
        private int CurrentCharges = -1; // Automatically assigned after SetStaticDefaults

        public sealed override void SetupContent() {
            SetStaticDefaults();
            CurrentCharges = ChargesFilledWhenEnteringWorld;
        }

        public abstract int CooldownTicks { get; }
        public abstract int Charges { get; }

        public virtual int DisplayCooldownTicks => CooldownTicks;

        public virtual int ChargesFilledWhenEnteringWorld => Charges;

        public virtual int ChargesFilledPerCooldownPeriod => 1;

        public virtual int GetCooldownTickRate() => 1;
        public virtual void OnGainedCharge() { }

        public bool CanBeUsed() {
            return CurrentCharges > 0;
        }

        public bool ConsumeCharge() {
            if (!CanBeUsed()) {
                return false;
            }
            CurrentCharges--;
            return true;
        }

        public bool IsCoolingDown() {
            return CurrentCharges < Charges;
        }

        public void Update() {
            if (!IsCoolingDown()) {
                return;
            }
            CooldownTimer += GetCooldownTickRate();
            if (CooldownTimer >= CooldownTicks) {
                CurrentCharges += ChargesFilledPerCooldownPeriod;
                OnGainedCharge();
            }
        }

        public int GetCurrentCharges() {
            return CurrentCharges;
        }
    }
}
