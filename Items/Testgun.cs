using System;
using System.Collections.Generic;
using ItemLevelTest.Dusts;
using ItemLevelTest.Projectiles;
using ItemLevelTest.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace ItemLevelTest.Items
{
    class Testgun : ModItem
    {
        public int exp = 0; //the current exp of the item (this is saved)why are you
        public int expRequired = 50; //the exp required to reach the next level, this value sets the base

        public int level = 0; //the item's current level (this is saved)
        public int dmgScale = 6; //changes the damage gain per level
        int critScale = 2; //changes the critical strike chance gain per level
        int spdScale = 1;
        float kbScale = 0.5f; //changes the knockback gained per level
        const float expScale = 1.4f; //Changes the multiplier for the amount of exp required for the next level after the previous

        //ability variables
        public int ab1 = 0; //passive
        public int ab2 = 0; //active
        public int ab3 = 0; //ultimate
        public bool VFXstate = true; //VFX toggle

        public NPC snipertarget = null;
        public int hits;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HX-17 Sigma");
            Tooltip.SetDefault("Gains 10% of damage dealt as EXP" + "\n\n\n\n\n\n\n\n\n\n\n\n");

            ItemID.Sets.ItemNoGravity[item.type] = true; //makes the item float when thrown

        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.useStyle = 5;
            item.useAnimation = 27;
            item.useTime = 27;
            item.knockBack = 6.5f;
            item.width = 94;
            item.height = 38;
            item.scale = 1f;
            item.rare = -12;
            item.crit = 10;
            item.value = Item.sellPrice(silver: 10);
            item.useAmmo = AmmoID.Bullet;
            item.shoot = 10;


            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item91;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (ab1 != 1)
            {
                flat += level * dmgScale;
            }
            else
            {
                flat += level * (dmgScale/2);
            }
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += level * critScale;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback += level * kbScale;
        }

        public override bool AltFunctionUse(Player player) //allows right click to be used
        {
            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, 1.0f, 1.0f, .7f);
            Main.PlaySound(SoundID.Pixie, item.Center);
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width), item.Center.Y - item.height), item.width * 2, item.height * 2, mod.DustType("Gundust"));
                Dust.NewDust(new Vector2(item.Center.X - (item.width) / 2, item.Center.Y - (item.height) / 2), item.width, item.height, mod.DustType("Gundust"));
            }
        }

        public static bool flash;


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, -2);
        }

        bool loaded = false;
        bool consumed = false;
        public override bool CanUseItem(Player player)
        {
            loaded = false;
            consumed = false;
            if (player.altFunctionUse != 2)
            {
                for (int z = 54; z <= 57; z++)
                {
                    if (player.inventory[z].ammo == AmmoID.Bullet && !consumed)
                    {
                        if (player.inventory[z].stack > 0)
                        {
                            loaded = true;
                            if (player.inventory[z].type != ItemID.EndlessMusketPouch)
                            {
                                player.inventory[z].stack--;
                            }
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
                        if (player.inventory[z].ammo == AmmoID.Bullet && !consumed)
                        {
                            if (player.inventory[z].stack > 0)
                            {
                                loaded = true;

                                if(player.inventory[z].type != ItemID.EndlessMusketPouch)
                                {
                                    player.inventory[z].stack--;
                                }
                                
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
                          

            if(loaded)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float R = 0;
            if (loaded && player.altFunctionUse != 2)
            {
                if (ab1 != 1)
                {
                    float x = (Main.screenPosition.X + Main.mouseX - 20) - player.position.X;
                    float y = (Main.screenPosition.Y + Main.mouseY - 20) - player.position.Y;

                    if (ab1 != 2)
                    {
                        R = (26);
                    }
                    else
                    {
                        R = (22);
                    }

                    float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
                    float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);



                    int index = Projectile.NewProjectile(player.Center, new Vector2(xvel, yvel), mod.ProjectileType("Testbullet"), (40 + level * dmgScale), 1f + level * kbScale, Main.myPlayer);
                    Testbullet proj = Main.projectile[index].modProjectile as Testbullet;
                    proj.instance = this;

                    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(xvel, yvel)) * 68f;
                    if (Collision.CanHit(Main.projectile[index].position, 0, 0, Main.projectile[index].position + muzzleOffset, 0, 0))
                    {
                        Main.projectile[index].position += muzzleOffset;
                    }

                }

                if (ab1 == 1)
                {
                    for (int k = 0; k <= 2; k++)
                    {
                        float x = (Main.screenPosition.X + Main.mouseX - 20) - player.position.X;
                        float y = (Main.screenPosition.Y + Main.mouseY - 20) - player.position.Y;

                        R = (20);

                        float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
                        float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);

                        int index = Projectile.NewProjectile(player.Center, new Vector2(xvel + Main.rand.Next(-3, 3), yvel + Main.rand.Next(-3, 3)), mod.ProjectileType("Testbullet"), (30 + level * (dmgScale / 2)), 1f + level * kbScale, Main.myPlayer);
                        Testbullet proj = Main.projectile[index].modProjectile as Testbullet;
                        proj.instance = this;

                        Vector2 muzzleOffset = Vector2.Normalize(new Vector2(xvel, yvel)) * 68f;
                        if (Collision.CanHit(Main.projectile[index].position, 0, 0, Main.projectile[index].position + muzzleOffset, 0, 0))
                        {
                            Main.projectile[index].position += muzzleOffset;
                        }
                    }

                }

            }
                return false;
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

            if (item.useTime - spdScale >= 2) //adjusts the item's usetime and animation, ensures that it will never drop below 2 (this causes buggy animation)
            {
                item.useTime = 27 - level * spdScale;
                item.useAnimation = 27 - level * spdScale;
            }
            else
            {
                item.useTime = 2;
                item.useAnimation = 2;
            }

            item.value = 10000 + level * 1000; //adjusts the item's gold value with level

            //Handles the dust that spawns on level up
            {
                for (int dustcounter = 0; dustcounter <= 35; dustcounter++)
                {
                    Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, mod.DustType("Leveldust"));
                }
            }

            //Handles chat text for the appropriate levels
            if (level == 2)
            {
                Main.NewText("Config choice available! Right click in your inventory to select an ability.");
            }

            if (level == 5)
            {
                Main.NewText("Flash ability choice available! Right click in your inventory to select an ability.");
            }

            if (level == 8)
            {
                Main.NewText("Ultimate ability choice available! Right click in your inventory to select an ability.");
            }


        }


        public override void HoldItem(Player player)
        {
            Flashhandler.instance = this;
            CDUI.ability = ab2;//sets the ability variable in the cooldown UI to display the correct icon
            CDUI.guninstance = this;

            CDUI.swordinstance = null;
            CDUI.spearinstance = null;
            CDUI.bowinstance = null;

            if ((ab2 != 1 && Flashhandler.cooldown == 0) || (ab2 == 3 && Flashhandler.cooldown <= 90))
            {
                for (float dustcounter = 0; dustcounter <= (float)(Math.PI * 2); dustcounter += (float)(Math.PI * 2) / 100)
                {
                    float theta = (dustcounter);
                    float dustx = (300 * (float)Math.Cos(theta));
                    float dusty = (300 * (float)Math.Sin(theta));
                    if (Main.rand.Next(14) == 0 && !flash)
                    {
                        Dust.NewDustPerfect(new Vector2((player.position.X + player.width / 2) + dustx, (player.position.Y + player.height / 2) + dusty), mod.DustType("Gundust2"), null, 0, new Color(255, 255, 255));
                    }
                }
            }
            if(ab2 == 1 && Flashhandler.cooldown == 0) 
            {
                for (float dustcounter = 0; dustcounter <= (float)(Math.PI * 2); dustcounter += (float)(Math.PI * 2) / 100)
                {
                    float theta = (dustcounter);
                    float dustx = (400 * (float)Math.Cos(theta));
                    float dusty = (400 * (float)Math.Sin(theta));
                    if (Main.rand.Next(14) == 0 && !flash)
                    {
                        Dust.NewDustPerfect(new Vector2((player.position.X + player.width / 2) + dustx, (player.position.Y + player.height / 2) + dusty), mod.DustType("Gundust2"), null, 0, new Color(255, 255, 255));
                    }
                }
            }

            if ((Main.mouseRight && Flashhandler.cooldown == 0) || (Main.mouseRight && Flashhandler.cooldown <= 90 && ab2 == 3))
            {
                if (Main.mouseRightRelease)
                {
                    flash = true;
                    Main.PlaySound(SoundID.Item1, player.Center);
                }
            }

            int rad = 0;
            if(snipertarget != null)
            {
                if(snipertarget.width >= snipertarget.height && snipertarget.width <= 100)
                {
                    rad = snipertarget.width;
                }

                else if (snipertarget.width < snipertarget.height && snipertarget.height <= 100)
                {
                    rad = snipertarget.height;
                }
                else
                {
                    rad = 100;
                }
                

                for (float dustcounter = 0; dustcounter <= (float)(Math.PI * 2); dustcounter += (float)(Math.PI * 2) / 50)
                {
                    float theta = (dustcounter);
                    float dustx = (rad * (float)Math.Cos(theta));
                    float dusty = (rad * (float)Math.Sin(theta));
                    if (Main.rand.Next(8) == 0 && ab1 == 2)
                    {
                        Dust dus = Dust.NewDustPerfect(snipertarget.Center + new Vector2(dustx, dusty), mod.DustType("Gundust4"), null, 0, new Color(255, 255, 255));
                        Dust dus2 = Dust.NewDustPerfect(new Vector2(snipertarget.Center.X + (float)Math.Sin(theta) * rad * 1.5f, snipertarget.Center.Y), mod.DustType("Gundust4"), null, 0, new Color(255, 255, 255));
                        Dust dus3 = Dust.NewDustPerfect(new Vector2(snipertarget.Center.X, snipertarget.Center.Y + (float)Math.Sin(theta) * rad * 1.5f), mod.DustType("Gundust4"), null, 0, new Color(255, 255, 255));
                        dus.customData = snipertarget;
                        dus2.customData = snipertarget;
                        dus3.customData = snipertarget;
                    }
                }

                if(snipertarget.life <= 0)
                {
                    snipertarget = null;
                    hits = 0;
                }
            }

        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "Tooltip1") //Description
                {
                    line.text = "Right click to flash";
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
                if (line.mod == "Terraria" && line.Name == "Tooltip11") //These lines show exp and level
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
                if (line.mod == "Terraria" && line.Name == "Tooltip12")
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
                if (line.mod == "Terraria" && line.Name == "Tooltip8") //these lines show abilities
                {
                    if (ab1 == 0 && level <= 2)
                    {
                        line.text = "Config: default";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab1 == 0 && level >= 2)
                    {
                        line.text = "Config: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab1 == 1)
                    {
                        line.text = "Config: Shotgun (" + level * 1 + "-" + ((level * 1) + 1) + "s)";
                        line.overrideColor = new Color(250, 250, 200);
                    }
                    else if (ab1 == 2)
                    {
                        line.text = "Config: Sniper (+" + (10 + level) + "/ hit)";
                        line.overrideColor = new Color(250, 250, 200);
                    }
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip9")
                {
                    if (ab2 == 0 && level <= 5)
                    {
                        line.text = "Flash: Default";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab2 == 0 && level >= 5)
                    {
                        line.text = "Flash: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab2 == 1)
                    {
                        line.text = "Flash: Angel's Wings (" + (20 + level) + " HP)";
                        line.overrideColor = new Color(250, 250, 200);
                    }
                    else if (ab2 == 2)
                    {
                        line.text = "Flash: Piercing Bolt (" + (250 + level * 10) + ")";
                        line.overrideColor = new Color(250, 250, 200);
                    }
                    else if (ab2 == 3)
                    {
                        line.text = "Flash: Daredevil";
                        line.overrideColor = new Color(250, 250, 200);
                    }
                }

                if (line.mod == "Terraria" && line.Name == "Tooltip10")
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
                        line.text = "Ultimate: DEV/NULL";
                        line.overrideColor = new Color(255, 255, 40 + (Main.DiscoB / 2));
                    }
                    else if (ab3 == 2)
                    {
                        line.text = "Ultimate: DEV/NULL";
                        line.overrideColor = new Color(255, 255, 40 + (Main.DiscoB / 2));
                    }
                }
                if (line.mod == "Terraria" && line.Name == "ItemName") //this edits the item's name's color
                {
                    line.overrideColor = new Color(255, 255, 40 + (Main.DiscoB / 2));
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip0")
                {
                    line.text = "Gains 10% of damage dealt as EXP";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip2")
                {
                    if(ab2 != 1)
                    {
                        line.text = "38 block flash range";
                    }
                    else
                    {
                        line.text = "50 block flash range";
                    }
                    line.overrideColor = new Color(255, 218, 75);
                }
            }
        }

        public override bool CanRightClick()//enables right clicking
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            Main.PlaySound(SoundID.Item79, player.Center); //sound FX
            if (!Upgradeui.visible) //only if the UI isnt already opened
            {
                Upgradeui.ab1 = ab1;
                Upgradeui.ab2 = ab2;
                Upgradeui.ab3 = ab3;
                Upgradeui.level = level;
                Upgradeui.swordinstance = null;
                Upgradeui.spearinstance = null;
                Upgradeui.bowinstance = null;

                Upgradeui.guninstance = this;
                Upgradeui.visible = true; //open the UI
            }
            item.stack++;//make sure the item dosent just dissappear 
        }

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        //ensures the item will not have a prefix (this breaks stuff)
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return 0;
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
                {"ab3", ab3},
                {"vfx", VFXstate}
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
            VFXstate = tag.GetBool("vfx");
            Leveler();
        }

    }
    class Flashhandler : ModPlayer
    {
        int timer = 0;
        float storedtime = 0;
        float storex = 0;
        float storey = 0;
        bool stored;
        public static Testgun instance = null;
        public static int cooldown;
        public override void PreUpdate()
        {

            if (player.wingTime == 0 && !Testgun.flash)
            {
                storedtime = 0;
            }

            if (timer > 0)
            {
                timer--;
            }

            if (cooldown > 0)
            {
                cooldown--;
            }

            if (Testgun.flash)
            {
                if (!stored)
                {
                    storex = Main.mouseX;
                    storey = Main.mouseY;
                    stored = true;
                }
                if (instance.ab2 == 0)
                {
                    if (timer == 0)
                    {
                        timer = 10;
                        cooldown = 120;
                    }

                    if (timer > 1)
                    {
                        float x = (Main.screenPosition.X + storex) - player.position.X;
                        float y = (Main.screenPosition.Y + storey) - player.position.Y;

                        float R = (32);

                        float xend = (R * x) / (float)Math.Sqrt(x * x + y * y);
                        float yend = (R * y) / (float)Math.Sqrt(x * x + y * y);

                        player.gravity = 1;
                        player.maxFallSpeed = 999;

                        if (player.wingTime > 0)
                        {
                            storedtime = player.wingTime;
                        }

                        player.wingTime = 0;


                        player.velocity = new Vector2(0, 0);
                        player.velocity = new Vector2(xend, yend);
                        for (int d = 0; d < 5; d++)
                        {
                            Dust.NewDust(player.Center - new Vector2(15, 15), 30, 30, mod.DustType("Gundust"));
                        }

                    }

                    if (timer == 1)
                    {
                        player.wingTime = storedtime;
                        storedtime = 0;

                        Testgun.flash = false;
                        player.velocity = new Vector2(0, 0);
                        player.gravity = 0.3f;
                        player.maxFallSpeed = 10.01f;

                    }
                }

                if (instance.ab2 == 1)
                {
                    if (timer == 0)
                    {
                        timer = 14;
                        cooldown = 240;
                    }

                    if (timer > 1)
                    {
                        float x = (Main.screenPosition.X + storex) - player.position.X;
                        float y = (Main.screenPosition.Y + storey) - player.position.Y;

                        float R = (32);

                        float xend = (R * x) / (float)Math.Sqrt(x * x + y * y);
                        float yend = (R * y) / (float)Math.Sqrt(x * x + y * y);

                        player.gravity = 1;
                        player.maxFallSpeed = 999;

                        if (player.wingTime > 0)
                        {
                            storedtime = player.wingTime;
                        }

                        player.wingTime = 0;


                        player.velocity = new Vector2(0, 0);
                        player.velocity = new Vector2(xend, yend);
                        for (int d = 0; d < 5; d++)
                        {
                            Dust.NewDust(player.Center - new Vector2(15, 15), 30, 30, mod.DustType("Gundust"));
                        }

                    }

                    if (timer == 1)
                    {
                        player.wingTime = storedtime;
                        storedtime = 0;

                        Testgun.flash = false;
                        player.velocity = new Vector2(0, 0);
                        player.gravity = 0.3f;
                        player.maxFallSpeed = 10.01f;

                        int proj = Projectile.NewProjectile(player.Center, new Vector2(0,0), mod.ProjectileType("Healblast"), 0, 0, Main.myPlayer);
                        Healblast proj2 = Main.projectile[proj].modProjectile as Healblast;
                        proj2.instance = instance;

                    }
                }

                if (instance.ab2 == 2)
                {
                    if (timer == 0)
                    {
                        timer = 10;
                        cooldown = 180;
                        Projectile.NewProjectile(player.Center, new Vector2(0, 0), mod.ProjectileType("Flashdamage"), 250 + instance.level * 10, 0, Main.myPlayer);
                        player.immune = true;
                    }

                    if (timer > 1)
                    {
                        float x = (Main.screenPosition.X + storex) - player.position.X;
                        float y = (Main.screenPosition.Y + storey) - player.position.Y;

                        float R = (32);

                        float xend = (R * x) / (float)Math.Sqrt(x * x + y * y);
                        float yend = (R * y) / (float)Math.Sqrt(x * x + y * y);

                        player.gravity = 1;
                        player.invis = true;
                        player.maxFallSpeed = 999;
                        player.immune = true;

                        if (player.wingTime > 0)
                        {
                            storedtime = player.wingTime;
                        }

                        player.wingTime = 0;


                        player.velocity = new Vector2(0, 0);
                        player.velocity = new Vector2(xend, yend);
                        for (int d = 0; d < 5; d++)
                        {
                            Dust.NewDust(player.Center - new Vector2(15, 15), 30, 30, mod.DustType("Gundust"));
                        }
                        for (int d = 0; d < 10; d++)
                        {
                            Dust.NewDust(player.Center - new Vector2(5, 5), 10, 10, mod.DustType("Gundust"));
                        }

                    }

                    if (timer == 1)
                    {
                        player.invis = false;
                        player.wingTime = storedtime;
                        storedtime = 0;
                        

                        Testgun.flash = false;
                        player.velocity = new Vector2(0, 0);
                        player.gravity = 0.3f;
                        player.maxFallSpeed = 10.01f;
                        player.immune = false;

                    }
                }

                if (instance.ab2 == 3)
                {
                    if (timer == 0)
                    {
                        timer = 10;
                        cooldown += 90;
                    }

                    if (timer > 1)
                    {
                        float x = (Main.screenPosition.X + storex) - player.position.X;
                        float y = (Main.screenPosition.Y + storey) - player.position.Y;

                        float R = (32);

                        float xend = (R * x) / (float)Math.Sqrt(x * x + y * y);
                        float yend = (R * y) / (float)Math.Sqrt(x * x + y * y);

                        player.gravity = 1;
                        player.maxFallSpeed = 999;

                        if (player.wingTime > 0)
                        {
                            storedtime = player.wingTime;
                        }

                        player.wingTime = 0;


                        player.velocity = new Vector2(0, 0);
                        player.velocity = new Vector2(xend, yend);
                        for (int d = 0; d < 5; d++)
                        {
                            Dust.NewDust(player.Center - new Vector2(15, 15), 30, 30, mod.DustType("Gundust"));
                        }

                    }

                    if (timer == 1)
                    {
                        player.wingTime = storedtime;
                        storedtime = 0;

                        Testgun.flash = false;
                        player.velocity = new Vector2(0, 0);
                        player.gravity = 0.3f;
                        player.maxFallSpeed = 10.01f;

                    }
                }


            }
            else
            {
                stored = false;
            }
        }
    }
}
