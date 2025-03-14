using FaeLibrary.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaeLibrary.Implementation.Cooldowns
{
    internal interface IFaeCooldown {

        public abstract int CooldownTicks { get; }
        public abstract int DisplayCooldownTicks { get; }
        public abstract int Charges { get; }
        public abstract int ChargesFilledPerCooldownPeriod { get; }
        public abstract int ChargesFilledWhenEnteringWorld { get; }


        public abstract int GetCooldownTickRate();
        public abstract void Update();
        public abstract bool CanBeUsed();
        public abstract bool IsCoolingDown();
        public abstract bool ConsumeCharge();
        public abstract void OnGainedCharge();
        public abstract int GetCurrentCharges();

    }
}
