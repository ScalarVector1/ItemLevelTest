using Microsoft.Xna.Framework;
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
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);//should not have to change
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;//dont change
            TileObjectData.newTile.CoordinatePadding = 2;//dont change
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);



            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Mysterious Altar");//Map name
            AddMapEntry(new Color(110, 175, 110), name);//Map color
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
            mod.GetTileEntity<AltarEntity>().Kill(i, j);
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

                if (player.HeldItem.type == mod.ItemType("Swordsteel1"))
                {
                    player.HeldItem.stack--;
                    altarentity.slot1 = mod.ItemType("Swordsteel1");
                }

                if (player.HeldItem.type == mod.ItemType("Swordsoul1"))
                {
                    player.HeldItem.stack--;
                    altarentity.slot2 = mod.ItemType("Swordsoul1");
                }

                if (player.HeldItem.type == mod.ItemType("Swordlog1"))
                {
                    player.HeldItem.stack--;
                    altarentity.slot3 = mod.ItemType("Swordlog1");
                }

                if (player.HeldItem.type == mod.ItemType("Swordlogadd1"))
                {
                    player.HeldItem.stack--;
                    altarentity.slot4 = mod.ItemType("Swordlogadd1");
                }

                if (player.HeldItem.type == 0)
                {
                    if (altarentity.slot1 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot1);
                        altarentity.slot1 = 0;
                        altarentity.swordItem1spawned = false;//find a better way to do this later
                    }

                    if (altarentity.slot2 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot2);
                        altarentity.slot2 = 0;
                        altarentity.swordItem2spawned = false;//find a better way to do this later
                    }

                    if (altarentity.slot3 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot3);
                        altarentity.slot3 = 0;
                        altarentity.swordItem3spawned = false;//find a better way to do this later
                    }

                    if (altarentity.slot4 != 0)
                    {
                        Item.NewItem(player.position, new Vector2(1, 1), altarentity.slot4);
                        altarentity.slot4 = 0;
                        altarentity.swordItem4spawned = false;//find a better way to do this later
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
            public bool bowmade = true;
            public bool staffmade = true;
            public bool spearmade = false;
            public bool gunmade = false;
            public bool orbmade = false;

            public bool swordItem1spawned = false;
            public int thisSword1Index;
            
            public bool swordItem2spawned = false;
            public int thisSword2Index;

            public bool swordItem3spawned = false;
            public int thisSword3Index;

            public bool swordItem4spawned = false;
            public int thisSword4Index;

            int timer = 0;
            public override void Update()
            {
                Player player = Main.LocalPlayer;
                

                if(timer > 0)
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
                            Dust.NewDust(new Vector2(((Position.X * 16) + 10), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"),0,0,0,default(Color), 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 50), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default(Color), 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 182), (Position.Y * 16) - 9), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default(Color), 0.4f);
                            Dust.NewDust(new Vector2(((Position.X * 16) + 142), (Position.Y * 16) - 29), 10, 10, mod.DustType("Sworddust2"), 0, 0, 0, default(Color), 0.4f);
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
                            Dust.NewDustPerfect(tilecenter, mod.DustType("Sworddust2"), new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(8, 10) * 0.1f);
                            if (Main.rand.Next(2) == 0)
                            {
                                Dust.NewDustPerfect(tilecenter, mod.DustType("Slagdust"), new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(12, 14) * 0.1f);
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
                            thisSword3Index = Projectile.NewProjectile(new Vector2(Position.X* 16 + 154, Position.Y* 16 - 20), new Vector2(0, 0), mod.ProjectileType("Sword3"), 0, 0);
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

                //next recipie here



            }

            public override bool ValidTile(int i, int j)
            {
                Tile tile = Main.tile[i, j];
                return tile.active() && tile.type == mod.TileType("Altar") && tile.frameX == 0 && tile.frameY == 0;
            }
            public override void PostGlobalUpdate()
            {

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
                {"orb", orbmade}


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

                int thisBack = Projectile.NewProjectile(new Vector2(Position.X * 16 + 104, Position.Y * 16 ), new Vector2(0, 0), mod.ProjectileType("Backdrop"), 0, 0);
                Backdrop thisBack2 = Main.projectile[thisBack].modProjectile as Backdrop;
                thisBack2.instance = this;

            }
        }
    }
}