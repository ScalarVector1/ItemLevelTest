using System;
using System.Collections.Generic;
using ItemLevelTest.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using ItemLevelTest.Projectiles;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace ItemLevelTest.Items
{
    class Testbow : ModItem
    {
        public int exp = 0; //the current exp of the item (this is saved)why are you
        int expRequired = 50; //the exp required to reach the next level, this value sets the base
        public int level = 0; //the item's current level (this is saved)
        int dmgScale = 5; //changes the damage gain per level
        int spdScale = 2; //changes the usetime reduction per level
        float velScale = 1f;//changes the shotspeed icnrease per level
        int critScale = 1; //changes the critical strike chance gain per level
        float kbScale = 0.5f; //changes the knockback gained per level
        const float expScale = 1.2f; //Changes the multiplier for the amount of exp required for the next level after the previous

        //ability variables
        public int ab1 = 0; //passive
        public int ab2 = 0; //active
        public int ab3 = 0; //ultimate

        public static int cd = 0; //cooldown

        //constants to deobfuscate the ability variables
        const int none = 0;
        const int active1 = 1;
        const int active2 = 2;
        const int active3 = 3;

        const int passive1 = 1;
        const int passive2 = 2;

        const int ultimate1 = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leveling Bow");
            Tooltip.SetDefault("Gains 10% of damage dealt as EXP" + "\n\n\n\n\n\n\n\n\n\n\n");

            ItemID.Sets.ItemNoGravity[item.type] = true; //makes the item float when thrown
        }

        public override void SetDefaults() //The values set here are the base level 1 values. remember that useTime is subtracted from rather than added to.
        {
            item.damage = 10;
            item.ranged = true;
            //item.shoot = mod.ProjectileType("Suicideprojectile");
            item.width = 16;
            item.height = 64;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = 10000;
            item.rare = -12;
            item.crit = 7;
            item.UseSound = SoundID.Item5;
            item.useTurn = true;
            item.autoReuse = true;
            item.noMelee = true;
            item.useAmmo = AmmoID.Arrow;                      
        }

        public override bool CanUseItem(Player player)
        {
            bool loaded = false;
            bool consumed = false;
            for (int z = 54; z<=57; z++)
            {
                if (player.inventory[z].ammo == AmmoID.Arrow && !consumed)
                {
                    loaded = true;
                    player.inventory[z].stack--;
                    consumed = true;

                }
                else if (loaded == true)
                {

                }
                else
                {
                    loaded = false;
                }

            }
            if (!consumed)
            {
                for (int z = 0; z <= 54; z++)
                {
                    if (player.inventory[z].ammo == AmmoID.Arrow && !consumed)
                    {
                        loaded = true;
                        player.inventory[z].stack--;
                        consumed = true;

                    }
                    else if (loaded == true)
                    {

                    }
                    else
                    {
                        loaded = false;
                    }

                }
            }
            if (loaded)
            {
                float x = (Main.screenPosition.X + Main.mouseX) - player.position.X;
                float y = (Main.screenPosition.Y + Main.mouseY) - player.position.Y;

                float R = (15 + level * velScale); //the number here is the base velocity!!

                float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
                float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);

                int index = Projectile.NewProjectile(player.position, new Vector2(xvel, yvel), mod.ProjectileType("Testarrow"), (10 + level * dmgScale), 0, Main.myPlayer);
                Testarrow proj = Main.projectile[index].modProjectile as Testarrow;
                proj.instance = this;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void Expcalc() //calculates the exp of the item, and if it should be leveled up (this handles the actual increase to the level variable itself also)
        {
            if (exp >= expRequired)
            {
                level++;
                Leveler();
                expRequired = (int)(expRequired * expScale);
                exp = 0; //reset exp
                Main.PlaySound(SoundID.Item37);
            }
        }

        public void Leveler() //the method that adjusts the properties of the item with the level of the weapon appropriately, also handles events that occur on levelup
        {
            Player player = Main.player[Main.myPlayer];

            if (item.useTime - 2 >= 2) //adjusts the item's usetime and animation, ensures that it will never drop below 2 (this causes buggy animation)
            {
                item.useTime = 25 - level * spdScale;
                item.useAnimation = 25 - level * spdScale;
            }
            else
            {
                item.useTime = 2;
                item.useAnimation = 2;
            }

            item.value = 10000 + level * 1000; //adjusts the item's gold value with level

            //Handles the dust that spawns on level up
            {
                for (int dustcounter = 0; dustcounter <= 45; dustcounter++)
                {
                    Dust.NewDust(new Vector2(player.MountedCenter.X - 30, player.MountedCenter.Y - 50), 70, 70, mod.DustType("Leveldust"));
                }
            }

            //Handles chat text for the appropriate levels
            if (level == 2)
            {
                Main.NewText("Passive ability choice available! Right click in your inventory to select an ability.");
            }

            if (level == 5)
            {
                Main.NewText("Active ability choice available! Right click in your inventory to select an ability.");
            }

            if (level == 8)
            {
                Main.NewText("Ultimate ability choice available! Right click in your inventory to select an ability.");
            }


        }

        public override void GetWeaponDamage(Player player, ref int damage) //these methods adjust the damage, critical hit chance, and knockback of the weapon with it's level.
        {
            damage = 10 + level * dmgScale;
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit = item.crit = 0 + level * critScale;
        }
        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback = 1f + level * kbScale;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "Tooltip1") //These lines show the stat growth and current added stat
                {
                    line.text = "Stat growth per level:";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip2")
                {
                    line.text = "+" + dmgScale + " Melee damage (" + dmgScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip3")
                {
                    line.text = "+" + spdScale + " Speed (" + spdScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip4")
                {
                    line.text = "+" + critScale + " Critical strike chance (" + critScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip5")
                {
                    line.text = "+" + kbScale + " Knockback (" + kbScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip10") //These lines show exp and level
                {
                    if (level < 10)
                    {
                        line.text = "Exp: " + exp + " / " + expRequired;
                    }
                    else
                    {
                        line.text = "";
                    }
                    line.overrideColor = new Color(170, 212, 120);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip11")
                {
                    if (level < 10)
                    {
                        line.text = "Level: " + level;
                        line.overrideColor = new Color(96, 176, 72);
                    }
                    else
                    {
                        line.text = "Level: MAX";
                        line.overrideColor = new Color(96, 176, 162);
                    }

                }
                if (line.mod == "Terraria" && line.Name == "Tooltip6") //these lines show abilities
                {
                    if (ab1 == 0 && level <= 2)
                    {
                        line.text = "Passive: LOCKED";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab1 == 0 && level >= 2)
                    {
                        line.text = "Passive: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab1 == 1)
                    {
                        line.text = "Passive: Burning Strike (" + level * 1 + "-" + ((level * 1) + 1) + "s)";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                    else if (ab1 == 2)
                    {
                        line.text = "Passive: Firebolts (" + (10 + level * dmgScale) + ")";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip7")
                {
                    if (ab2 == 0 && level <= 5)
                    {
                        line.text = "Active: LOCKED";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab2 == 0 && level >= 5)
                    {
                        line.text = "Active: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab2 == 1)
                    {
                        line.text = "Active: Slag Buster (" + (10 + level * dmgScale) * 3 + ")";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                    else if (ab2 == 2)
                    {
                        line.text = "Active: Slagburst (" + ((10 + level * dmgScale) / 3) * 6 + "/s)";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                    else if (ab2 == 3)
                    {
                        line.text = "Active: Slag Ward (" + ((10 + level * dmgScale) * 5) + ")";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                }

                if (line.mod == "Terraria" && line.Name == "Tooltip9")
                {
                    if (ab3 == 0 && level <= 8)
                    {
                        line.text = "Ultimate: LOCKED";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab3 == 0 && level >= 8)
                    {
                        line.text = "Ultimate: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab3 == 1)
                    {
                        line.text = "Ultimate: Aura of Cinders (10/s)";
                        line.overrideColor = new Color(255, Main.DiscoG, 45);
                    }
                    else if (ab3 == 2)
                    {
                        line.text = "Ultimate: DEV/NULL";
                        line.overrideColor = new Color(255, Main.DiscoG, 45);
                    }
                }
                if (line.mod == "Terraria" && line.Name == "ItemName") //this edits the item's name's color
                {
                    line.overrideColor = new Color(40, Main.DiscoG - 50, 250);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip0")
                {
                    line.overrideColor = new Color(255, 218, 75);
                }
            }
        }


        //Ensures multiples of the same item can exist in the same inventory
        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {"exp", exp},
                {"expRequired", expRequired},
                {"level", level},
                {"ab1", ab1},
                {"ab2", ab2},
                {"ab3", ab3}
            };
        }

        public override void Load(TagCompound tag)
        {
            exp = tag.GetInt("exp");
            expRequired = tag.GetInt("expRequired");
            level = tag.GetInt("level");
            ab1 = tag.GetInt("ab1");
            ab2 = tag.GetInt("ab2");
            ab3 = tag.GetInt("ab3");
            Leveler();

        }


    }
}
