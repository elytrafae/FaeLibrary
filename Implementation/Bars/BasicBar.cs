using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.Bars {
    public abstract class BasicBar : ModTexturedType {

        /// <summary>
        /// If true, the texture for the bar's bar is loaded from the location CustomBarTexture
        /// If false, the bar will use the Magic Pixel instead
        /// </summary>
        public virtual bool UseCustomBarTexture => false;
        
        /// <summary>
        /// The file name of this bar's bar texture file in the mod loader's file space.
        /// </summary>
        public virtual string CustomBarTexture => (GetType().Namespace + "." + Name).Replace('.', '/');

        public abstract float Fullness { get; }

        protected sealed override void Register() {

        }

        public sealed override void SetupContent() {
            SetStaticDefaults();
        }
    }
}
