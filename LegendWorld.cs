using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using static ItemLevelTest.Tiles.Altar;

namespace ItemLevelTest
{
    public class LegendWorld : ModWorld
    {

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Charging Venerido...", VeneridoGen));
            }
        }
        
        private void VeneridoGen(GenerationProgress progress)
        {
            progress.Message = "Charging Venerido";

            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * .00015); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next(Main.maxTilesY - 400, Main.maxTilesY); 
                WorldGen.TileRunner(x, y, (double)WorldGen.genRand.Next(12, 17), WorldGen.genRand.Next(7, 12), mod.TileType("Voreblock"), false, 0f, 0f, false, true);
            }
        }

        public override void PostWorldGen()
        {
            for(int k = -7; k <= 7; k++)
            {
                Main.tile[Main.spawnTileX + k, Main.spawnTileY].type = TileID.Grass;

                for(int d = 1; d <= 30; d++)
                {
                    Main.tile[Main.spawnTileX + k, Main.spawnTileY - d].ClearEverything();
                }

            }
            WorldGen.PlaceTile(Main.spawnTileX - 6, Main.spawnTileY - 5, mod.TileType("Altar"), false, true);
            mod.GetTileEntity<AltarEntity>().Place(Main.spawnTileX - 6, Main.spawnTileY - 5);
        }





    }
}
