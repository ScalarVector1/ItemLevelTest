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
        int spdScale = 1; //changes the usetime reduction per level
        float velScale = 0.4f;//changes the shotspeed icnrease per level
        int critScale = 3; //changes the critical strike chance gain per level
        float kbScale = 0.2f; //changes the knockback gained per level
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
            Tooltip.SetDefault((100 + (dmgScale * level * 10)) +" max chage damage" + "\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

            ItemID.Sets.ItemNoGravity[item.type] = true; //makes the item float when thrown
        }

        public override void SetDefaults() //The values set here are the base level 1 values. remember that useTime is subtracted from rather than added to.
        {
            item.damage = 10;
            item.ranged = true;
            //item.shoot = mod.ProjectileType("Suicideprojectile");
            item.width = 16;
            item.height = 64;
            item.useTime = 37;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = 10000;
            item.rare = -12;
            item.crit = 4;
            item.UseSound = SoundID.Item5;
            item.useTurn = true;
            item.autoReuse = true;
            item.noMelee = true;
            item.useAmmo = AmmoID.Arrow;                      
        }

        public static bool charging = false;
        public static float charge = 0;
        const float hyp = 40;
        int dustpulse = 29;
        int soundpulse = 14;
        bool consumedcharge = false;
        bool loadedcharge = false;
        Item selectedammo;

        public override void UpdateInventory(Player player)
        {
            for (int z = 54; z <= 57; z++)
            {
                if (player.inventory[z].ammo == AmmoID.Arrow && !consumedcharge)
                {
                    if (player.inventory[z].stack > 0)
                    {
                        loadedcharge = true;
                        selectedammo = player.inventory[z];
                        consumedcharge = true;
                    }
                    else
                    {
                        loadedcharge = false;
                    }
                }
                else if (loadedcharge == true)
                {

                }
                else
                {
                    loadedcharge = false;
                }

            }
            if (!consumedcharge)
            {
                for (int z = 0; z <= 54; z++)
                {
                    if (player.inventory[z].ammo == AmmoID.Arrow && !consumedcharge)
                    {
                        if (player.inventory[z].stack > 0)
                        {
                            loadedcharge = true;
                            selectedammo = player.inventory[z];
                            consumedcharge = true;
                        }
                        else
                        {
                            loadedcharge = false;
                        }

                    }
                    else if (loadedcharge == true)
                    {

                    }
                    else
                    {
                        loadedcharge = false;
                    }

                }
            }



            if (player.HeldItem.modItem == this && loadedcharge)
            {
                if (Main.mouseRight)
                {
                    if(charge == 0)
                    {
                        for (int k = 0; k <= 6000; k++)
                        {
                            if (Main.dust[k].type == mod.DustType("Bowdust2"))
                            {
                                Main.dust[k].active = false;
                            }
                        }
                    }
                    if (charge < 1)
                    {
                        charge += 0.01f;
                    }
                    charging = true;
                }


                if (!Main.mouseRight && charge > 0)
                {
                    if (charge >= 0.25 && selectedammo.stack > 0)
                    {
                        float x = (Main.screenPosition.X + Main.mouseX) - player.position.X;
                        float y = (Main.screenPosition.Y + Main.mouseY) - player.position.Y;

                        float R = (25 + level * velScale) * charge; //the number here is the base velocity!!

                        float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
                        float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);

                        if (charge < 1)
                        {
                            Projectile.NewProjectile(player.Center, new Vector2(xvel, yvel), mod.ProjectileType("Testarrow2"), (int)(((10 + level * dmgScale) * 5) * charge), 0, Main.myPlayer);
                            Main.PlaySound(SoundID.Item68, player.Center);
                        }
                        if (charge >= 1)
                        {
                            if(ab2 == 0)
                            {
                                Projectile.NewProjectile(player.Center, new Vector2(xvel, yvel), mod.ProjectileType("Testarrow2"), (((10 + level * dmgScale) * 5)), 0, Main.myPlayer);
                                Main.PlaySound(SoundID.Item72, player.Center);
                            }
                            if(ab2 == 1)
                            {
                                Projectile.NewProjectile(player.Center, new Vector2(xvel, yvel), mod.ProjectileType("Phantomarrow"), (((10 + level * dmgScale) * 7)), 0, Main.myPlayer);
                                Main.PlaySound(SoundID.Item72, player.Center);
                            }
                        }

                        charge = 0;
                        dustpulse = 29;
                        charging = false;
                        selectedammo.stack--;
                        
                    }
                    else
                    {
                        charge = 0;
                        dustpulse = 29;
                        charging = false;
                        Main.PlaySound(SoundID.Item16, player.Center);
                    }

                }


                if (charging)
                {
                    if (charge < 1)
                    {
                        soundpulse++;
                        float theta = (float)(Math.PI * 2 * charge);
                        float dustx = (hyp * (float)Math.Cos(theta));
                        float dusty = (hyp * (float)Math.Sin(theta));
                        Dust.NewDustPerfect(new Vector2((player.position.X + player.width/2) + dustx,(player.position.Y + player.height/2) + dusty), mod.DustType("Bowdust2"),null,0,new Color(255,255,255));
                        if(soundpulse >= 15)
                        {
                            Main.PlaySound(SoundID.Item24, player.Center);
                            soundpulse = 0;
                        }
                        
                    }
                    if (charge >= 1)
                    {
                        for(int k = 0; k<=6000; k++)
                        {
                            if (Main.dust[k].type == mod.DustType("Bowdust2"))
                            {
                                Main.dust[k].active = false;
                            }
                        }
                        dustpulse++;

                            if (dustpulse == 30)
                            {
                                for (float dustcounter = 0; dustcounter <= (float)(Math.PI * 2); dustcounter += (float)(Math.PI * 2) / 100)
                                {
                                    float theta = (dustcounter);
                                    float dustx = (hyp * (float)Math.Cos(theta));
                                    float dusty = (hyp * (float)Math.Sin(theta));
                                    Dust.NewDustPerfect(new Vector2((player.position.X + player.width/2) + dustx, (player.position.Y + player.height/2) + dusty), mod.DustType("Bowdust3"), null, 0, new Color(255, 255, 255));
                                }
                            Main.PlaySound(SoundID.Item15, player.Center);
                            
                            dustpulse = 0;
                            }
                    }
                }
            }
            if (player.HeldItem.type != mod.ItemType("Testbow"))
            {
                charge = 0;
                dustpulse = 29;
                charging = false;
            }
        }
        public override bool CanUseItem(Player player)
        {
            //left click
            bool loaded = false;
            bool consumed = false;
            if (!charging)
            {
                for (int z = 54; z <= 57; z++)
                {
                    if (player.inventory[z].ammo == AmmoID.Arrow && !consumed)
                    {
                        if (player.inventory[z].stack > 0)
                        {
                            loaded = true;
                            player.inventory[z].stack--;
                            consumed = true;
                        }
                        else
                        {
                            loaded = false;
                        }
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
                            if (player.inventory[z].stack > 0)
                            {
                                loaded = true;
                                player.inventory[z].stack--;
                                consumed = true;
                            }
                            else
                            {
                                loaded = false;
                            }

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
            }



            
            if (loaded)
            {
                float x = (Main.screenPosition.X + Main.mouseX) - player.position.X;
                float y = (Main.screenPosition.Y + Main.mouseY) - player.position.Y;

                float R = (12 + level * velScale); //the number here is the base velocity!!

                float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
                float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);

                int index = Projectile.NewProjectile(player.Center, new Vector2(xvel, yvel), mod.ProjectileType("Testarrow"), (10 + level * dmgScale), 0, Main.myPlayer);
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
                Main.NewText("Charge ability choice available! Right click in your inventory to select an ability.");
                ab2 = 1;
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
            crit = item.crit = 4 + level * critScale;
        }
        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback = 1f + level * kbScale;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "Tooltip2") //Description
                {
                    line.text = "Hold right click to charge";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip3") //These lines show the stat growth and current added stat
                {
                    line.text = "Stat growth per level:";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip4")
                {
                    line.text = "+" + dmgScale + " Ranged damage (" + dmgScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip5")
                {
                    line.text = "+" + spdScale + " Speed (" + spdScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip6")
                {
                    line.text = "+" + critScale + " Critical strike chance (" + critScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip7")
                {
                    line.text = "+" + kbScale + " Knockback (" + kbScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip8")
                {
                    line.text = "+" + velScale + " Velocity (" + velScale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip9")
                {
                    line.text = "+" + 5 * dmgScale + " Max charge damage (" + dmgScale * 5 * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip13") //These lines show exp and level
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
                if (line.mod == "Terraria" && line.Name == "Tooltip14")
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
                if (line.mod == "Terraria" && line.Name == "Tooltip10") //these lines show abilities
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
                if (line.mod == "Terraria" && line.Name == "Tooltip11")
                {
                    if (ab2 == 0 && level <= 5)
                    {
                        line.text = "Charge: Default";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab2 == 0 && level >= 5)
                    {
                        line.text = "Charge: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab2 == 1)
                    {
                        line.text = "Charge: Phantom Bolt (" + (10 + level * dmgScale) * 7 + ")";
                        line.overrideColor = new Color(80, 200, 175);
                    }
                    else if (ab2 == 2)
                    {
                        line.text = "Charge: [PH]carpetbomb (" + (10 + level * dmgScale) / 5 + " X 10)";
                        line.overrideColor = new Color(80, 200, 175);
                    }
                    else if (ab2 == 3)
                    {
                        line.text = "Active: Slag Ward (" + ((10 + level * dmgScale) * 5) + " Max)";
                        line.overrideColor = new Color(80, 200, 175);
                    }
                }

                if (line.mod == "Terraria" && line.Name == "Tooltip12")
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
                if (line.mod == "Terraria" && line.Name == "Tooltip1")
                {
                    line.text = "Gains 10% of damage dealt as EXP";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if(line.mod == "Terraria" && line.Name == "Tooltip0")
                {
                    line.text = (50 + (dmgScale * level * 5)) + " max chage damage";
                }
            }
        }

        public override void HoldItem(Player player)
        {
            CHUI.ability = ab2;//sets the ability variable in the cooldown UI to display the correct icon
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
