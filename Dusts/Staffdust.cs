using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
    public class Staffdust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 220;
            dust.color.B = 255;
            dust.color.R = 245;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity * 0.5f;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.93f;
            dust.color.R -= 2;
            dust.color.G -= 4;

            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }

            if (dust.scale < 0.35f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    public class Staffdust2 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 220;
            dust.color.B = 255;
            dust.color.R = 245;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            Player player = Main.LocalPlayer;
            dust.position += player.velocity;
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            dust.color.R -= 2;
            dust.color.G -= 4;


            if (dust.scale < 0.25f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    public class Staffdust3 : ModDust
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
            dust.position += dust.velocity * 0.5f;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.93f;

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
                if (dust.color.R <= 252)
                {
                    dust.color.R += 3;
                }
            }

            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }

            if (dust.scale < 0.35f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}