using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;

namespace ItemLevelTest.Tiles
{
    public class Altar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.Width = 13;//Width of the tile in blocks
            TileObjectData.newTile.Height = 5;//Height of the tile in blocks
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);//should not have to change
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;//dont change
            TileObjectData.newTile.CoordinatePadding = 2;//dont change
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);

            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<AltarEntity>().Hook_AfterPlacement, -1, 0, false);

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
            AltarEntity altarentity = mod.GetTileEntity<AltarEntity>();
            Player player = Main.LocalPlayer; //needs to be changed for multiplayer adaptation

            if (player.HeldItem.type == mod.ItemType("Swordsteel1"))
            {
                player.HeldItem.stack--;
                altarentity.slot1 = 1;
            }

            if (player.HeldItem.type == mod.ItemType("Swordsoul1"))
            {
                player.HeldItem.stack--;
                altarentity.slot2 = 1;
            }
        }
    }

    public class AltarEntity : ModTileEntity
    {
        //0 = nothing, 1 = sword, 2 = bow, 3 = staff, 4 = spear, 5 = gun, 6 = orb, 7 = ultimate
        private const int range = 100;
        public int slot1 = 0;
        public int slot2 = 0;
        public int slot3 = 0;
        public int slot4 = 0;
        public bool crafting = false;
        public bool craftsword = false;

        public override void Update()
        {
            if (slot1 == 1 && slot2 == 1 && slot3 == 1 && slot4 == 1)
            {
                craftsword = true;
                crafting = true;
            }
            if (slot1 == 1)
            {
                Dust.NewDust(new Vector2(this.Position.X, this.Position.Y), 10 , 10, mod.DustType("Sworddust"));
            }
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
    }
}