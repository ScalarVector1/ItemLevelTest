using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ItemLevelTest.Items
{


    public class Vingot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venerido Bar");
            Tooltip.SetDefault("'Shocking to the touch'");
        }



        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 44;
            item.height = 28;
            item.rare = -11;
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Furnaces);
            recipe.AddIngredient(null, "Vore", 999);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }

    public class Vore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venerido Ore");
            Tooltip.SetDefault("Mostly worthless rock");
        }



        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 44;
            item.height = 28;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 1;
            
            item.createTile = mod.TileType("Voreblock");

        }



    }

    public class Vorb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venerido Orb");
            Tooltip.SetDefault("Used to reset abilities");
        }



        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 24;
            item.height = 24;
            item.rare = 1;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Furnaces);
            recipe.AddIngredient(null, "Vore", 200);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }






}