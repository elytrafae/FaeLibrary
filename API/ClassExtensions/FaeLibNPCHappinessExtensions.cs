using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.Implementation;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.ModLoader;

namespace FaeLibrary.API.ClassExtensions {
    public static class FaeLibNPCHappinessExtensions {


        public static NPCHappiness SetCustomFaeAffection(this NPCHappiness happiness, IFaeShopPersonalityTrait trait) {
            FaeLibNPCHappinessImplementation.AddCustomPersonalityTrait(happiness, trait);
            return happiness;
        }

    }
}
