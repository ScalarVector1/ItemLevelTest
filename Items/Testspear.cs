using System;
using System.Collections.Generic;
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
    class Testspear : ModItem
    {
        public int exp = 0; //the current exp of the item (this is saved)why are you
        public int expRequired = 50; //the exp required to reach the next level, this value sets the base

        public static int energy = 0;
        public static int maxenergy = 1000;

        public int level = 0; //the item's current level (this is saved)
        int dmgScale = 8; //changes the damage gain per level
        int critScale = 1; //changes the critical strike chance gain per level
        float kbScale = 0.5f; //changes the knockback gained per level
        const float expScale = 1.4f; //Changes the multiplier for the amount of exp required for the next level after the previous

        //ability variables
        public int ab1 = 0; //passive
        public int ab2 = 0; //active
        public int ab3 = 0; //ultimate
        public bool VFXstate = true; //VFX toggle

        public static int cd = 0; //cooldown

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spear Prototype");
            Tooltip.SetDefault("Gains 10% of damage dealt as EXP" + "\n\n\n\n\n\n\n\n\n\n\n");

            ItemID.Sets.ItemNoGravity[item.type] = true; //makes the item float when thrown

        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.useStyle = 5;
            item.useAnimation = 20;
            item.useTime = 20;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = 5;
            item.value = Item.sellPrice(silver: 10);

            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false; 

            item.UseSound = SoundID.Item1;
        }
        public
            override bool AltFunctionUse(Player player) //allows right click to be used
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                return false;
            }

            float x = (Main.screenPosition.X + Main.mouseX) - player.position.X;
            float y = (Main.screenPosition.Y + Main.mouseY) - player.position.Y;

            float R = (10); //the number here is the base velocity!!

            float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
            float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);

            
            int visualproj = Projectile.NewProjectile(new Vector2(player.MountedCenter.X - xvel * 7, player.MountedCenter.Y - yvel * 7), new Vector2(xvel, yvel), mod.ProjectileType("Testspearproj"), 1, 1); //visual projectile
            int proj = Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(xvel, yvel), mod.ProjectileType("Testspearproj2"), 40 + level * dmgScale, 1, Main.myPlayer); //hitbox projectile
            Testspearproj visualproj2 = Main.projectile[visualproj].modProjectile as Testspearproj;
            Testspearproj2 proj2 = Main.projectile[proj].modProjectile as Testspearproj2;
            proj2.instance = this;
            visualproj2.instance = this;

            return player.ownedProjectileCounts[proj] < 1;


            }

        int timer = 0;
        public static bool casting = false;

        public override void HoldItem(Player player)
        {
            CDUI.ability = ab2;//sets the ability variable in the cooldown UI to display the correct icon
            CDUI.spearinstance = this;

            CDUI.swordinstance = null;
            CDUI.bowinstance = null;
            CDUI.guninstance = null;

            if (player.HeldItem.modItem == this)
            {
                if (Main.mouseRight && energy > 0)
                {
                    if (ab2 == 1)
                    {


                        if (timer > 1)
                        {
                            timer--;
                        }
                        if (timer == 0)
                        {
                            timer = 61;
                            casting = true;
                        }
                        if (timer == 1)
                        {

                            float x = Main.rand.Next(-50, 50);
                            float y = Main.rand.Next(-50, 50);

                            float R = (10); //the number here is the base velocity!!

                            float xvel = (R * x) / (float)Math.Sqrt(x * x + y * y);
                            float yvel = (R * y) / (float)Math.Sqrt(x * x + y * y);


                            int visualproj = Projectile.NewProjectile(new Vector2(player.MountedCenter.X - xvel * 7, player.MountedCenter.Y - yvel * 7), new Vector2(xvel, yvel), mod.ProjectileType("Testspearproj4"), 1, 1); //visual projectile
                            int proj = Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(xvel, yvel), mod.ProjectileType("Testspearproj3"), 40 + level * dmgScale, 1, Main.myPlayer); //hitbox projectile
                            Main.PlaySound(SoundID.Item1, player.Center);
                            energy -= 20;
                            timer = 5;
                        }
                    }
                    if(ab2 == 2)
                    {
                        if (timer > 1)
                        {
                            timer--;
                        }
                        if (timer == 0)
                        {
                            timer = 61;
                            casting = true;
                            for(int z = 1; z <= 8; z++)
                            {
                                Projectile.NewProjectile(player.MountedCenter - new Vector2(0, 0), new Vector2(0, 0), mod.ProjectileType("Spearwhirl" + z), 50 + (level * dmgScale / 2), 0, Main.myPlayer);
                            }
                        }
                        if (timer >= 1)
                        {
                            energy -= 2;

                        }

                    }
                    
                }
                else
                {
                    timer = 0;
                    casting = false;
                }
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
                Main.NewText("Passive ability choice available! Right click in your inventory to select an ability.");
            }

            if (level == 5)
            {
                Main.NewText("Channeled ability choice available! Right click in your inventory to select an ability.");
            }

            if (level == 8)
            {
                Main.NewText("Ultimate ability choice available! Right click in your inventory to select an ability.");
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
                Upgradeui.guninstance = null;
                Upgradeui.bowinstance = null;

                Upgradeui.spearinstance = this;
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

    public class Energy : ModPlayer//cooldown handler
    {
        public override void PreUpdate()
        {
            if(Testspear.energy < Testspear.maxenergy && !Testspear.casting && !Main.mouseRight)
            {
                Testspear.energy++;
            }
            if(Testspear.energy > Testspear.maxenergy)
            {
                Testspear.energy = Testspear.maxenergy;
            }
        }
    }
}