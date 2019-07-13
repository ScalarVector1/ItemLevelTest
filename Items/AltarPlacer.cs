using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace ItemLevelTest.Items
{

    public class AltarPlacer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DEV Altar Placer");
            Tooltip.SetDefault("You should not have this!");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 14;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 1;
            item.consumable = false;
            item.value = 0;
            item.createTile = mod.TileType("Altar");
        }
    }
}