using ItemLevelTest.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Dusts
{
    public class Gundust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.3f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 2.1f;
            dust.color.G = 240;
            dust.color.B = 225;
            dust.color.R = 240;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return dust.color;
        }

        public override bool Update(Dust dust)
        {
            dust.position.Y += dust.velocity.Y;
            dust.position.X += dust.velocity.X;
            dust.rotation += dust.velocity.X;

            dust.scale *= 0.94f;

            dust.color.B--;



            float light = 0.02f * dust.scale;
            if (dust.scale <= 2.5 + .55)
            {
                Lighting.AddLight(dust.position, dust.color.R * 0.002f, dust.color.G * 0.002f, dust.color.B * 0.002f);
            }


            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }
            return false;
        }


    }
    class Gundust2 : Gundust
    {
        public override bool Update(Dust dust)
        {
            Player player = Main.LocalPlayer;
            dust.position += dust.velocity + player.velocity;

            dust.scale *= 0.92f;

            dust.color.B--;
            dust.color *= 0.94f;


            if (dust.scale < 0.1f || Testgun.flash)
            {
                dust.active = false;
            }
            return false;
        }
    }

    class Gundust3 : Gundust
    {
        public override bool Update(Dust dust)
        {
            Player player = Main.LocalPlayer;
            dust.position += dust.velocity;
            dust.velocity *= 1.01f;

            dust.scale *= 0.87f;

            dust.color.B--;
            dust.color *= 0.98f;


            if (dust.scale < 0.35f)
            {
                dust.active = false;
            }
            return false;
        }
    }

    class Gundust4 : Gundust
    {
        NPC npc = null;
        public override bool Update(Dust dust)
        {
            if(dust.customData is NPC)
            {
                npc = (NPC)dust.customData;
            }
            dust.position += dust.velocity + npc.velocity;
            dust.velocity *= 1.01f;

            dust.scale *= 0.87f;

            dust.color.B--;
            dust.color *= 0.98f;


            if (dust.scale < 0.35f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}