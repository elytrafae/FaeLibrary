using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;

namespace FaeLibrary.API.EquipmentEffects {
    public abstract partial class EquipmentEffect {
        public abstract void EffectUpdate(Player player);
        public abstract LocalizedText GetTooltip();
    }
}
