using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Tiles
{
    public class Voreblock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileStone[Type] = true;
            Main.tileSpelunker[Type] = true;
            dustType = mod.DustType("Oredust");
            minPick = 65;
            drop = mod.ItemType("Vore");
            AddMapEntry(new Color(128, 96, 145));
        }






    }
}