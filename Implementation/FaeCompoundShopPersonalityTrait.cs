using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API;
using FaeLibrary.API.ClassExtensions;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;

namespace FaeLibrary.Implementation {
    internal class FaeCompoundShopPersonalityTrait : IShopPersonalityTrait {

        private List<IFaeShopPersonalityTrait> traits = new();

        public void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance) {
            float multiplier = FaeLibNPCHappinessImplementation.GetCurrentPriceAdjustment(shopHelperInstance);
            for (int i = 0; i < traits.Count; i++) {
                traits[i].ModifyShopPrice(info, shopHelperInstance, ref multiplier);
            }
            FaeLibNPCHappinessImplementation.SetCurrentPriceAdjustment(shopHelperInstance, multiplier);
        }

        public void Add(IFaeShopPersonalityTrait trait) { 
            traits.Add(trait);
        }
    }
}
