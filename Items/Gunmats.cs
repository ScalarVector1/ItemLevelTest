using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace ItemLevelTest.Items
{
    public class Gun1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xinthil Linving Metal");
            Tooltip.SetDefault("A dangerous and highly illegal metal-nanite hybrid");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 9));
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 30;
            item.height = 234 / 9;
            item.rare = -11;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class Gun2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Simulated soul");
            Tooltip.SetDefault("A simulation identical to that used to bring HX-17 to life");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 5));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 48;
            item.height = 26;
            item.rare = -11;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class Gun3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holo-silk");
            Tooltip.SetDefault("Impossibly smooth silk made of pure light");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 10));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 40;
            item.height = 280 / 10;
            item.rare = -11;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class Gun4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Modified X-13 Photon Module");
            Tooltip.SetDefault("A photon module re-configured to be significantly more unstable...");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 34;
            item.height = 34;
            item.rare = -11;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}