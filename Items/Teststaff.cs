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
    class Teststaff : ModItem
    {
        public int exp = 0; //the current exp of the item (this is saved)why are you
        public int expRequired = 50; //the exp required to reach the next level, this value sets the base

        public static int primal = 0;
        public static int maxprimal = 50;

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




        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff prototype");
            Tooltip.SetDefault("Gains 10% of damage dealt as EXP" + "\n\n\n\n\n\n\n\n\n\n\n\n");

            ItemID.Sets.ItemNoGravity[item.type] = true; //makes the item float when thrown

        }

        public override void SetDefaults()
        {
            item.damage = 20;
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
            item.shoot = 10;
            item.mana = 15;


            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item42;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (ab1 != 1)
            {
                flat += level * dmgScale;
            }
            else
            {
                flat += level * (dmgScale / 2);
            }
            if(ab2 == 1)
            {
                flat += primal;
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

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float R = 0;
            Main.PlaySound(SoundID.Item45);
            if (player.altFunctionUse != 2)
            {
                float x = (Main.screenPosition.X + Main.mouseX - 20) - player.position.X;
                float y = (Main.screenPosition.Y + Main.mouseY - 20) - player.position.Y;


                    R = (15);
                

                float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
                float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);

                int dam = (20 + level * dmgScale);

                if(ab1 == 1)
                {
                    dam += level * 5;
                }

                if(ab2 == 1)
                {
                    dam += primal;
                }


                    int index = Projectile.NewProjectile(player.Center, new Vector2(xvel, yvel), mod.ProjectileType("Testbolt"), dam , 1f + level * kbScale, Main.myPlayer);
                    Testbolt proj = Main.projectile[index].modProjectile as Testbolt;
                    proj.instance = this;
                

                if(ab1 == 1)
                {
                    int index2 = Projectile.NewProjectile(player.Center, new Vector2(xvel, yvel), mod.ProjectileType("Testbolt"), dam , 1f + level * kbScale, Main.myPlayer);
                    Testbolt proj2 = Main.projectile[index2].modProjectile as Testbolt;
                    proj2.instance = this;
                    proj2.invert = true;
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

        float timer = 0;
        int primaltimer = 0;
        public override void HoldItem(Player player)
        {
            CDUI.ability = ab2;//sets the ability variable in the cooldown UI to display the correct icon
            CDUI.staffinstance = this;

            CDUI.swordinstance = null;
            CDUI.spearinstance = null;
            CDUI.bowinstance = null;
            CDUI.guninstance = null;


            if (ab2 == 3)
            {
                {
                    float theta = (timer);
                    float dustx = (primal * (float)Math.Cos(theta));
                    float dusty = (primal * (float)Math.Sin(theta));
                    {
                        Dust.NewDustPerfect(new Vector2((player.position.X + player.width / 2) + dustx, (player.position.Y + player.height / 2) + dusty), mod.DustType("Staffdust2"), null, 0, new Color(255, 255, 255), 0.8f);
                    }
                    timer += (float)Math.PI / 50;
                    if(timer >= (float)Math.PI * 2)
                    {
                        timer = 0;
                    }
                }
            }

            if(primaltimer == 0)
            {
                primaltimer = 30;

                if(primal > 0)
                {
                    primal--;
                }
            }
            else
            {
                primaltimer--;
            }

            if (Main.mouseRight)
            {

                if (player.statMana > 0)
                {
                    player.statMana -= 3;
                    primal++;
                }

                if (primal >= maxprimal)
                {
                    primal = maxprimal;
                }
            }

            
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "Tooltip1") //Description
                {
                    line.text = "Right click to convert mana to twisted mana";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip3") //These lines show the stat growth and current added stat
                {
                    line.text = "Stat growth per level:";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip4")
                {
                    line.text = "+" + dmgScale + " Magic damage (" + dmgScale * level + ")";                
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
                        line.text = "Passive: NONE";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab1 == 0 && level >= 2)
                    {
                        line.text = "Passive: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab1 == 1)
                    {
                        line.text = "Passive: Spell Twister ("+(level * 5) +")";
                        line.overrideColor = new Color(220, 120, 250);
                    }
                    else if (ab1 == 2)
                    {
                        line.text = "Passive: Volatile Magic ("+(50 + level * 5) +")";
                        line.overrideColor = new Color(220, 120, 250);
                    }
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip9")
                {
                    if (ab2 == 0 && level <= 5)
                    {
                        line.text = "Twist: NONE";
                        line.overrideColor = new Color(70, 70, 70);
                    }
                    if (ab2 == 0 && level >= 5)
                    {
                        line.text = "Twist: AVAILABLE, right click to choose";
                        line.overrideColor = new Color(180, 180, 180);
                    }
                    else if (ab2 == 1)
                    {
                        line.text = "Twist: Mana Infusion (1/Mana)";
                        line.overrideColor = new Color(220, 120, 250);
                    }
                    else if (ab2 == 2)
                    {
                        line.text = "Twist: null";
                        line.overrideColor = new Color(220, 120, 250);
                    }
                    else if (ab2 == 3)
                    {
                        line.text = "Twist: null";
                        line.overrideColor = new Color(220, 120, 250);
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
                        line.overrideColor = new Color(180 + Main.DiscoB / 10, 100, 255);
                    }
                    else if (ab3 == 2)
                    {
                        line.text = "Ultimate: DEV/NULL";
                        line.overrideColor = new Color(180 + Main.DiscoB / 10, 100, 255);
                    }
                }
                if (line.mod == "Terraria" && line.Name == "ItemName") //this edits the item's name's color
                {
                    line.overrideColor = new Color(180 + Main.DiscoB / 10, 100, 255);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip0")
                {
                    line.text = "Gains 10% of damage dealt as EXP";
                    line.overrideColor = new Color(255, 218, 75);
                }
                if (line.mod == "Terraria" && line.Name == "Tooltip2")
                {
                    line.text = "500 twisted mana max";

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
                Upgradeui.guninstance = null;

                Upgradeui.staffinstance = this;
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
}