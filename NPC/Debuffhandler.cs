using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Tiles
{
    public class Debuffhandler : GlobalNPC
    {
        public override void ResetEffects(NPC npc)
        {
            slagmelt = false;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool slagmelt = false;
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (slagmelt)
            {
                npc.lifeRegen -= 6;
                damage += 6;
            }
        }
    }
}