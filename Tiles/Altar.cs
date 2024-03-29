﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;
using ItemLevelTest.Projectiles;
using Terraria.ModLoader.IO;
using System;
using Terraria.Localization;

namespace ItemLevelTest.Tiles
{
    public class Altar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<AltarEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.newTile.Width = 13;//Width of the tile in blocks
            TileObjectData.newTile.Height = 5;//Height of the tile in blocks
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 18 };
            //TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);//should not have to change
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;//dont change
            TileObjectData.newTile.CoordinatePadding = 2;//dont change
            //TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);


            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Mysterious Altar");//Map name
            AddMapEntry(new Color(110, 175, 110), name);//Map color
            dustType = mod.DustType("Pickdustaltar");
            disableSmartCursor = true;


        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {

        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            //mod.GetTileEntity<AltarEntity>().Kill(i, j);
            fail = true;
        }

        public override void RightClick(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int left = i - (tile.frameX / 18);
            int top = j - (tile.frameY / 18);
            int index = mod.GetTileEntity<AltarEntity>().Find(left, top);

                if (index == -1)    
                {
                    return;
                }
                AltarEntity altarentity = (AltarEntity)TileEntity.ByID[index];
                Player player = Main.LocalPlayer; //needs to be changed for multiplayer adaptation
            if (!altarentity.crafting) { 
                //--------------------------------------------------------------
                if (player.HeldItem.type == mod.ItemType("Swordsteel1") && altarentity.slot1 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot1 = mod.ItemType("Swordsteel1");
                }

                if (player.HeldItem.type == mod.ItemType("Swordsoul1") && altarentity.slot2 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot2 = mod.ItemType("Swordsoul1");
                }

                if (player.HeldItem.type == mod.ItemType("Swordlog1") && altarentity.slot3 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot3 = mod.ItemType("Swordlog1");
                }

                if (player.HeldItem.type == mod.ItemType("Swordlogadd1") && altarentity.slot4 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot4 = mod.ItemType("Swordlogadd1");
                }

                //-------------------------------------------------------------

                if (player.HeldItem.type == mod.ItemType("Spear1") && altarentity.slot1 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot1 = mod.ItemType("Spear1");
                }

                if (player.HeldItem.type == mod.ItemType("Spear2") && altarentity.slot2 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot2 = mod.ItemType("Spear2");
                }

                if (player.HeldItem.type == mod.ItemType("Spear3") && altarentity.slot3 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot3 = mod.ItemType("Spear3");
                }

                if (player.HeldItem.type == mod.ItemType("Spear4") && altarentity.slot4 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot4 = mod.ItemType("Spear4");
                }

                //-------------------------------------------------------------

                if (player.HeldItem.type == mod.ItemType("Gun1") && altarentity.slot1 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot1 = mod.ItemType("Gun1");
                }

                if (player.HeldItem.type == mod.ItemType("Gun2") && altarentity.slot2 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot2 = mod.ItemType("Gun2");
                }

                if (player.HeldItem.type == mod.ItemType("Gun3") && altarentity.slot3 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot3 = mod.ItemType("Gun3");
                }

                if (player.HeldItem.type == mod.ItemType("Gun4") && altarentity.slot4 == 0)
                {
                    player.HeldItem.stack--;
                    altarentity.slot4 = mod.ItemType("Gun4");
                }


                if (player.HeldItem.type == 0)
                {
                    if (altarentity.slot1 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot1);
                        altarentity.slot1 = 0;
                        altarentity.swordItem1spawned = false;//find a better way to do this later
                        altarentity.spearItem1spawned = false;
                        altarentity.gunItem1spawned = false;
                    }

                    if (altarentity.slot2 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot2);
                        altarentity.slot2 = 0;
                        altarentity.swordItem2spawned = false;//find a better way to do this later
                        altarentity.spearItem2spawned = false;
                        altarentity.gunItem2spawned = false;
                    }

                    if (altarentity.slot3 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot3);
                        altarentity.slot3 = 0;
                        altarentity.swordItem3spawned = false;//find a better way to do this later
                        altarentity.spearItem3spawned = false;
                        altarentity.gunItem3spawned = false;
                    }

                    if (altarentity.slot4 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot4);
                        altarentity.slot4 = 0;
                        altarentity.swordItem4spawned = false;//find a better way to do this later
                        altarentity.spearItem4spawned = false;
                        altarentity.gunItem4spawned = false;
                    }
                }

            }
            
        }



        public class AltarEntity : ModTileEntity
        {
            private const int range = 100;
            public int slot1 = 0;
            public int slot2 = 0;
            public int slot3 = 0;
            public int slot4 = 0;
            public bool crafting = false;

            public bool swordmade = false;
            public bool bowmade = false;
            public bool staffmade = false;
            public bool spearmade = false;
            public bool gunmade = false;
            public bool orbmade = false;
            public bool pickmade = false;
            //----------------------------------------------
            public bool swordItem1spawned = false;
            public int thisSword1Index;
            
            public bool swordItem2spawned = false;
            public int thisSword2Index;

            public bool swordItem3spawned = false;
            public int thisSword3Index;

            public bool swordItem4spawned = false;
            public int thisSword4Index;
            //----------------------------------------------
            public bool spearItem1spawned = false;
            public int thisSpear1Index;

            public bool spearItem2spawned = false;
            public int thisSpear2Index;

            public bool spearItem3spawned = false;
            public int thisSpear3Index;

            public bool spearItem4spawned = false;
            public int thisSpear4Index;
            //----------------------------------------------
            public bool gunItem1spawned = false;
            public int thisGun1Index;

            public bool gunItem2spawned = false;
            public int thisGun2Index;

            public bool gunItem3spawned = false;
            public int thisGun3Index;

            public bool gunItem4spawned = false;
            public int thisGun4Index;

            int timer = 0;
            public override void Update()
            {
                Player player = Main.LocalPlayer;


                if (timer > 0)
                {
                    timer--;
                }

                if (slot1 == mod.ItemType("Swordsteel1") && slot2 == mod.ItemType("Swordsoul1") && slot3 == mod.ItemType("Swordlog1") && slot4 == mod.ItemType("Swordlogadd1"))
                {
                    crafting = true;
                    if (timer == 0)
                    {
                        timer = 300;
                        for (int dc = 0; dc < 30; dc++)
                        {
                            Dust.NewDust(new Vector2(((Position.X * 16) + 10), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 50), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 182), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 142), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                        }
                    }

                    if (timer > 1)
                    {
                        Dust.NewDust(new Vector2(((Position.X * 16) + 10) + (((300 - timer) / 300f) * 84), ((Position.Y * 16) - 9) + (((300 - timer) / 300f) * 66)), 6, 6, mod.DustType("Sworddust"));
                        Dust.NewDust(new Vector2(((Position.X * 16) + 50) + (((300 - timer) / 300f) * 49), ((Position.Y * 16) - 29) + (((300 - timer) / 300f) * 86)), 6, 6, mod.DustType("Sworddust"));
                        Dust.NewDust(new Vector2(((Position.X * 16) + 182) - (((300 - timer) / 300f) * 84), ((Position.Y * 16) - 9) + (((300 - timer) / 300f) * 66)), 6, 6, mod.DustType("Sworddust"));
                        Dust.NewDust(new Vector2(((Position.X * 16) + 142) - (((300 - timer) / 300f) * 49), ((Position.Y * 16) - 29) + (((300 - timer) / 300f) * 86)), 6, 6, mod.DustType("Sworddust"));

                    }

                    if (timer == 1)
                    {
                        Vector2 tilecenter = new Vector2((Position.X * 16) + 114, (Position.Y * 16) + 50);
                        Item.NewItem(tilecenter, new Vector2(1, 1), mod.ItemType("Koranithus"));
                        slot1 = 0;
                        slot2 = 0;
                        slot3 = 0;
                        slot4 = 0;
                        swordItem1spawned = false;
                        swordItem2spawned = false;
                        swordItem3spawned = false;
                        swordItem4spawned = false;
                        swordmade = true;
                        crafting = false;

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
                            Dust.NewDustPerfect(tilecenter, mod.DustType("Sworddust2"), new Vector2(xvel, yvel), 0, default, Main.rand.Next(8, 10) * 0.1f);
                            if (Main.rand.Next(2) == 0)
                            {
                                Dust.NewDustPerfect(tilecenter, mod.DustType("Slagdust"), new Vector2(xvel, yvel), 0, default, Main.rand.Next(12, 14) * 0.1f);
                            }
                            //sound FX
                            Main.PlaySound(SoundID.Item37, player.Center);
                            Main.PlaySound(SoundID.Item45, player.Center);
                        }
                    }
                }

                if (slot1 == mod.ItemType("Swordsteel1"))
                {
                    if (!swordItem1spawned)//find a better way to do this later
                    {
                        thisSword1Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 15, Position.Y * 16), new Vector2(0, 0), mod.ProjectileType("Sword1"), 0, 0);
                        Sword1 sword1 = Main.projectile[thisSword1Index].modProjectile as Sword1;
                        sword1.instance = this;
                        swordItem1spawned = true;
                    }


                }

                if (slot2 == mod.ItemType("Swordsoul1"))
                {
                    if (!swordItem2spawned)//find a better way to do this later
                    {
                        thisSword2Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 54, Position.Y * 16 - 20), new Vector2(0, 0), mod.ProjectileType("Sword2"), 0, 0);
                        Sword2 sword2 = Main.projectile[thisSword2Index].modProjectile as Sword2;
                        sword2.instance = this;
                        swordItem2spawned = true;
                    }
                }

                if (slot3 == mod.ItemType("Swordlog1"))
                {
                    if (!swordItem3spawned)//find a better way to do this later
                    {
                        thisSword3Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 154, Position.Y * 16 - 20), new Vector2(0, 0), mod.ProjectileType("Sword3"), 0, 0);
                        Sword3 sword3 = Main.projectile[thisSword3Index].modProjectile as Sword3;
                        sword3.instance = this;
                        swordItem3spawned = true;
                    }
                }

                if (slot4 == mod.ItemType("Swordlogadd1"))
                {
                    if (!swordItem4spawned)//find a better way to do this later
                    {
                        thisSword4Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 193, Position.Y * 16), new Vector2(0, 0), mod.ProjectileType("Sword4"), 0, 0);
                        Sword4 sword4 = Main.projectile[thisSword4Index].modProjectile as Sword4;
                        sword4.instance = this;
                        swordItem4spawned = true;
                    }
                }

                //---------------------------------------------------

                if (slot1 == mod.ItemType("Spear1") && slot2 == mod.ItemType("Spear2") && slot3 == mod.ItemType("Spear3") && slot4 == mod.ItemType("Spear4"))
                {
                    crafting = true;
                    if (timer == 0)
                    {
                        timer = 300;
                        for (int dc = 0; dc < 30; dc++)
                        {
                            Dust.NewDust(new Vector2(((Position.X * 16) + 10), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 50), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 182), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 142), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                        }
                    }

                    if (timer > 1)
                    {
                        Dust.NewDust(new Vector2(((Position.X * 16) + 10) + (((300 - timer) / 300f) * 84), ((Position.Y * 16) - 9) + (((300 - timer) / 300f) * 66)), 6, 6, mod.DustType("Speardust"));
                        Dust.NewDust(new Vector2(((Position.X * 16) + 50) + (((300 - timer) / 300f) * 49), ((Position.Y * 16) - 29) + (((300 - timer) / 300f) * 86)), 6, 6, mod.DustType("Speardust"));
                        Dust.NewDust(new Vector2(((Position.X * 16) + 182) - (((300 - timer) / 300f) * 84), ((Position.Y * 16) - 9) + (((300 - timer) / 300f) * 66)), 6, 6, mod.DustType("Speardust"));
                        Dust.NewDust(new Vector2(((Position.X * 16) + 142) - (((300 - timer) / 300f) * 49), ((Position.Y * 16) - 29) + (((300 - timer) / 300f) * 86)), 6, 6, mod.DustType("Speardust"));

                    }

                    if (timer == 1)
                    {
                        Vector2 tilecenter = new Vector2((Position.X * 16) + 114, (Position.Y * 16) + 50);
                        Item.NewItem(tilecenter, new Vector2(1, 1), mod.ItemType("Testspear"));
                        slot1 = 0;
                        slot2 = 0;
                        slot3 = 0;
                        slot4 = 0;
                        spearItem1spawned = false;
                        spearItem2spawned = false;
                        spearItem3spawned = false;
                        spearItem4spawned = false;
                        spearmade = true;
                        crafting = false;

                        string text = player.name + " Has Crafted PH TESTSPEAR!";
                        if (Main.netMode == 2) // Server
                        {
                            NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 100f, 45f, 255f, 0, 0, 0);
                        }
                        else if (Main.netMode == 0) // Client
                        {
                            Main.NewText(text, new Color(170, 140, 220));
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
                            Dust.NewDustPerfect(tilecenter, mod.DustType("Sworddust2"), new Vector2(xvel, yvel), 0, default, Main.rand.Next(8, 10) * 0.1f);

                            //sound FX
                            Main.PlaySound(SoundID.Item37, player.Center);
                            Main.PlaySound(SoundID.Item45, player.Center);
                        }
                    }
                }

                if (slot1 == mod.ItemType("Spear1"))
                {
                    if (!spearItem1spawned)//find a better way to do this later
                    {
                        thisSpear1Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 15, Position.Y * 16), new Vector2(0, 0), mod.ProjectileType("Spear1p"), 0, 0);
                        Spear1p spear1p = Main.projectile[thisSpear1Index].modProjectile as Spear1p;
                        spear1p.instance = this;
                        spearItem1spawned = true;
                    }
                }

                if (slot2 == mod.ItemType("Spear2"))
                {
                    if (!spearItem2spawned)//find a better way to do this later
                    {
                        thisSpear2Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 54, Position.Y * 16 - 20), new Vector2(0, 0), mod.ProjectileType("Spear2p"), 0, 0);
                        Spear2p spear2p = Main.projectile[thisSpear2Index].modProjectile as Spear2p;
                        spear2p.instance = this;
                        spearItem2spawned = true;
                    }
                }

                if (slot3 == mod.ItemType("Spear3"))
                {
                    if (!spearItem3spawned)//find a better way to do this later
                    {
                        thisSpear3Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 154, Position.Y * 16 - 20), new Vector2(0, 0), mod.ProjectileType("Spear3p"), 0, 0);
                        Spear3p spear3p = Main.projectile[thisSpear3Index].modProjectile as Spear3p;
                        spear3p.instance = this;
                        spearItem3spawned = true;
                    }
                }

                if (slot4 == mod.ItemType("Spear4"))
                {
                    if (!spearItem4spawned)//find a better way to do this later
                    {
                        thisSpear4Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 193, Position.Y * 16), new Vector2(0, 0), mod.ProjectileType("Spear4p"), 0, 0);
                        Spear4p spear4p = Main.projectile[thisSpear4Index].modProjectile as Spear4p;
                        spear4p.instance = this;
                        spearItem4spawned = true;
                    }
                }

                //---------------------------------------------------

                    if (slot1 == mod.ItemType("Gun1") && slot2 == mod.ItemType("Gun2") && slot3 == mod.ItemType("Gun3") && slot4 == mod.ItemType("Gun4"))
                    {
                        crafting = true;
                        if (timer == 0)
                        {
                            timer = 300;
                            for (int dc = 0; dc < 30; dc++)
                            {
                                Dust.NewDust(new Vector2(((Position.X * 16) + 10), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                                Dust.NewDust(new Vector2(((Position.X * 16) + 50), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                                Dust.NewDust(new Vector2(((Position.X * 16) + 182), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                                Dust.NewDust(new Vector2(((Position.X * 16) + 142), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.4f);
                            }
                        }

                        if (timer > 1)
                        {
                            Dust.NewDust(new Vector2(((Position.X * 16) + 10) + (((300 - timer) / 300f) * 84), ((Position.Y * 16) - 9) + (((300 - timer) / 300f) * 66)), 6, 6, mod.DustType("Gundust"));
                            Dust.NewDust(new Vector2(((Position.X * 16) + 50) + (((300 - timer) / 300f) * 49), ((Position.Y * 16) - 29) + (((300 - timer) / 300f) * 86)), 6, 6, mod.DustType("Gundust"));
                            Dust.NewDust(new Vector2(((Position.X * 16) + 182) - (((300 - timer) / 300f) * 84), ((Position.Y * 16) - 9) + (((300 - timer) / 300f) * 66)), 6, 6, mod.DustType("Gundust"));
                            Dust.NewDust(new Vector2(((Position.X * 16) + 142) - (((300 - timer) / 300f) * 49), ((Position.Y * 16) - 29) + (((300 - timer) / 300f) * 86)), 6, 6, mod.DustType("Gundust"));

                        }

                        if (timer == 1)
                        {
                            Vector2 tilecenter = new Vector2((Position.X * 16) + 114, (Position.Y * 16) + 50);
                            Item.NewItem(tilecenter, new Vector2(1, 1), mod.ItemType("Testgun"));
                            slot1 = 0;
                            slot2 = 0;
                            slot3 = 0;
                            slot4 = 0;
                            gunItem1spawned = false;
                            gunItem2spawned = false;
                            gunItem3spawned = false;
                            gunItem4spawned = false;
                            gunmade = true;
                            crafting = false;

                            string text = player.name + " Has Crafted HX-17 Sigma!";
                            if (Main.netMode == 2) // Server
                            {
                                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 255f, 255f, 150f, 0, 0, 0);
                            }
                            else if (Main.netMode == 0) // Client
                            {
                                Main.NewText(text, new Color(170, 140, 220));
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
                                Dust.NewDustPerfect(tilecenter, mod.DustType("Sworddust2"), new Vector2(xvel, yvel), 0, default, Main.rand.Next(8, 10) * 0.1f);

                                //sound FX
                                Main.PlaySound(SoundID.Item37, player.Center);
                                Main.PlaySound(SoundID.Item45, player.Center);
                            }
                        }
                    }

                    if (slot1 == mod.ItemType("Gun1"))
                    {
                        if (!gunItem1spawned)//find a better way to do this later
                        {
                            thisGun1Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 15, Position.Y * 16), new Vector2(0, 0), mod.ProjectileType("Gun1"), 0, 0);
                            Gun1 gun1 = Main.projectile[thisGun1Index].modProjectile as Gun1;
                            gun1.instance = this;
                            gunItem1spawned = true;
                        }
                    }

                    if (slot2 == mod.ItemType("Gun2"))
                    {
                        if (!gunItem2spawned)//find a better way to do this later
                        {
                            thisGun2Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 54, Position.Y * 16 - 20), new Vector2(0, 0), mod.ProjectileType("Gun2"), 0, 0);
                            Gun2 gun2 = Main.projectile[thisGun2Index].modProjectile as Gun2;
                            gun2.instance = this;
                            gunItem2spawned = true;
                        }
                    }

                    if (slot3 == mod.ItemType("Gun3"))
                    {
                        if (!gunItem3spawned)//find a better way to do this later
                        {
                            thisGun3Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 154, Position.Y * 16 - 20), new Vector2(0, 0), mod.ProjectileType("Gun3"), 0, 0);
                            Gun3 gun3 = Main.projectile[thisGun3Index].modProjectile as Gun3;
                            gun3.instance = this;
                            gunItem3spawned = true;
                        }
                    }

                    if (slot4 == mod.ItemType("Gun4"))
                    {
                        if (!gunItem4spawned)//find a better way to do this later
                        {
                            thisGun4Index = Projectile.NewProjectile(new Vector2(Position.X * 16 + 193, Position.Y * 16), new Vector2(0, 0), mod.ProjectileType("Gun4"), 0, 0);
                            Gun4 gun4 = Main.projectile[thisGun4Index].modProjectile as Gun4;
                            gun4.instance = this;
                            gunItem4spawned = true;
                        }
                    }

                    //next recipie here



                
            }


            public override bool ValidTile(int i, int j)
            {
                Tile tile = Main.tile[i, j];
                return tile.active() && tile.type == mod.TileType("Altar") && tile.frameX == 0 && tile.frameY == 0;
            }

            public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
            {
                if (Main.netMode == 1)
                {
                    NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
                    NetMessage.SendData(87, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                    return -1;
                }
                return Place(i, j);

            }

            public override TagCompound Save()
            {
                return new TagCompound
            {
                {"slot1", slot1},
                {"slot2", slot2},
                {"slot3", slot3},
                {"slot4", slot4},
                {"sword", swordmade},
                {"bow", bowmade},
                {"staff", staffmade},
                {"spear", spearmade},
                {"gun", gunmade},
                {"orb", orbmade},
                    {"pick", pickmade}


            };
            }

            public override void Load(TagCompound tag)
            {
                slot1 = tag.GetInt("slot1");
                slot2 = tag.GetInt("slot2");
                slot3 = tag.GetInt("slot3");
                slot4 = tag.GetInt("slot4");
                swordmade = tag.GetBool("sword");
                bowmade = tag.GetBool("bow");
                staffmade = tag.GetBool("staff");
                spearmade = tag.GetBool("spear");
                gunmade = tag.GetBool("gun");
                orbmade = tag.GetBool("orb");
                pickmade = tag.GetBool("pick");

                int thisBack = Projectile.NewProjectile(new Vector2(Position.X * 16 + 104, Position.Y * 16 ), new Vector2(0, 0), mod.ProjectileType("Backdrop"), 0, 0);
                Backdrop thisBack2 = Main.projectile[thisBack].modProjectile as Backdrop;
                thisBack2.instance = this;

            }
        }
    }
}