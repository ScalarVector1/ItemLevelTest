using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
	public class Swordoredust : ModDust
	{
        public float acceleration = 0.5f;
        public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.05f;
  			dust.noGravity = false;
			dust.noLight = true;
			dust.scale *= 1.6f;
		}

		public override bool Update(Dust dust)
		{
            dust.position.Y += dust.velocity.Y / 2;
            dust.velocity.Y += acceleration;
			dust.rotation += dust.velocity.X;
            dust.position.X += dust.velocity.X;

            dust.scale *= 0.93f;
            
			float light = 0.02f * dust.scale;
            if (Main.rand.Next(1) == 0)
            {
                Lighting.AddLight(dust.position, .232f * 2, .207f * 2, .150f * 2);
            }
            else
            {
                Lighting.AddLight(dust.position, .154f * 2, .186f * 2, .184f * 2);
            }
           
			if (dust.scale < 0.35f)
			{
				dust.active = false;
			}
			return false;
		}



    }

    public class Swordoredust2 : ModDust
    {
        public float acceleration = 0.2f;
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.02f;
            dust.noGravity = false;
            dust.noLight = true;
            dust.scale *= 1f;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y / 5;
            dust.velocity.Y += acceleration;
            dust.rotation += dust.velocity.X;
            dust.position.X += dust.velocity.X;

            dust.scale *= 0.99f;

            float light = 0.02f * dust.scale;
            if (Main.rand.Next(1) == 0)
            {
                Lighting.AddLight(dust.position, .232f, .207f, .150f);
            }
            else
            {
                Lighting.AddLight(dust.position, .154f, .186f, .184f);
            }

            if (dust.scale < 0.35f)
            {
                dust.active = false;
            }
            return false;
        }



    }
}