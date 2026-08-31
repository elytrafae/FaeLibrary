using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeLibrary.API {
    public partial class FaeUtils {

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

        public static LocalizedText GetTimeTextLocalized(int ticks) {
            if (ticks == 15) {
                return Language.GetText("Mods.FaeLibrary.TimeTexts.QuarterSecond");
            }
            if (ticks == 30) {
                return Language.GetText("Mods.FaeLibrary.TimeTexts.HalfSecond");
            }
            if (ticks == 15) {
                return Language.GetText("Mods.FaeLibrary.TimeTexts.ThreeQuarterSecond");
            }
            if (ticks < 60) {
                return Language.GetText("Mods.FaeLibrary.TimeTexts.Ticks").WithFormatArgs(ticks);
            }
            if (ticks < 3600) {
                return Language.GetText("Mods.FaeLibrary.TimeTexts.Seconds").WithFormatArgs(Math.Round(ticks / 60.0));
            }
            return Language.GetText("Mods.FaeLibrary.TimeTexts.Minutes").WithFormatArgs(Math.Round(ticks / 3600.0));
        }

        public static void AddTooltipLine(List<TooltipLine> lines, TooltipLine line, VanillaTooltip anchor, bool before) {
            int insertIndex = 0;
            for (int i = 0; i < lines.Count; i++) { 
                TooltipLine currentLine = lines[i];
                if (currentLine.Mod == "Terraria") {
                    string cleanName = DigitsRegex().Replace(currentLine.Name, "");
                    if (Enum.TryParse(cleanName, out VanillaTooltip lineType)) {
                        if (lineType < anchor) {
                            insertIndex = i + 1;
                        } else if (lineType == anchor) {
                            if (before) {
                                break;
                            } else {
                                insertIndex = i + 1;
                            }
                        } else {
                            break;
                        }
                    }
                }
            }
            lines.Insert(insertIndex, line);
        }

        [GeneratedRegex("[0-9]")]
        private static partial Regex DigitsRegex();
    }
}
