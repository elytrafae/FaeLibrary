using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace FaeLibrary.API {
    public class FaeUtils {

        /// <summary>
        /// <para>NOTE: This method was simply copy-pasted from Calamity. This is the best way something like this can be achieved, so credit to them.</para>
        /// Adds a shimmer recipe, while having the result transform into the ingredient's original result.
        /// <para>This is used for inserting items into various shimmer result trees/loops, like the Class Emblem loop.</para>
        /// </summary>
        public static void InsertShimmerResult(int result, int ingredient) {
            ItemID.Sets.ShimmerTransformToItem[result] = ItemID.Sets.ShimmerTransformToItem[ingredient];
            ItemID.Sets.ShimmerTransformToItem[ingredient] = result;
        }


        public static float DotProduct(Vector2 vec1, Vector2 vec2) {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
        }

        /// <summary>
        /// Drops the specified amount of coins for the player, converted into the different types of coins to avoid issues
        /// </summary>
        /// <param name="player"></param>
        /// <param name="value"></param>
        /// <param name="source"></param>
        public static void DropCoinsForPlayer(Player player, int value, IEntitySource source) {
            int copper = value % 100;
            value /= 100;
            int silver = value % 100;
            value /= 100;
            int gold = value % 100;
            value /= 100;
            int platinum = value;

            if (copper > 0) {
                player.QuickSpawnItem(source, ItemID.CopperCoin, copper);
            }
            if (silver > 0) {
                player.QuickSpawnItem(source, ItemID.SilverCoin, silver);
            }
            if (gold > 0) {
                player.QuickSpawnItem(source, ItemID.GoldCoin, gold);
            }
            while (platinum > 0) {
                int amount = Math.Min(platinum, Item.CommonMaxStack);
                player.QuickSpawnItem(source, ItemID.PlatinumCoin, amount);
                platinum -= amount;
            }
        }

        public static string GetDurationText(int ticks) {
            return (ticks / 60 < 60) ? Language.GetTextValue("CommonItemTooltip.SecondDuration", Math.Round((double)ticks / 60.0)) : Language.GetTextValue("CommonItemTooltip.MinuteDuration", Math.Round((double)(ticks / 3600.0)));
        }

        public static string GetTimeText(int ticks) {
            if (ticks == 15) {
                return Language.GetTextValue("Mods.FaeLibrary.TimeTexts.QuarterSecond");
            }
            if (ticks == 30) {
                return Language.GetTextValue("Mods.FaeLibrary.TimeTexts.HalfSecond");
            }
            if (ticks == 15) {
                return Language.GetTextValue("Mods.FaeLibrary.TimeTexts.ThreeQuarterSecond");
            }
            if (ticks < 60) {
                return Language.GetTextValue("Mods.FaeLibrary.TimeTexts.Ticks", ticks);
            }
            if (ticks < 3600) { 
                return Language.GetTextValue("Mods.FaeLibrary.TimeTexts.Seconds", Math.Round(ticks / 60.0));
            }
            return Language.GetTextValue("Mods.FaeLibrary.TimeTexts.Minutes", Math.Round(ticks / 3600.0));
        }

    }
}
