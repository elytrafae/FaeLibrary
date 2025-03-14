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

        // TODO: Add a system that is able to add new hooks to the game
        // Ideas for hooks: Change the chance of an Underground Fairy spawning
    }
}
