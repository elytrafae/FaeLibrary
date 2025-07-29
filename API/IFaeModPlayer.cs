using FaeLibrary.API.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.API {
    public interface IFaeModPlayer {

        /// <summary>
        /// This is called whenever this player dodges an attack.
        /// Only gets called on the client of the player that dodged.
        /// </summary>
        /// <param name="info">Hurt Info from the dodge</param> 
        /// <param name="dodgeType">The type of dodge that occured. Check <see name="DodgeType"> for possible values.</param> 
        public virtual void OnDodge(Player.HurtInfo info, DodgeType dodgeType) {
        }

        /// <summary>
        /// This is called whenever a potion is consumed, and allows 
        /// you to modify its initial potion delay.
        /// NOTE: This is frontloaded, so if you modify this depending on a condition, the delay won't update accordingly!
        /// </summary>
        /// <param name="item"></param>
        /// <param name="delay"></param>
        public virtual void ModifyPotionDelay(Item item, ref int delay) { 
            
        }
    }
}
