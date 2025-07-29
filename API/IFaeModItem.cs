using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FaeLibrary.API {
    public interface IFaeModItem {

        /// <summary>
        /// This is called whenever this potion is consumed, and allows 
        /// you to modify its initial potion delay.
        /// NOTE: This is frontloaded, so if you modify this depending on a condition, the delay won't update accordingly!
        /// </summary>
        /// <param name="player"></param>
        /// <param name="delay"></param>
        public virtual void ModifyPotionDelay(Player player, ref int delay) {

        }

    }
}
