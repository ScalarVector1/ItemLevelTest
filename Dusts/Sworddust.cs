using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
	public class Sworddust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.3f;
			dust.noGravity = true;
			dust.noLight = false;
			dust.scale *= 2.1f;
            dust.color.G = 245;
            dust.color.B = 210;
            dust.color.R = 245;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

        public override bool Update(Dust dust)
		{
            dust.position.Y += dust.velocity.Y;
			dust.rotation += dust.velocity.X;

                dust.scale *= 0.94f;
            if (dust.color.G == 0)
            {
                dust.color.B = 0;
            }
            if (dust.color.G >= 140)
            {
                dust.color.G -= 6;
                if (dust.color.B >= 18)
                {
                    dust.color.B -= 17;
                }
                if(dust.color.R <= 252)
                {
                    dust.color.R += 3;
                }
                
            }
             
               

            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }
            

			if (dust.scale < 0.55f)
			{
				dust.active = false;
			}
			return false;
		}


    }
}

