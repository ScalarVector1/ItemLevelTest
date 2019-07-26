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
            recipe.AddIngredient(null, "Gun1sub");
            recipe.AddIngredient(null, "Vingot");
            recipe.AddIngredient(null, "Metalgift");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class Gun1sub : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xinthil Nanites");
            Tooltip.SetDefault("Specially made nanites intended to fuse with metal");
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 16;
            item.height = 24;
            item.rare = -11;

        }
    }

    public class Gun2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Simulated soul");
            Tooltip.SetDefault("A simulation identical to that used to bring HX-17 to life");
            Main.PlaySound(SoundID.Pixie, item.Center);
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
            recipe.AddIngredient(null, "Gun2sub");
            recipe.AddIngredient(null, "Gun2sub2", 500);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, 1, 1f, .4f);
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width), item.Center.Y - item.height), item.width * 2, item.height * 2, mod.DustType("Gundust"));
                Dust.NewDust(new Vector2(item.Center.X - (item.width) / 2, item.Center.Y - item.height / 2), item.width, item.height, mod.DustType("Gundust"));
            }
        }
    }

    public class Gun2sub : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AI Core");
            Tooltip.SetDefault("A stock AI core, capable of simulating life" + "\n");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 24;
            item.height = 24;
            item.rare = -11;

        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "Tooltip1")
                {
                    line.text = "Bosses slain while holding this item will drop soul simulator frags";
                }
            }
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, 1, 1f, .4f);
            Main.PlaySound(SoundID.Pixie, item.Center);
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width), item.Center.Y - item.height), item.width * 2, item.height * 2, mod.DustType("Gundust"));
                Dust.NewDust(new Vector2(item.Center.X - (item.width) / 2, item.Center.Y - item.height / 2), item.width, item.height, mod.DustType("Gundust"));
            }
        }

        public override bool OnPickup(Player player)
        {
            for (int dustcounter = 0; dustcounter <= 40; dustcounter++)
            {
                Dust.NewDustPerfect(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), mod.DustType("Gundust"), new Vector2(0, Main.rand.Next(-500, 500) * .05f + 0.1f), 0, default, 1.05f);
                Dust.NewDustPerfect(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), mod.DustType("Gundust"), new Vector2(Main.rand.Next(-500, 500) * .05f + 0.1f, 0), 0, default, 1.05f);
            }

            for (int soundcounter = 0; soundcounter <= 3; soundcounter++)
            {
                Main.PlaySound(SoundID.NPCDeath7, item.Center);
            }
            return true;
        }
    }

    public class Gun2sub2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul simulator frag");
            Tooltip.SetDefault("A single character of code used to simulate a living soul");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 10));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 6;
            item.height = 8;
            item.rare = 3;
            item.maxStack = 500;

        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, .1f, .1f, .04f);
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width + 8) / 2, item.Center.Y - item.width / 2), item.width, item.width, mod.DustType("Gundust"));
            }
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