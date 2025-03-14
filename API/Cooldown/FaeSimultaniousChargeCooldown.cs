using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.Implementation.Cooldowns;
using Terraria.ModLoader;

namespace FaeLibrary.API.Cooldown
{
    public abstract class FaeSimultaniousChargeCooldown : ModType, IFaeCooldown {

        protected sealed override void Register() {

        }

        private List<int> CooldownTimers;

        public sealed override void SetupContent() {
            SetStaticDefaults();
            CooldownTimers = new List<int>(Charges);
            int chargesToBeFilled = Math.Min(ChargesFilledWhenEnteringWorld, Charges);
            for (int i = 0; i < chargesToBeFilled; i++) {
                CooldownTimers[i] = CooldownTicks;
            }
            for (int i = chargesToBeFilled; i < Charges; i++) {
                CooldownTimers[i] = 0;
            }
        }

        public abstract int CooldownTicks { get; }
        public abstract int Charges { get; }

        public virtual int DisplayCooldownTicks => CooldownTicks;

        public virtual int ChargesFilledWhenEnteringWorld => Charges;

        public virtual int ChargesFilledPerCooldownPeriod => 1;

        public virtual int GetCooldownTickRate() => 1;
        public virtual void OnGainedCharge() { }

        public bool CanBeUsed() {
            for (int i = 0; i < Charges; i++) {
                if (CooldownTimers[i] >= CooldownTicks) {
                    return true;
                }
            }
            return false;
        }

        public bool ConsumeCharge() {
            for (int i = 0; i < Charges; i++) {
                if (CooldownTimers[i] >= CooldownTicks) {
                    CooldownTimers[i] -= CooldownTicks;
                    return true;
                }
            }
            return false;
        }

        public bool IsCoolingDown() {
            for (int i = 0; i < Charges; i++) {
                if (CooldownTimers[i] < CooldownTicks) {
                    return true;
                }
            }
            return false;
        }

        public void Update() {
            int cooldownRate = GetCooldownTickRate();
            for (int i = 0; i < Charges; i++) {
                if (CooldownTimers[i] < CooldownTicks) {
                    CooldownTimers[i] += cooldownRate;
                    if (CooldownTimers[i] >= CooldownTicks) {
                        OnGainedCharge();
                    }
                }
            }
        }

        public int GetCurrentCharges() {
            int count = 0;
            for (int i = 0; i < Charges; i++) {
                if (CooldownTimers[i] >= CooldownTicks) {
                    count += 1;
                }
            }
            return count;
        }
    }
}
