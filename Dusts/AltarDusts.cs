using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
    public class Sworddustaltar : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 230;
            dust.color.B = 210;
            dust.color.R = 245;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            dust.color.A = 255;
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            if(dust.color.G > dust.color.B)
            {
                dust.color.G -= 3;
                dust.color.B -= 2;
                dust.color.R -= 1;
            }




            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.45f)
            {
                dust.active = false;
            }
            return false;
        }


    }

    public class Bowdustaltar : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 255;
            dust.color.B = 230;
            dust.color.R = 210;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            dust.color.A = 255;
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            dust.color.R -= 2;
            dust.color.G -= 1;





            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.45f)
            {
                dust.active = false;
            }
            return false;
        }


    }

    public class Staffdustaltar : ModDust
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
            dust.color.A = 255;
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            dust.color.R -= 1;
            dust.color.G -= 2;





            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.45f)
            {
                dust.active = false;
            }
            return false;
        }


    }

    public class Speardustaltar : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 240;
            dust.color.B = 255;
            dust.color.R = 225;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            dust.color.A = 255;
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            dust.color.R -= 2;
            dust.color.G -= 3;
            dust.color.B--;





            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.45f)
            {
                dust.active = false;
            }
            return false;
        }


    }

    public class Gundustaltar : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 255;
            dust.color.B = 230;
            dust.color.R = 255;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            dust.color.A = 255;
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            dust.color.R -= 1;
            dust.color.G -= 1;
            dust.color.B -= 2;





            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.45f)
            {
                dust.active = false;
            }
            return false;
        }


    }

    public class Orbdustaltar : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 245;
            dust.color.B = 230;
            dust.color.R = 255;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            dust.color.A = 255;
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            dust.color.R -= 1;
            dust.color.G -= 2;
            dust.color.B -= 3;





            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.45f)
            {
                dust.active = false;
            }
            return false;
        }


    }

    public class Pickdustaltar : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 255;
            dust.color.B = 255;
            dust.color.R = 255;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            dust.color.A = 255;
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.95f;
            dust.color.R -= 2;
            dust.color.G -= 2;
            dust.color.B -= 2;





            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.45f)
            {
                dust.active = false;
            }
            return false;
        }


    }



}