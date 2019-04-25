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

namespace ItemLevelTest.Items
{
    class Koranithus : ModItem
    {
        int exp = 0; //the current exp of the item (this is saved)why are you
        int expRequired = 50; //the exp required to reach the next level, this value sets the base
        public int level = 0; //the item's current level (this is saved)
        int dmgScale = 5; //changes the damage gain per level
        int spdScale = 2; //changes the usetime reduction per level
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
        const int slagbuster = 1;
        const int slagburst = 2;
        const int slagward = 3;

        const int burningstrike = 1;
        const int firebolts = 2;

        const int cinderaura = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Koranithus");
            Tooltip.SetDefault("Gains 10% of damage dealt as EXP" + "\n\n\n\n\n\n\n\n\n\n\n");

            ItemID.Sets.ItemNoGravity[item.type] = true; //makes the item float when thrown

        }

        //handles effects of the dropped item
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, 2, .7f, .6f);
            Main.PlaySound(SoundID.Pixie, item.Center);
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(new Vector2(item.Center.X - (item.width), item.Center.Y - item.width), item.width * 2, item.width * 2, mod.DustType("Sworddust"));
                Dust.NewDust(new Vector2(item.Center.X - (item.width) / 2, item.Center.Y - (item.width) / 2), item.width, item.width, mod.DustType("Sworddust"));
            }
        }

        public override void SetDefaults() //The values set here are the base level 1 values. remember that useTime is subtracted from rather than added to.
        {
            item.damage = 10;
            item.melee = true;
            item.width = 62;
            item.height = 62;
            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = 1;
            item.knockBack = 1f;
            item.value = 10000;
            item.rare = -12;
            item.crit = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
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

        public void Leveler() //the method that adjusts the properties of the item with the level of the weapon appropriately, also handles events that occur on levelup
        {
            Player player = Main.player[Main.myPlayer];

            if (item.useTime - 2 >= 2) //adjusts the item's usetime and animation, ensures that it will never drop below 2 (this causes buggy animation)
            {
                item.useTime = 50 - level * spdScale;
                item.useAnimation = 50 - level * spdScale;
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

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (level < 10) //This section calculates the EXP gained on hitting an enemy, and adds it to the exp variable
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


            //this section handles the passive ability "burning strike"
            if (ab1 == burningstrike)
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(0, 60) + level * 60);
            }
             
            if (ab1 == firebolts)
            {
                if (Main.rand.Next(4) == 1)
                {
                    int Xoffset = 0;
                    Xoffset = Main.rand.Next(-100, 100);
                    Projectile.NewProjectile(new Vector2 ((target.position.X + 10) + Xoffset, target.position.Y - 1000), new Vector2((float)Xoffset/-50, 20), mod.ProjectileType("Firebolt"), (10 + level * dmgScale), 0, Main.myPlayer);
                }
            }
        }
        //Handles the "slash" effect dust seen ingame
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            float length = item.Size.Length() + 1;
            float r = player.direction == 1 ? ((float)Math.PI / 4) * -1 : (float)Math.PI * 5 / 4;
            Dust.NewDustPerfect(player.MountedCenter + new Vector2(length * (float)Math.Cos(player.itemRotation + r), length * (float)Math.Sin(player.itemRotation + r)), mod.DustType("Sworddust"));
            Dust.NewDustPerfect(player.MountedCenter + new Vector2(length * (float)Math.Cos(player.itemRotation + r - 0.12), length * (float)Math.Sin(player.itemRotation + r - 0.12)), mod.DustType("Sworddust"));
            Dust.NewDustPerfect(player.MountedCenter + new Vector2(length * (float)Math.Cos(player.itemRotation + r - 0.04), length * (float)Math.Sin(player.itemRotation + r - 0.04)), mod.DustType("Sworddust"));
        }

        public override bool AltFunctionUse(Player player) //allows right click to be used
        {
            return true;
        }

        public override bool CanUseItem(Player player) //This method handles all 3 active abilities
        {
            if (player.altFunctionUse == 2)
            {


                if (ab2 == none) //no ability unlocked or selected
                {
                    Main.NewText("Active abilities unlock at level 5!");
                    return false;
                }


                if (ab2 == slagbuster) //"slag buster" ability, fires a damaging explosive projectile
                {
                    if (cd == 0) //only possible if cooldown isnt active
                    {
                        item.shoot = mod.ProjectileType("Slagbuster"); //the item will fire the projectile
                        item.shootSpeed = 5.5f;

                        Main.PlaySound(SoundID.Item20, player.Center); //sound effects
                        Main.PlaySound(SoundID.Item62, player.Center);
                        Main.PlaySound(SoundID.Item68, player.Center);

                        for (int dustcounter = 100; dustcounter >= 0; dustcounter--) //dust and associated math
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

                        cd = 210; //adds 210 frames to the cooldown = 
                        return true;
                    }
                    else //occurs if cooldown is active
                    {
                        return false;
                    }
                }


                if (ab2 == slagburst)//handles the ability "slagburst"
                {
                    if (cd == 0)//only if cooldown is inactive
                    {
                        //Spawns the projectile at the mouse's position
                        Projectile.NewProjectile(new Vector2(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY), new Vector2(0, 10), mod.ProjectileType("Slagburst"), (10 + level * dmgScale) / 3, 0, Main.myPlayer);
                        Main.PlaySound(SoundID.Item42, player.Center); //sound effects
                        item.shoot = 0; //keeps the sword tiself from shooting anything
                        cd = 1800; //sets the cooldown to 1800 ticks (equal to 30 seconds)
                        return true;
                    }
                    else //occurs if cooldown is ative
                    {
                        return false;
                    }
                }


                if (ab2 == slagward) //handles the ability "Slag ward"
                {
                    if (cd == 0)//only if cooldown is inactive
                    {
                        //summons the 3 shield projectiles
                        Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(0, 0), mod.ProjectileType("Slagward"), 10 + level * dmgScale * 5, 5, Main.myPlayer);
                        Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(0, 0), mod.ProjectileType("Slagward2"), 10 + level * dmgScale * 5, 5, Main.myPlayer);
                        Projectile.NewProjectile(new Vector2(player.MountedCenter.X, player.MountedCenter.Y), new Vector2(0, 0), mod.ProjectileType("Slagward3"), 10 + level * dmgScale * 5, 5, Main.myPlayer);
                        //summons the shield VFX projectile (purely visual)
                        Projectile.NewProjectile(new Vector2(player.MountedCenter.X + 2, player.MountedCenter.Y + 2), new Vector2(0, -1), mod.ProjectileType("Shield"), 0, 0, Main.myPlayer);
                        player.AddBuff(mod.BuffType("Slagward"), 900);//Adds the associated defense buff to the player
                        //summons the ring of rocky dust
                        for (int dustcounter = 0; dustcounter <= 30; dustcounter++)
                        {
                            Dust dust = Dust.NewDustDirect(player.Center, 0, 0, mod.DustType<Dusts.Slagdust2>(), Scale: 1.009f * 0.1f);
                            dust.customData = player;
                        }
                        //summons the ring of fire dust around the shield VFX
                        for (int dustcounter = 0; dustcounter <= 80; dustcounter++)
                        {
                            Dust.NewDustPerfect(new Vector2(player.Center.X + (float)Math.Cos((Math.PI * 2 / 80) * dustcounter) * 80, player.Center.Y + (float)Math.Sin((Math.PI * 2 / 80) * dustcounter) * 80), mod.DustType("Sworddust3"), new Vector2(Main.rand.Next(0, 10) * .01f, -1), 0, default(Color), Main.rand.Next(10, 20) * 0.1f);
                        }
                        Main.PlaySound(SoundID.Item62, player.Center);//sound FX
                        cd = 1800;//adds 1800 ticks to the cooldown (equal to 30 seconds)
                        return true;
                    }
                    else//if cooldown is active
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                //corrects values for damage and shoot if the appropriate ability is not selected
                item.damage = 10 + level * dmgScale;
                item.shoot = 0;
                return true;
            }

        }

        //This method handles the firing of the slagbuster projectile if that ability is selected
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (item.shoot == mod.ProjectileType("Slagbuster"))
            {
                damage = (10 + level * dmgScale) * 3;
                return true;
            }
            else
            {
                return false;
            }
        }


        public override bool CanRightClick()//enables right clicking
        {
            return true;
        }


        public override void RightClick(Player player)
        {
            Main.PlaySound(SoundID.Item79, player.Center); //sound FX
            if (level >= 2 && !Upgradeui.visible) //only if the appropraite level and the UI isnt already opened
            {
                Upgradeui.visible = true; //open the UI
                Upgradeui.ab1 = ab1;
                Upgradeui.ab2 = ab2;
                Upgradeui.ab3 = ab3;
                Upgradeui.level = level;
                Upgradeui.instance = this;
            }
            if (level <= 1)//if underleveled
            {
                Main.NewText("Abilities unlock at level 2!");

            }
            item.stack++;//make sure the item dosent just dissappear 
        }


        public override void HoldItem(Player player)
        {
            CDUI.ability = ab2;//sets the ability variable in the cooldown UI to display the correct icon


            if (ab3 == cinderaura)//Handles the ultimate ability "Aura of Cinders"
            {
                Projectile.NewProjectile(new Vector2(player.position.X, player.position.Y), new Vector2(0, 0), mod.ProjectileType("Slagaura"), 0, 0, Main.myPlayer);
            }
        }


        //These methods hnadle saving and loading experience, level, and ability choices
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


        public override void AddRecipes() //the recipie of the item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Swordsteel1");
            recipe.AddIngredient(null, "Swordsoul1");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


        public override void OnCraft(Recipe recipe)
        {
            //handles the text displayed in chat
            Player player = Main.player[Main.myPlayer];
            string text = player.name + " Has Crafted Koranithus!";
            if (Main.netMode == 2) // Server
            {
                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 100f, 45f, 255f, 0, 0, 0);
            }
            else if (Main.netMode == 0) // Client
            {
                Main.NewText(text, new Color(255, 100, 45));
                Main.NewText("Your body goes numb...", new Color(100, 100, 100));
            }

            //handles dust spawned on craft
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
                //sound FX
                Main.PlaySound(SoundID.Item37, player.Center);
                Main.PlaySound(SoundID.Item45, player.Center);
            }
        }


        //This method handles the tooltips on the item
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
                    line.overrideColor = new Color(255, Main.DiscoG, 50);
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

        //ensures the item will not have a prefix (this breaks stuff)
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return 0;
        }

    }

    //These next classes handle things related to the sword that must be handled in other types of classes, such as cooldown and UI

    public class Cooldown : ModPlayer//cooldown handler
    {
        public override void PreUpdate()
        {
            if (Koranithus.cd > 0)
            {
                Koranithus.cd--;//ticks down cooldown every grame

                if (Koranithus.cd == 0)
                {
                    for (int dustcounter = 25; dustcounter > 0; dustcounter--)
                    {
                        //spawns dust on recharge
                        Dust.NewDust(new Vector2(player.Center.X - (player.width) / 2, player.Center.Y - player.height / 2), player.width, player.height, 6, player.velocity.X, player.velocity.Y);
                        //sound FX on recharge
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
            //this section handles the cooldown UI
            Item holding = player.HeldItem;
            if (player.HeldItem.type == mod.ItemType("Koranithus"))//when holding the sword
            {
                if (player.HeldItem != holding)//failsafe if holding nothing
                {
                    CDUI.visible = false;
                }
                CDUI.visible = true; //handles the visibility of the UI
            }
            else
            {
                CDUI.visible = false;
            }
        }
    }
}
