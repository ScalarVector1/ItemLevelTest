using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
	public class Bowdust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity.Y = 0;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1.6f;
            dust.color.G = 255;
            dust.color.B = 180;
            dust.color.R = 180;
        }

		public override bool Update(Dust dust)
		{
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.97f;
            if (dust.color.G >= 120)
            {
                dust.color.G -= 1;

                if(dust.color.B < 250)
                {
                    dust.color.B += 3;
                }
                
            }


            float light = 0.02f * dust.scale;

                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f );
            
            if (dust.scale < 0.35f)
            {
                dust.active = false;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

    }
}