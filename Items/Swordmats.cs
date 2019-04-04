using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ItemLevelTest.Items
{
    public class Swordspirit1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Koranthi's Spirit");
            Tooltip.SetDefault("A Fragment of the Forge's Master Herself");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 7));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 28;
            item.height = 36;
            item.rare = -11;

        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, 2, .7f, .6f);
            Main.PlaySound(SoundID.Pixie, item.Center);
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width + 8), item.Center.Y - item.width), item.width * 2, item.width * 2, mod.DustType("Sworddust"));
                Dust.NewDust(new Vector2(item.Center.X - (item.width + 8) / 2, item.Center.Y - item.width / 2), item.width, item.width, mod.DustType("Sworddust"));
            }

        }
        public override bool OnPickup(Player player)
        {
            for (int dustcounter = 0; dustcounter <= 40; dustcounter++)
            {
                Dust.NewDustPerfect(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), mod.DustType("Sworddust2"), new Vector2(0, Main.rand.Next(500) * .01f + 0.1f), 0, default(Color), 1.05f);
                Dust.NewDustPerfect(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), mod.DustType("Sworddust2"), new Vector2(0, -1 * (Main.rand.Next(500) * .01f + 0.1f)), 0, default(Color), 1.05f);
            }

            for (int soundcounter = 0; soundcounter <= 3; soundcounter++)
            {
                Main.PlaySound(SoundID.NPCDeath7, item.Center);
            }
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Swordspirit1");
            recipe.AddTile(null, "Swordaltar1t");
            recipe.SetResult(null, "Swordsoul1");
            recipe.AddRecipe();
        }
    }

    public class Swordsoul1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Forge");
            Tooltip.SetDefault("A Reclaimed Shard of Koranthi's Soul");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 9));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }



        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 46;
            item.height = 32;
            item.rare = -11;

        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, 2, .7f, .6f);
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width + 8), item.Center.Y - item.width), item.width * 2, item.width * 2, mod.DustType("Sworddust"));
                Dust.NewDust(new Vector2(item.Center.X - (item.width + 8)/2, item.Center.Y - item.width/2), item.width, item.width, mod.DustType("Sworddust"));
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }

    public class Swordaltar1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[PH] Swordaltar1");
            Tooltip.SetDefault("[PH] Craft soul from spirit here");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 14;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("Swordaltar1t");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemID.StoneBlock, 999);
            recipe.AddIngredient(ItemID.Ruby, 50);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient(ItemID.WoodenSword);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

    
    public class Swordore1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Ore");
            Tooltip.SetDefault("A Pearl of Purity in the dark...");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 7));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 20;
            item.height = 30;
            item.rare = -11;

        }

        public override void PostUpdate()
        {
            Main.PlaySound(SoundID.Pixie, item.Center);
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width), item.Center.Y - item.width), item.width * 2, item.width * 2, mod.DustType("Swordoredust"));
                Dust.NewDust(new Vector2(item.Center.X - (item.width) / 2, item.Center.Y - item.width / 2), item.width, item.width, mod.DustType("Swordoredust"));
            }
        }

        public override bool OnPickup(Player player)
        {
            for (int dustcounter = 0; dustcounter <= 120; dustcounter++)
            {
                  Dust.NewDustPerfect(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), mod.DustType("Swordoredust2"), new Vector2(Main.rand.Next(-40, 40) * 0.1f, Main.rand.Next(-120,-30)*0.1f ), 0, default(Color), 2.1f);
                
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Swordore1");
            //recipe.AddTile(null, "Swordforge1t");
            recipe.SetResult(null, "Swordbar1");
            recipe.AddRecipe();
        }
    }

    public class Swordbar1 : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Eternal Bar");
            Tooltip.SetDefault("Purifying metal");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 7));

        }
        public override void SetDefaults()
        {
            item.height = 24;
            item.width = 30;
            item.rare = -11;
        }

        public override void PostUpdate()
        {
            
            if (Main.rand.Next(2) == 0)
            {
                
                Dust.NewDust(new Vector2(item.Center.X - (item.width) / 2, item.Center.Y - item.width / 2), item.width, item.width, mod.DustType("Swordoredust"));
            }
        }
    }

    public class Swordsteel1 : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Reclaimed Coremetal");
            Tooltip.SetDefault("The Material Used by the Dragon Herself...");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
        }
        public override void SetDefaults()
        {
            item.height = 24;
            item.width = 30;
            item.rare = -11;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Swordbar1");
            recipe.AddIngredient(null, "Vingot");
            recipe.AddRecipeGroup("IronBar", 50);
            //recipe.AddTile(null, "Swordforge1t");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class Swordlog1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cinderplank");
            Tooltip.SetDefault("[PH]Craft with crystalline obsidian");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 200);
            recipe.AddIngredient(ItemID.RichMahogany, 200);
            recipe.AddIngredient(ItemID.Hellstone, 100);
        }
    }
    
    public class Swordlogadd1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalline Obsidian");
            Tooltip.SetDefault("[PH]Craft with cinderplank");
        
        }
    }

    public class Swordplank1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devilswood");
            Tooltip.SetDefault("[PH]Final wood material");

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Swordlog1");
            recipe.AddIngredient(null, "Swordlogadd1");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    class Dropworld : ModWorld
    { 
        public static bool oredrop = false;
        public bool orefailed = false;
        public static bool spiritdrop = false;
        public static bool spiritfailed = false;
        public override void PreUpdate()
        {

            Player player = Main.LocalPlayer;
            if (((NPC.AnyNPCs(NPCID.BrainofCthulhu)) || (NPC.AnyNPCs(NPCID.EaterofWorldsHead)) || (NPC.AnyNPCs(NPCID.EaterofWorldsTail)) || (NPC.AnyNPCs(NPCID.EaterofWorldsBody))) && orefailed == false)
            {
                oredrop = true;
                if (!player.armor.Where((x, i) => i >= 2 && i <= 8 + player.extraAccessorySlots).All(x => x.IsAir))
                {
                    oredrop = false;
                    orefailed = true;
                }
            }
            if (!((NPC.AnyNPCs(NPCID.BrainofCthulhu)) || (NPC.AnyNPCs(NPCID.EaterofWorldsHead)) || (NPC.AnyNPCs(NPCID.EaterofWorldsTail)) || (NPC.AnyNPCs(NPCID.EaterofWorldsBody))) && orefailed == true)
            {
                orefailed = false;
            }

            if ((NPC.AnyNPCs(NPCID.SkeletronHead)) && spiritfailed == false)
            {
                spiritdrop = true;
                if (!player.armor.Where((x, i) => i <= 2).All(X => X.IsAir))
                {
                    spiritdrop = false;
                    spiritfailed = true;
                }
            }
            if ((NPC.AnyNPCs(NPCID.SkeletronHead)) && spiritfailed == true)
            {
                spiritdrop = false;
            }
        }
    }
    class Drop : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            int playerIndex = npc.lastInteraction;
            if (!Main.player[playerIndex].active || Main.player[playerIndex].dead)
            {
                playerIndex = npc.FindClosestPlayer(); // Since lastInteraction might be an invalid player fall back to closest player.
            }
            Player player = Main.player[playerIndex];
            
           
                if ((npc.type == NPCID.BrainofCthulhu) || ((npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.EaterofWorldsBody) && npc.boss && Dropworld.oredrop == true))

                {
                    Item.NewItem((int)player.position.X, (int)player.position.Y - 100, player.width, player.height, mod.ItemType("Swordore1"));
                }
            
            if (npc.type == NPCID.SkeletronHead && Dropworld.spiritdrop == true)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Swordspirit1"));
            }
        }
    }
}