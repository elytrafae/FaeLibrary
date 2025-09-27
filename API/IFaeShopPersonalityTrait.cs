using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API.ClassExtensions;
using FaeLibrary.Implementation;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;

namespace FaeLibrary.API {

    // Not an actual Shop Personality Trait, but will be called by one
    public interface IFaeShopPersonalityTrait {

        void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance, ref float priceMultiplier);

        public static void AddHappinessReportText(ShopHelper shopHelperInstance, string textKeyInCategory, object substitutes = null, int otherNPCType = 0) {
            FaeLibNPCHappinessImplementation.AddHappinessReportText(shopHelperInstance, textKeyInCategory, substitutes, otherNPCType);
        }
    }
}
