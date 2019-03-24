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
			dust.noLight = true;
			dust.scale *= 2.1f;
            dust.color.G = 240;
            dust.color.B = 0;
            dust.color.R = 255;
        }

       /* public override Color? GetAlpha(Dust dust, Color color)
        {
            return Color.White;
        }*/

        public override bool Update(Dust dust)
		{
            dust.position.Y += dust.velocity.Y;
			dust.rotation += dust.velocity.X;

                dust.scale *= 0.94f;
            if (dust.color.G >= 10)
            {
                dust.color.G -= 10;
            }
            else
            {
                dust.color.G = 0;
            }
             
               

            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, 0.55f + (2.65f - (dust.scale + 0.25f)) / 9, 0.5f - (2.5f - dust.scale) / 9, 0.45f - (2.5f - dust.scale) / 9);
            }
            else
            {
                Lighting.AddLight(dust.position, 0.55f + 2.65f/9, 2.65f / 9, 0 / 9);
            }
			if (dust.scale < 0.55f)
			{
				dust.active = false;
			}
			return false;
		}


    }
}

