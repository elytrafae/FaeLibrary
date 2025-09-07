using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FaeLibrary.Config {
    internal class ClientConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [ReloadRequired]
        [DefaultValue(false)]
        public bool missingTextureEnabled;

        public static ClientConfig Get() { 
            return ModContent.GetInstance<ClientConfig>();
        }
    }
}
