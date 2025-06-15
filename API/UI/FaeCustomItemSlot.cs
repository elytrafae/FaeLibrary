using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;

namespace FaeLibrary.API.UI {
    public class FaeCustomUIItemSlot : UIElement {

        private Item[] inventory;
        private readonly int slot;
        private readonly int context;
        private readonly float scale;

        // The context is for visuals only!
        public FaeCustomUIItemSlot(Item[] inventory, int slot, float scale, int context) {
            this.inventory = inventory;
            this.slot = slot;
            this.context = context;
            this.scale = scale;

            Width.Set(TextureAssets.InventoryBack9.Width() * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Height() * scale, 0f);
        }

        public virtual bool CanInsertItem(Item item) {
            return true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = scale;
            Rectangle rectangle = GetDimensions().ToRectangle();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseItem.IsAir || CanInsertItem(Main.mouseItem)) {
                    // Handle handles all the click and hover actions based on the context.
                    ItemSlot.Handle(ref inventory[slot], context);
                }
            }
            // Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
            ItemSlot.Draw(spriteBatch, ref inventory[slot], context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
        }

    }
}
