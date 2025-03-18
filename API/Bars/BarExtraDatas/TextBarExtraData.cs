using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;

namespace FaeLibrary.API.Bars.BarExtraDatas
{
    public class TextBarExtraData : BarExtraData
    {

        string text;
        Color color;

        public TextBarExtraData(string text) : this(text, Color.White)
        {
        }

        public TextBarExtraData(string text, Color color)
        {
            this.text = text;
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.DrawString(FontAssets.ItemStack.Value, text, rect.TopLeft(), color);
        }
    }
}
