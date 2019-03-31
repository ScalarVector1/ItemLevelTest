using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Items
{
  
         class Scarf : ModItem
        {
            public override string Texture
            {
                get
                {
                    return "ItemLevelTest/Items/Scarf";
                }
            }
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Scarf tester");
                Tooltip.SetDefault("Probably buggy and laggy");
             

            }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 5;
            item.useAnimation = 5;
        }

        public override bool CanUseItem(Player player)
            {
                Dust root = Dust.NewDustPerfect(player.MountedCenter, mod.DustType("Scarftestroot"));
                root.customData = player;
                for (int u = 0; u <= 29; u++)
                {
                    Dust segment = Dust.NewDustPerfect(new Vector2(player.MountedCenter.X - u * 3, player.MountedCenter.Y), mod.DustType("Scarftest"));
                    segment.customData = u;
                }
                return true;
            }
        
    }
}
