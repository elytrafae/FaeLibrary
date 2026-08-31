using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.Implementation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeLibrary.API.ClassExtensions {
    public static class FaeLibRecipeExtensions {

        public static Recipe AddCrossModIngredient(this Recipe recipe, Mod mod, string itemName, int fallback, int stack = 1) {
            if (mod.TryFind(itemName, out ModItem modItem)) {
                recipe.AddIngredient(modItem, stack);
            } else {
                AddFallbackIngredient(recipe, fallback, stack);
            }
            return recipe;
        }

        public static Recipe AddCrossModIngredient(this Recipe recipe, string modName, string itemName, int fallback, int stack = 1) {
            if (ModLoader.TryGetMod(modName, out Mod mod)) {
                return recipe.AddCrossModIngredient(mod, itemName, fallback, stack);
            }
            AddFallbackIngredient(recipe, fallback, stack);
            return recipe;
        }

        public static Recipe AddCrossModTile(this Recipe recipe, Mod mod, string tileName, int fallback) {
            if (mod.TryFind(tileName, out ModTile modTile)) {
                recipe.AddTile(modTile);
            } else {
                AddFallbackTile(recipe, fallback);
            }
            return recipe;
        }

        public static Recipe AddCrossModTile(this Recipe recipe, string modName, string tileName, int fallback) {
            if (ModLoader.TryGetMod(modName, out Mod mod)) {
                return recipe.AddCrossModTile(mod, tileName, fallback);
            }
            AddFallbackTile(recipe, fallback);
            return recipe;
        }

        private static void AddFallbackIngredient(Recipe recipe, int itemId, int stack) {
            if (itemId != ItemID.None) {
                recipe.AddIngredient(itemId, stack);
            }
        }

        private static void AddFallbackTile(Recipe recipe, int tileId) {
            if (tileId < 0) {
                recipe.AddTile(tileId);
            }
        }
    }
}
