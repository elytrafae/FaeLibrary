using FaeLibrary.API.Bars.BarExtraDatas;
using FaeLibrary.API.Enums;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace FaeLibrary.API.Bars
{
    public abstract class FaeResourceBar : ModTexturedType, IComparable<FaeResourceBar> {

        /// <summary>
        /// If true, the texture for the bar's bar is loaded from the location CustomBarTexture
        /// If false, the bar will use the Magic Pixel instead
        /// </summary>
        public virtual bool UseCustomBarTexture => false;

        /// <summary>
        /// The file name of this bar's bar texture file in the mod loader's file space.
        /// </summary>
        public virtual string CustomBarTexture => Texture + "_Bar";

        public virtual BarRenderingLogic BarTextureDrawLogic => BarRenderingLogic.STRETCH;


        public virtual bool UseCustomBackgroundTexture => false;
        public virtual string CustomBackgroundTexture => Texture + "_Background";
        public virtual BarRenderingLogic BackgroundTextureDrawLogic => BarRenderingLogic.STRETCH;


        public abstract bool Visible { get; }
        public abstract float Fullness { get; }
        public virtual Color BackgroundColor => Color.Black;
        public virtual Color BarColor => Color.White;

        public virtual BarExtraData ExtraData => null;
        public virtual int Priority => 50;

        private static int CurrentNumericID = 0;

        internal int NumericID { set;  get; } // DO NOT USE THIS FOR ANYTHING OTHER THAN SORTING!!!

        protected sealed override void Register()
        {
            NumericID = CurrentNumericID;
            CurrentNumericID++;
        }

        public sealed override void SetupContent()
        {
            SetStaticDefaults();
        }

        public int CompareTo(FaeResourceBar other) {
            int myScore = Priority * 1000 + NumericID;
            int otherScore = other.Priority * 1000 + other.NumericID;
            return myScore - otherScore;
        }
    }
}
