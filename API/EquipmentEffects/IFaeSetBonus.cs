using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FaeLibrary.API.EquipmentEffects {
    internal interface IFaeSetBonus {

        public EquipmentEffectHolder SetBonusEffects { get; }
        public bool IsArmorSet(Item head, Item body, Item legs);

    }
}
