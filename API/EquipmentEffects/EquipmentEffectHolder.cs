using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReLogic.Peripherals.RGB;
using Terraria;
using Terraria.Localization;

namespace FaeLibrary.API.EquipmentEffects {
    internal class EquipmentEffectHolder {

        private List<EquipmentEffect> effects = new();
        private List<LocalizedText> cachedText = new();

        public EquipmentEffectHolder() { 
            
        }

        public EquipmentEffectHolder Add(EquipmentEffect effect) {
            effects.Add(effect);
            return this;
        }

        public EquipmentEffectHolder Remove(EquipmentEffect effect) {
            effects.Remove(effect);
            return this;
        }

        public IReadOnlyList<EquipmentEffect> GetAll() {
            return effects;
        }

        public void UpdateAll(Player player) {
            foreach (var effect in effects) {
                effect.EffectUpdate(player);
            }
        }

        public string GetTooltip() {
            if (cachedText.Count <= 0) {
                cachedText = [.. effects.Select(e => e.GetTooltip())];
            }
            return String.Join("\n", cachedText.Select(e => e.Value));
        }

    }
}
