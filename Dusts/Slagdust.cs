using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
    public class Slagdust : ModDust
    {
        public float acceleration = 0.025f;
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.05f;
            dust.noGravity = false;
            dust.noLight = true;
            dust.scale *= 1.3f;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y / 2;
            dust.velocity.Y += acceleration;
            dust.rotation += dust.velocity.X * Main.rand.Next(80, 120) * .01f;
            dust.position.X += dust.velocity.X;

            dust.scale *= 0.99f;


            if (dust.scale < 0.40f)
            {
                dust.active = false;
            }
            return false;
        }



    }
}


    