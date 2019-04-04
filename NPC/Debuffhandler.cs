using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Tiles
{
    public class Buffhandler : GlobalNPC
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
                npc.lifeRegen -= 12;
                damage += 3;
            }
        }

    }
    public class Buffhandlerplayer : ModPlayer
    {
       public bool ward = false;
        public override void ResetEffects()
        {
            ward = false;
        }
        public override void PostUpdateBuffs()
        {

        
            if (ward)
            {
                player.statDefense += 20;
            }
        }
    }
}