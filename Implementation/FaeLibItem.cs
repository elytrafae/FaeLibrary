using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.API.ClassExtensions;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation {
    public class FaeLibItem : GlobalItem {

        public override void ModifyItemScale(Item item, Player player, ref float scale) {
            StatModifier combinedStat = new();
            foreach (DamageClass dmgClass in ModContent.GetContent<DamageClass>()) {
                if (item.DamageType == dmgClass || item.DamageType.CountsAsClass(dmgClass)) {
                    combinedStat = combinedStat.CombineWith(player.GetItemSizeStat(dmgClass));
                }
            }
            scale = combinedStat.ApplyTo(scale);
        }

    }
}
