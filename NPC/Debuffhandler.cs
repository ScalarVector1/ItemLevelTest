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
            phantom = false;
           
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool slagmelt = false;
        public bool phantom = false;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (slagmelt)
            {
                npc.lifeRegen -= 12;
                damage += 3;               
            }

            if (phantom)
            {
                npc.lifeRegen -= 11;
                damage += 6;
            }
        }

        public override bool PreAI(NPC npc)
        {
            if (phantom)
            {
                if(Main.rand.Next(3) == 1)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("Bowdust5"), 0, -3, 0, new Color(255, 255, 255));
                }
            }
            return true;
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