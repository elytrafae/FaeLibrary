using FaeLibrary.API.Bars;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace FaeLibrary.Implementation.Bars
{
    class BarsUI : UIState {

        UIElement parent;
        List<BarUIElement> bars;

        public override void OnInitialize() {
            bars = new();
            parent = new();
            parent.MaxHeight.Set(256, 0f);
            parent.Width.Set(256, 0f);
            parent.Top.Set(300, 0f);
            parent.Left.Set(10, 0f);

            Append(parent);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            // Remove bars that should not be visible now
            int i = 0;
            SortedSet<FaeResourceBar> barsThatAreAlreadyVisible = new();
            while (i < bars.Count) {
                if (!bars[i].bar.Visible) {
                    parent.RemoveChild(bars[i]);
                    bars.RemoveAt(i);
                } else {
                    barsThatAreAlreadyVisible.Add(bars[i].bar);
                    i++;
                }
            }

            // Add bars that should be visible, but are not yet
            foreach (FaeResourceBar bar in ModContent.GetContent<FaeResourceBar>()) {
                if (bar.Visible && !barsThatAreAlreadyVisible.Contains(bar)) {
                    BarUIElement barElem = new BarUIElement(bar);
                    bars.Add(barElem);
                    parent.Append(barElem);
                    barElem.Left.Set(0, 0);
                }
            }

            // Make sure everything has a proper position
            for (i = 0; i < bars.Count; i++) {
                BarUIElement bar = bars[i];
                bar.Top.Set(30 * i, 0);
            }
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class BarsUISystem : ModSystem {
        internal BarsUI BarsUI;
        private UserInterface BarsUIInterface;

        public override void Load() {
            BarsUI = new();
            BarsUI.Activate();
            BarsUIInterface = new UserInterface();
            BarsUIInterface.SetState(BarsUI);
        }

        public override void UpdateUI(GameTime gameTime) {
            BarsUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (mouseTextIndex != -1) {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "FaeLibrary: BarsUI",
                    delegate {
                        BarsUIInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

    }
}