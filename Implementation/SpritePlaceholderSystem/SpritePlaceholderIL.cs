using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FaeLibrary.Config;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace FaeLibrary.Implementation.SpritePlaceholderSystem {
    public class SpritePlaceholderIL : ModSystem {

        public override void Load() {
            if (ClientConfig.Get().missingTextureEnabled) {
                PatchModType(typeof(ModItem), "AutoStaticDefaults");
                PatchModType(typeof(ModProjectile), "AutoStaticDefaults");
                PatchModType(typeof(ModNPC), "AutoStaticDefaults");
                PatchModType(typeof(ModBuff), "SetupContent");
            }
        }

        private void PatchModType(Type type, string methodName) {
            MethodInfo info = type.GetMethod(methodName);
            if (info != null) {
                MonoModHooks.Modify(info, ILSetupContent(type));
            } else {
                Mod.Logger.Error("Error patching " + type.FullName + " failed! Method " + methodName + " not found!");
            }
        }

        private ILContext.Manipulator ILSetupContent(Type type) {
            return (ILContext il) => {
                ILCursor c = new(il);
                Type modContentType = typeof(ModContent);
                if (c.TryGotoNext(MoveType.Before, i => i.MatchCall(modContentType, "Request"))) {
                    ILLabel hasSpriteLabel = c.DefineLabel();

                    c.EmitLdarg0();
                    c.EmitCallvirt(type.GetProperty("Texture").GetGetMethod());
                    c.EmitCall(modContentType.GetMethod("HasAsset", BindingFlags.Static | BindingFlags.Public));
                    c.EmitBrtrue(hasSpriteLabel);
                    c.EmitPop();
                    c.EmitPop();
                    c.EmitLdstr("FaeLibrary/Assets/MissingTexture");
                    c.EmitLdcI4(2);
                    c.MarkLabel(hasSpriteLabel);
                }
            };
        }

    }
}
