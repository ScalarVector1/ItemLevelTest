using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace ItemLevelTest.Items
{
    public class Spear1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PH spear mat 1");
            Tooltip.SetDefault("Put text here");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 36;
            item.height = 36;
            item.rare = -11;

        }
    }
    public class Spear2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PH spear mat 2");
            Tooltip.SetDefault("Put text here");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 36;
            item.height = 36;
            item.rare = -11;

        }
    }
    public class Spear3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PH spear mat 3");
            Tooltip.SetDefault("Put text here");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 36;
            item.height = 36;
            item.rare = -11;

        }
    }
    public class Spear4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PH spear mat 4");
            Tooltip.SetDefault("Put text here");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 36;
            item.height = 36;
            item.rare = -11;

        }
    }
}