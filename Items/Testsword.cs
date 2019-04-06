using System;
using System.Collections.Generic;
using ItemLevelTest.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;



namespace ItemLevelTest.Items
{
    public class Testsword : ModItem //change name later lol 
    {
        int exp = 0;
        int expreq = 50;
        public int level = 0;
        int dmgscale = 5;
        int spdscale = 2;
        int critscale = 1;
        float kbscale = 0.5f;
        int spawned = 0;
        int timer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Koranithus");
            Tooltip.SetDefault("Gains 10% of damage dealt as EXP" + "\n\n\n\n\n\n\n\n\n\n\n");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 11));
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 62;
            item.height = 62;
            item.useTime = 50;
            item.useAnimation = 60;
            item.useStyle = 1;
            item.knockBack = 1f;
            item.value = 10000;
            item.rare = -12;
            item.crit = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddIngredient(null, "Swordsteel1");
            recipe.AddIngredient(null, "Swordsoul1");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void OnCraft(Recipe recipe) 
        {
            //text
            Player player = Main.player[Main.myPlayer];
            string text = player.name + " Has Crafted Testswordname[PH]!";
            if (Main.netMode == 2) // Server
            {
                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 100f, 45f, 255f, 0, 0, 0);
            }
            else if (Main.netMode == 0) // Single Player
            {
                Main.NewText(text, new Color(255, 100, 45));
                Main.NewText("Your body goes numb...", new Color(100, 100, 100));
            }

            //dust
            for (int dustcounter = 300; dustcounter >= 0; dustcounter--)
            {
                float yvel = 0;
                float xvel = 0;
                float hyp = 0;
                hyp = Main.rand.Next(0, 100) * 0.1f;
                xvel = Main.rand.Next(-400, 400) * .01f;
                if (Main.rand.Next(2) == 0)
                {
                    yvel = (float)Math.Sqrt(hyp - xvel * xvel);
                }
                else
                {
                    yvel = (float)Math.Sqrt(hyp - xvel * xvel) * -1;
                }
                Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), mod.DustType("Sworddust2"), new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(8, 10) * 0.1f);
                if (Main.rand.Next(2) == 0)
                {
                    Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), mod.DustType("Slagdust"), new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(12, 14) * 0.1f);
                }
                Main.PlaySound(SoundID.Item37, player.Center);
                Main.PlaySound(SoundID.Item45, player.Center);
                
            }

        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            damage = 10 + level * dmgscale;
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit = item.crit = 0 + level * critscale;
        }
        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback = level * kbscale;
        }
        


        public void Leveler()
        {
            Player player = Main.player[Main.myPlayer];
            //item.damage = 10 + level * dmgscale;
            if (item.useTime - 2 >= 2)
            {
                item.useTime = 50 - level * spdscale;
                item.useAnimation = 50 - level * spdscale;
            }
            else
            {
                item.useTime = 2;
                item.useAnimation = 2;
            }
            item.value = 10000 + level * 1000;
            //item.knockBack = level * kbscale;
            //item.crit = 0 + level * critscale;
            if (level >= 10)
            {
                for (int dustcounter = 0; dustcounter <= 60; dustcounter++)
                {
                    Dust.NewDust(new Vector2(player.MountedCenter.X - 30, player.MountedCenter.Y - 50), 70, 70, mod.DustType("Leveldust"));
                    Dust.NewDust(new Vector2(player.MountedCenter.X - 30, player.MountedCenter.Y - 50), 90, 90, mod.DustType("Leveldust"));
                }
            }
            else
            {
                for (int dustcounter = 0; dustcounter <= 45; dustcounter++)
                {
                    Dust.NewDust(new Vector2(player.MountedCenter.X - 30, player.MountedCenter.Y - 50), 70, 70, mod.DustType("Leveldust"));
                }
            }
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
            //Ability();

            /*if (level >= 10)  
            {
                
                //for (int i = 0; i < player.inventory.Length; i++)
                //{
                    //Item item = player.inventory[i];
                    if (item.type == mod.ItemType("Testsword"))
                    {
                        item.active = false;
                        Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Swordore1"));
                    }
                //}
            }*/
        }

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            foreach (TooltipLine line in tooltips) 
            {
                if (line.mod == "Terraria" && line.Name == "Tooltip1")
                {
                    line.text = "Stat growth per level:";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip2")
                {
                    line.text = "+" + dmgscale + " Melee damage (" + dmgscale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip3")
                {
                    line.text = "+" + spdscale + " Speed (" + spdscale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip4")
                {
                    line.text = "+" + critscale + " Critical strike chance (" + critscale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip5")
                {
                    line.text = "+" + kbscale + " Knockback (" + kbscale * level + ")";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip10") 
                {
                    if (level < 10)
                    {
                        line.text = "Exp: " + exp + " / " + expreq;
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
                if (line.mod == "Terraria" && line.Name == "Tooltip6")
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
                        line.text = "Passive: Burning Strike (" + level * 1 + "-" + ((level * 1) + 1) +"s)";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                    else if (ab1 == 2)
                    {
                        line.text = "Passive: Firebolts (" + (10 + level * dmgscale) + ")";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                }
                    if (line.mod == "Terraria" && line.Name == "Tooltip7")
                {
                    if(ab2 == 0 && level <= 5)
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
                        line.text = "Active: Slag Buster (" + (10 + level * dmgscale) * 3 + ")";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                    else if (ab2 == 2)
                    {
                        line.text = "Active: Slagburst (" + ((10 + level * dmgscale) / 3) * 6 + "/s)";
                        line.overrideColor = new Color(255, 100, 45);
                    }
                    else if (ab2 == 3)
                    {
                        line.text = "Active: Slag Ward (" + ((10 + level * dmgscale) * 5)  + ")";
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
                if (line.mod == "Terraria" && line.Name == "ItemName")
                {
                    line.overrideColor = new Color(255, Main.DiscoG, 50);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip0")
                {
                    line.overrideColor = new Color(255, 218, 75);
                }
            }   
        }

        public void Expcalc()
        {
            if (exp >= expreq)
            {
                level++;
                Leveler();
                expreq = expreq * 2;
                exp = 0;
                Main.PlaySound(SoundID.Item37);
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (level < 10)
            {

                if (damage >= 10)
                {
                    exp = exp + damage / 10;
                    Expcalc();
                }
                else
                {
                    exp++;
                    Expcalc();
                }
            }
            else
            {
                exp = 0;
            }

            //passive stuff
            if(ab1 == 1)
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(0,60) + level * 60);
            }

            if (ab1 == 2)
            {
                if (Main.rand.Next(9) == 1)
                {

                }
            }




        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
             float length = item.Size.Length() + 1;
             float r = player.direction == 1 ? ((float)Math.PI / 4) * -1 : (float)Math.PI * 5 / 4;
             Dust.NewDustPerfect(player.MountedCenter + new Vector2(length * (float)Math.Cos(player.itemRotation + r), length * (float)Math.Sin(player.itemRotation + r)), mod.DustType("Sworddust"));
             Dust.NewDustPerfect(player.MountedCenter + new Vector2(length * (float)Math.Cos(player.itemRotation + r - 0.12), length * (float)Math.Sin(player.itemRotation + r - 0.12)), mod.DustType("Sworddust"));
             Dust.NewDustPerfect(player.MountedCenter + new Vector2(length * (float)Math.Cos(player.itemRotation + r - 0.04), length * (float)Math.Sin(player.itemRotation + r - 0.04)), mod.DustType("Sworddust"));

        }
        Player player = Main.LocalPlayer;
        private int ab1 = 0;
        int ab2 = 0; 
        private int ab3 = 0;
        public static int cd = 0;

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if(ab2 == 0)
                {
                    Main.NewText("Active abilities unlock at level 5!");
                    return false;
                }
                if (ab2 == 1)
                {
                    if (cd == 0)
                    {
                        //Main.NewText("You picked the wrong house, fool!");                       
                        //item.damage = (10 + level * dmgscale) * 3;
                        item.shoot = mod.ProjectileType("Slagbuster");
                        item.shootSpeed = 5.5f;
                        
                        Main.PlaySound(SoundID.Item20, player.Center);
                        Main.PlaySound(SoundID.Item62, player.Center);
                        Main.PlaySound(SoundID.Item68, player.Center);

                        for (int dustcounter = 100; dustcounter >= 0; dustcounter--)
                        {
                            float yvel = 0;
                            float xvel = 0;
                            float hyp = 0;
                            hyp = Main.rand.Next(0, 70) * 0.1f;
                            xvel = Main.rand.Next(-400, 400) * .01f;
                            if (Main.rand.Next(2) == 0)
                            {
                                yvel = (float)Math.Sqrt(hyp - xvel * xvel);
                            }
                            else
                            {
                                yvel = (float)Math.Sqrt(hyp - xvel * xvel) * -1;
                            }
                            Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), mod.DustType("Sworddust2"), new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(4, 6) * 0.1f);
                        }

                            cd = 210;
                        
                        return true;   
                        
                    }
                    else
                    {
                        
                        return false;
                    }
                }
                if (ab2 == 2)
                {
                    if (cd == 0)
                    {
                        Projectile.NewProjectile(new Vector2(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY), new Vector2(0, 10), mod.ProjectileType("Slagburst"), (10 + level * dmgscale) / 3, 0, Main.myPlayer);
                        Main.PlaySound(SoundID.Item42, player.Center);
                        item.shoot = 0;
                        cd = 1800;
                        
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if(ab2 == 3)
                {
                    if (cd == 0)
                    {
                    Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(0, 0), mod.ProjectileType("Slagward"), 10 + level * dmgscale * 5, 5, Main.myPlayer);
                    Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(0, 0), mod.ProjectileType("Slagward2"), 10 + level * dmgscale * 5, 5, Main.myPlayer);
                    Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(0, 0), mod.ProjectileType("Slagward3"), 10 + level * dmgscale * 5, 5, Main.myPlayer);
                    Projectile.NewProjectile(new Vector2(player.MountedCenter.X + 2, player.MountedCenter.Y + 2), new Vector2(0, -1), mod.ProjectileType("Shield"),0, 0, Main.myPlayer);
                        player.AddBuff(mod.BuffType("Slagward"), 900);
                        for (int dustcounter = 0;dustcounter <= 30; dustcounter++)
                        {
                            //Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), mod.DustType("Slagdust2"), new Vector2(0,0), 0, default(Color), Main.rand.Next(10, 20) * 0.1f);
                            Dust dust = Dust.NewDustDirect(player.Center, 0, 0, mod.DustType<Dusts.Slagdust2>(), Scale: 1.009f * 0.1f);
                            dust.customData = player;
                        }

                        for (int dustcounter = 0; dustcounter <= 80; dustcounter++)
                        {
                            Dust.NewDustPerfect(new Vector2(player.Center.X + (float)Math.Cos((Math.PI * 2 / 80) * dustcounter) * 80 , player.Center.Y + (float)Math.Sin((Math.PI * 2 / 80)*dustcounter) * 80 ), mod.DustType("Sworddust3"), new Vector2(Main.rand.Next(0 , 10) * .01f,-1), 0, default(Color), Main.rand.Next(10, 20) * 0.1f);
                            
                        }



                        Main.PlaySound(SoundID.Item62, player.Center);
                    cd = 1800;
                    return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                    
                   
                }
                else
                {
                    
                    return true;
                }
                //  if (ab2 == 3... etc. etc.
            }
            else
            {
                item.damage = 10 + level * dmgscale;
                item.shoot = 0;
                return true;
            }
            
        }

        public override bool Shoot(Player player,
ref Vector2 position,
ref float speedX,
ref float speedY,
ref int type,
ref int damage,
ref float knockBack)
        {
            if (item.shoot == mod.ProjectileType("Slagbuster"))
            {
                damage = (10 + level * dmgscale) * 3;
                return true;
            }
            else
            {
                return false;
            }
        }


  

        //Choice code needed here in the future, for now 1 ability
        public override bool CanRightClick()
        {

                return true;
            
        }
        public override void RightClick(Player player)
        {
            Main.PlaySound(SoundID.Item79, player.Center);//ui popup code here 
            if (level >= 2 && !Upgradeui.visible)
            {
                Upgradeui.visible = true;
                
                //set values needed to get the correct UI here

            }

                /*if (level >= 2)
                {
                    //etc etc
                    item.stack++;
                    ab1 = 1;
                }
                if (level >= 5 && ab2 == 0)
                {

                    ab2 = 1;
                }
                 //ability toggling code for test purposes untill a GUI works properly
                else if (level >= 5 && ab2 == 1)
                {

                    ab2 = 2;
                }

                else if (level >= 5 && ab2 == 2)
                {

                    ab2 = 3;
                }

                else if (level >= 5 && ab2 == 3)
                {

                    ab2 = 1;
                }

                if (level >= 8)
                {
                    ab3 = 1;
                }*/

                if ( level <= 1)
            {
                Main.NewText("Abilities unlock at level 2!");
                
            }
            item.stack++;
        }

        public override void HoldItem(Player player)
        {
            
            CDUI.ability = ab2;


            if (ab3 == 1)
            {
               Projectile.NewProjectile(new Vector2(player.position.X, player.position.Y), new Vector2(0, 0), mod.ProjectileType("Slagaura"), 0, 0, Main.myPlayer);         
            }
                
        }


        public override TagCompound Save()
        {
            return new TagCompound
            {
                {"exp", exp},
                {"expreq", expreq},
                {"level", level},
                {"ab1", ab1}, // ability choices are saved and loaded
                {"ab2", ab2},
                {"ab3", ab3}
            };
        }

        public override void Load(TagCompound tag)
        {
            exp = tag.GetInt("exp");
            expreq = tag.GetInt("expreq");
            level = tag.GetInt("level");
            ab1 = tag.GetInt("ab1");
            ab2 = tag.GetInt("ab2");
            ab3 = tag.GetInt("ab3");
            Leveler();
            
        }

    }
    public class Cooldown : ModPlayer
    {
        public override void PreUpdate()
        {
            if(Testsword.cd > 0)
            {
                Testsword.cd--;

                if (Testsword.cd == 0)
                {
                    for (int dustcounter = 25; dustcounter >0; dustcounter--)
                    {
                        Dust.NewDust(new Vector2(player.Center.X - (player.width) / 2, player.Center.Y - player.height / 2), player.width, player.height, 6, player.velocity.X, player.velocity.Y);
                        Main.PlaySound(SoundID.Item20, player.Center);
                        Main.PlaySound(SoundID.Item74, player.Center);
                    }
                }
            }
        }



    }
    public class UImanager : ModPlayer
    {
   
        public override void PostUpdate()
        {
            Item holding = player.HeldItem;
            if (player.HeldItem.type == mod.ItemType("Testsword"))
            {
                if (player.HeldItem != holding)
                {
                    CDUI.visible = false;
                }
                CDUI.visible = true;
            }
  

            else
            {
                CDUI.visible = false;
            }


            if (Upgradeui.visible)
            {
                if(player.controlHook)
                {
                    Upgradeui.visible = false;
                }
            }


        }
    }
    class Testswordmax : Testsword
    {
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            level = 10;
        }
    }



    }
