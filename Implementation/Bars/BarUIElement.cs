using FaeLibrary.API.Bars;
using FaeLibrary.API.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace FaeLibrary.Implementation.Bars
{
    internal class BarUIElement : UIElement {

        private static readonly Rectangle BAR_SLIT = new(42, 21, 200, 8);
        private static readonly Rectangle EXTRA_DATA_SPACE = new(42, 4, 184, 16);

        public FaeResourceBar bar;
        Asset<Texture2D> mainTexture;
        Asset<Texture2D> backgroundTexture;
        Asset<Texture2D> barTexture;
        Asset<Texture2D> iconTexture;

        public BarUIElement(FaeResourceBar bar) {
            this.bar = bar;
            Width.Set(256, 0f);
            Height.Set(32, 0f);
            mainTexture = ModContent.Request<Texture2D>("FaeLibrary/Assets/Bars/DefaultBar");
            backgroundTexture = bar.UseCustomBackgroundTexture ? ModContent.Request<Texture2D>(bar.CustomBackgroundTexture) : TextureAssets.MagicPixel;
            barTexture = bar.UseCustomBarTexture ? ModContent.Request<Texture2D>(bar.CustomBarTexture) : TextureAssets.MagicPixel;
            iconTexture = ModContent.Request<Texture2D>(bar.Texture);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            CalculatedStyle dims = GetInnerDimensions();
            Vector2 position = new Vector2(dims.X, dims.Y);
            Vector2 slitPosition = position + BAR_SLIT.TopLeft();

            Rectangle barSlit = new Rectangle((int)slitPosition.X, (int)slitPosition.Y, BAR_SLIT.Width, BAR_SLIT.Height);
            DrawABar(spriteBatch, bar.BackgroundTextureDrawLogic, backgroundTexture.Value, bar.BackgroundColor, barSlit);
            barSlit.Width = (int)(barSlit.Width * bar.Fullness);
            DrawABar(spriteBatch, bar.BarTextureDrawLogic, barTexture.Value, bar.BarColor, barSlit);

            spriteBatch.Draw(mainTexture.Value, position, Color.White);

            spriteBatch.Draw(iconTexture.Value, position + new Vector2(2, 2), Color.White); // Assumes texture is 34x28

            if (bar.ExtraData != null) {
                Rectangle currentExtraDataRectangle = new Rectangle((int)(EXTRA_DATA_SPACE.X + position.X), (int)(EXTRA_DATA_SPACE.Y + position.Y), EXTRA_DATA_SPACE.Width, EXTRA_DATA_SPACE.Height);
                bar.ExtraData.Draw(spriteBatch, currentExtraDataRectangle);
            }
        }

        private void DrawABar(SpriteBatch spriteBatch, BarRenderingLogic renderLogic, Texture2D texture, Color color, Rectangle rect) {
            if (renderLogic == BarRenderingLogic.STRETCH) {
                Vector2 slitScale = new Vector2(rect.Width, rect.Height) / texture.Size();
                spriteBatch.Draw(texture, rect.TopLeft(), null, color, 0, Vector2.Zero, slitScale, SpriteEffects.None, 0);
            } else if (renderLogic == BarRenderingLogic.REPEAT) {
                int width = texture.Width;
                int totalWidth = rect.Width;
                int i = 0;
                while (width * i < totalWidth) {
                    Vector2 pos = rect.TopLeft() + new Vector2(i * width, 0);
                    Rectangle origRect = new(0, 0, Math.Min(width, totalWidth - width * i), Math.Min(texture.Height, rect.Height));
                    spriteBatch.Draw(texture, pos, origRect, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
                    i++;
                }
            }
        }

    }
}
