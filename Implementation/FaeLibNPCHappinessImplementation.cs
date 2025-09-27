using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation {
    internal class FaeLibNPCHappinessImplementation {

        private static readonly FieldInfo _database;
        private static readonly FieldInfo _currentPriceAdjustment;
        private static readonly MethodInfo AddHappinessReportTextMethod;

        static FaeLibNPCHappinessImplementation() {
            Type ShopHelperType = typeof(ShopHelper);
            _database = ShopHelperType.GetField(nameof(_database), BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new MemberAccessException(nameof(_database) + " not found! Critical error!");
            _currentPriceAdjustment = ShopHelperType.GetField(nameof(_currentPriceAdjustment), BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new MemberAccessException(nameof(_currentPriceAdjustment) + " not found! Critical error!");
            AddHappinessReportTextMethod = ShopHelperType.GetMethod(nameof(AddHappinessReportText), BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new MethodAccessException(nameof(AddHappinessReportTextMethod) + " not found! Critical error!");
        }

        internal static PersonalityDatabase GetPersonalityDatabase() {
            return GetPersonalityDatabase(Main.ShopHelper);
        }

        internal static PersonalityDatabase GetPersonalityDatabase(ShopHelper helper) {
            return (PersonalityDatabase)_database.GetValue(helper);
        }

        internal static float GetCurrentPriceAdjustment(ShopHelper helper) {
            return (float)_currentPriceAdjustment.GetValue(helper);
        }

        internal static void SetCurrentPriceAdjustment(ShopHelper helper, float value) {
            _currentPriceAdjustment.SetValue(helper, value);
        }

        internal static void AddHappinessReportText(ShopHelper helper, string textKeyInCategory, object substitutes = null, int otherNPCType = 0) {
            AddHappinessReportTextMethod.Invoke(helper, [textKeyInCategory, substitutes, otherNPCType]);
        }

        public static void AddCustomPersonalityTrait(NPCHappiness happiness, IFaeShopPersonalityTrait trait) {
            var profile = GetPersonalityDatabase().GetOrCreateProfileByNPCID(happiness.NpcType);
            var shopModifiers = profile.ShopModifiers;

            IShopPersonalityTrait compound = shopModifiers.Find(personality => personality is FaeCompoundShopPersonalityTrait);
            if (compound == null) {
                compound = new FaeCompoundShopPersonalityTrait();
                shopModifiers.Add(compound);
            }
            
            ((FaeCompoundShopPersonalityTrait)compound).Add(trait);
        }

    }
}
