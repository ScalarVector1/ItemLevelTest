using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
	public class Sworddust2 : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.3f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 2.1f;
		}

		public override bool Update(Dust dust)
		{
            dust.position.Y += dust.velocity.Y;
            dust.position.X += dust.velocity.X;

            
                dust.rotation += Main.rand.Next(-200, 200) * .0025f;
            


                dust.scale *= 0.994f;
            
			float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, (0.55f + (2.65f - (dust.scale + 0.25f)) / 9) * dust.scale * 0.5f, (0.5f - (2.5f - dust.scale) / 9) * dust.scale * 0.5f, (0.45f - (2.5f - dust.scale) / 9) * dust.scale * 0.5f);
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