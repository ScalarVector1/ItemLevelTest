using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
	public class Oredust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.3f;
            dust.velocity.Y -= 1.5f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 2.1f;
		}

		public override bool Update(Dust dust)
		{
            dust.position.Y += dust.velocity.Y;
			dust.rotation += dust.velocity.X;
          

                dust.scale *= 0.98f;
            
			float light = 0.22f * dust.scale;

                Lighting.AddLight(dust.position, 0.93f * 0.6f, 0.62f * 0.6f, 1.38f * 0.55f);
          
			if (dust.scale < 0.55f)
			{
				dust.active = false;
			}
			return false;
		}


    }
}