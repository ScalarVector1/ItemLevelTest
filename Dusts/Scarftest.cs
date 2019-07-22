using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace ItemLevelTest.Dusts
{
    class Scarftest : ModDust
    {
        private int n = 0;
        public override void OnSpawn(Dust dust)
        {          
            dust.noGravity = true;
            dust.noLight = true;
            n = 0;
        }
        public override Color? GetAlpha(Dust dust, Color color)
        {
            return new Color(255,255,525,50);
        }

        Dust dustprev = null;
        Dust dustnext = null;
        private float K = 13.2f; //spring constant of the scarf              all of these values will be changed to properly simulate 
        private float M = 0.00187f; //mass or each particle
        private float W = 0f; //wind force
        private float T = 1/60f; //time
        private float g = 250f; //acceleration due to gravity
        float timer = 0;


        public override bool Update(Dust dust)
        {


            for (int k = 0; k <= 6000; k++)//check the whole flipping dust array  !!!THIS IS PROBABLY CAUSING PERFORMANCE ISSUES, REPLACE IF POSSIBLE!!!
            {
                if (Main.dust[k].customData is int && (int)Main.dust[k].customData == (int)dust.customData - 1 && Main.dust[k].type == mod.DustType("Scarftest")) // find the previous dust in the dust index
                {
                    dustprev = Main.dust[k]; //set that as dustprev
                }
                else if (Main.dust[k].type == mod.DustType("Scarftestroot"))//the root dust is also allowed to be dustprev
                {
                    dustprev = Main.dust[k]; //set that as dustprev instead
                }
                if (Main.dust[k].customData is int && (int)Main.dust[k].customData == (int)dust.customData + 1 && Main.dust[k].type == mod.DustType("Scarftest")) // find the next dust in the dust index
                {
                    dustnext = Main.dust[k]; //set that as dustnext
                }
            }

                dust.position.X -= (0.5f * //add to the current position the change in distance based off of d=1/2 at^2
                (
                (((dust.position.X - dustprev.position.X) * K) / M) + //distance formula, hooke's law, and a = fnet/m are used to find the net force in the X direction
                (((dust.position.X - dustnext.position.X) * K) / M) + //same as above for the tension from the next point as opposed to that of the previous
                (W / M) //add the applied force of the wind
                ) * T * T);// the t^2 portion of d = 1/2 at^2
                
                dust.position.Y -= (0.5f *
                (
                (((dust.position.Y - dustprev.position.Y) * K) / M) + //mirrors the above code, but for the Y direction
                (((dust.position.Y - dustnext.position.Y) * K) / M) -
                (g) //add accel. due to gravity instead of an applied force
                ) * T * T);

                dust.position.X -= (float)Math.Sin(((float)Math.PI * 2) * (timer / 1500f)) / 26;
            dust.position.Y -= (float)Math.Sin(((float)Math.PI * 2) * (timer / 750f)) / 35;

            dust.rotation = Main.rand.NextFloat(0, (float)Math.PI / 2);//gives a frizzy effect

            dust.active = Effecthandler.spearSpawned;

            timer += 1;
            W = (float)Math.Abs((Math.Sin(((float)Math.PI * 2) * (timer / 1500f)) / 7f));

            if (timer >= 1500)
            {
                timer = 0;
            }

            float light = 0.02f * dust.scale;
            Lighting.AddLight(dust.position, 0.4f, 0.4f, 0.6f);

            return false;
        }
    }

    class Scarftestroot : ModDust
    {

        private int n = 0;
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            n = 0;
        }

        public override Color? GetAlpha(Dust dust, Color color)
        {
            return Color.White;
        }
       
        public override bool Update(Dust dust)
        {
            if (dust.customData != null && dust.customData is Player)
            {
      
                    Player player = (Player)dust.customData;
                    dust.position.Y = player.MountedCenter.Y - 5;
                if (player.direction == 1)
                {
                    dust.position.X = player.MountedCenter.X - 8;
                }
                else
                {
                    dust.position.X = player.MountedCenter.X + 8;
                }

            }
            dust.active = Effecthandler.spearSpawned;

            return false;
        }
    }



   
}
