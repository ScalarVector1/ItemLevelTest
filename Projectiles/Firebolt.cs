
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;

namespace ItemLevelTest.Projectiles
{
    class Firebolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firebolt");
        }
        public override void SetDefaults()
        {
            projectile.damage = 15;
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
        }

        public override void AI()
        {
            Dust.NewDust(new Vector2(projectile.position.X + 10, projectile.position.Y + 10), 1, 1, 6);
        }

        public override void Kill(int timeLeft)
        {
            for (int dustcounter = 40; dustcounter >= 0; dustcounter--)
            {
                float yvel = 0;
                float xvel = 0;
                float hyp = 0;
                hyp = Main.rand.Next(0, 30) * 0.1f;
                xvel = Main.rand.Next(-400, 400) * .01f;
                if (Main.rand.Next(2) == 0)
                {
                    yvel = (float)Math.Sqrt(hyp - xvel * xvel);
                }
                else
                {
                    yvel = (float)Math.Sqrt(hyp - xvel * xvel) * -1;
                }
                Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), 6, new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(20, 24) * 0.1f);
            }
        }
    }
}
