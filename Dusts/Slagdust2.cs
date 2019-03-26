using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
    public class Slagdust2 : ModDust
    {
        float x = 0;
        float y = 0;
        int r = 90;
 
        public override void OnSpawn(Dust dust)
        {
            
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale = 1.009f;
            dust.rotation = Main.rand.NextFloat(0, (float)Math.PI * 2);
           
            

        }
     
        public override bool Update(Dust dust)
        {
            dust.rotation += (float)Math.PI * 2 / Main.rand.Next(180, 220);
            if (dust.customData != null && dust.customData is Player)
            {
                Player player = (Player)dust.customData;   
                double rot = dust.rotation;
                dust.position = new Vector2(player.MountedCenter.X + x, player.MountedCenter.Y + y);
                y = (float)Math.Sin(dust.rotation) * r;
                x = (float)Math.Cos(dust.rotation) * r;
                
            }
            dust.scale -= 0.00001f;



            if (dust.scale <= 1)
            {
                dust.active = false;
               
            }

            if (((dust.scale - 1) * 100000) % 10 == 0)
            {


                Dust.NewDustPerfect(new Vector2(dust.position.X, dust.position.Y), 6, new Vector2(Main.rand.Next(-80, 80) * 0.01f, Main.rand.Next(-80, 80) * 0.01f), 0, default(Color), Main.rand.Next(5, 8) * 0.1f);
            }
            
            return false;
        }



    }
}


    