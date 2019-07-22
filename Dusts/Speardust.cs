using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
    public class Speardust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 165;
            dust.color.B = 225;
            dust.color.R = 145;
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

            if (dust.color.G >= 50)
            {
                dust.color.G -= 3;
                if (dust.color.B >= 150)
                {
                    dust.color.B -= 1;
                }
                if (dust.color.R >= 50)
                {
                    dust.color.R -= 2;
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