using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ItemLevelTest.Items
{
    class Metalgift : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metallic Blessing");
            Tooltip.SetDefault("Used to refine rare metals");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 16));
        }

        public override void SetDefaults()
        {
            item.alpha = 150;
            item.width = 30;
            item.height = 24;
            item.rare = -11;
            item.maxStack = 1;

        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperBar, 50);
            recipe.AddIngredient(ItemID.IronBar, 50);
            recipe.AddIngredient(ItemID.SilverBar, 50);
            recipe.AddIngredient(ItemID.GoldBar, 50);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(null, "Metalgift");
            recipe.AddRecipe();
        }
    }
}
