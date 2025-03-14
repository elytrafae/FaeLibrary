using FaeLibrary.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.API
{
    public interface IFaeGlobalProjectile {

        /// <summary>
        /// Allows you to dynamically change the tick rate of projectiles.
        /// Takes care of fractional numbers under the hood by fluctuating tickrate if needed, so just add any number you want!
        /// Avoid using this if there is a better alternative!
        /// </summary>
        /// <param name="projectile">The affected projectile</param> 
        /// <param name="speed">The speed, as a StatModifier</param>
        public virtual void ModifyUpdateRate(Projectile projectile, ref StatModifier speed) { }

        /// <summary>
        /// Allows you to force certain projectiles to be affected by summon tag bonuses or not.
        /// NOTE: Under the hood this is done by changing what <see cref="Projectile.IsMinionOrSentryRelated"/> returns.
        /// </summary>
        /// <param name="projectile">The affected projectile</param> 
        /// <returns>Return null if you want to let vanilla behaviour through, true if you want this projectile to count for tag bonuses, and false if you don't want it to count</returns>
        public virtual bool? CanBenefitFromSummonTagBonuses(Projectile projectile) { return null; }

        /// <summary>
        /// Allows you to dynamically change the summon tag effectiveness of the projectile.
        /// Only runs on projectiles that can benefit from summon tag bonuses!
        /// </summary>
        /// <param name="projectile">The affected projectile</param> 
        /// <param name="effectiveness">A Stat Modifier, used to change the effectiveness</param>
        public virtual void ModifySummonTagEffectveness(Projectile projectile, ref StatModifier effectiveness) { }

        /// <summary>
        /// Allows you to run code before the projectile's AI runs.
        /// This also runs once and only once regardless of how many times AI runs in a single frame!
        /// </summary>
        /// <param name="projectile">The affected projectile</param> 
        public virtual void IgnoreExtraUpdatesBeforeAI(Projectile projectile) { }

    }
}
