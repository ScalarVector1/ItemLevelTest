using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
	public class Leveldust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity.Y = -4;
			dust.noGravity = false;
			dust.noLight = true;
			dust.scale *= 1.5f;
		}

		public override bool Update(Dust dust)
		{
            dust.rotation = 1;
			dust.scale *= 0.995f;
            dust.velocity.Y -= 0.3f;
			float light = 0.2f * dust.scale;
			Lighting.AddLight(dust.position, 0.45f, 0.6f, 0.5f);
			if (dust.scale < 0.2f)
			{
				dust.active = false;
			}
			return false;
		}


    }
}