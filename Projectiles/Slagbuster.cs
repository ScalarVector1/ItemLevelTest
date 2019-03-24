
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ItemLevelTest.Projectiles
{
    class Slagbuster:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slag buster");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 40;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.penetrate = 2;
            drawOriginOffsetY = -13;
            drawOriginOffsetX = 10;


        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            Dust.NewDust(new Vector2(projectile.Center.X - (projectile.width) / 2 - 15, projectile.Center.Y - projectile.height / 2 - 15), projectile.width+30, projectile.height + 30, mod.DustType("Sworddust"));
            Dust.NewDust(new Vector2(projectile.Center.X - (projectile.width) / 2 - 15 , projectile.Center.Y - projectile.height / 2 - 15), projectile.width+30, projectile.height + 30, 6,0,0,0,default(Color),1.2f);

            if (Main.rand.Next(6) == 0)
            {
                Dust.NewDust(new Vector2(projectile.Center.X - (projectile.width) / 2 - 15 , projectile.Center.Y - projectile.height / 2 - 15), projectile.width + 30, projectile.height + 30, mod.DustType("Slagdust"), 0, 0, 0, default(Color), 1.2f);
            }
            projectile.position.X += projectile.velocity.X;
            projectile.position.Y += projectile.velocity.Y;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeLeft)
        {


        

            for (int dustcounter = 120; dustcounter >= 0; dustcounter--)
            {
                float yvel = 0;
                float xvel = 0;
                float hyp = 0;
                hyp = Main.rand.Next(0, 70) * 0.1f;
                xvel = Main.rand.Next(-400, 400) * .01f;
                if (Main.rand.Next(2) == 0)
                {
                    yvel = (float)Math.Sqrt(hyp - xvel * xvel);
                }
                else
                {
                    yvel = (float)Math.Sqrt(hyp - xvel * xvel) * -1;
                }

                //Dust.NewDust(new Vector2(projectile.Center.X - (projectile.width) / 2, projectile.Center.Y - projectile.height / 2), projectile.width, projectile.height, mod.DustType("Sworddust"));
                //Dust.NewDust(new Vector2(projectile.Center.X - (projectile.width) / 2, projectile.Center.Y - projectile.height / 2), projectile.width, projectile.height, 6);
                Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), 6, new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(20, 24) * 0.1f);
                if (Main.rand.Next(3) == 0)
                {
                    Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Slagdust"), new Vector2(xvel * 1.1f , yvel * 1.1f), 0, default(Color), Main.rand.Next(15, 18) * 0.1f);
                }
                //Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Sworddust"), new Vector2(0, Main.rand.Next(-45, 45) * 0.1f), 0, default(Color), Main.rand.Next(130, 150) * 0.01f);
                Main.PlaySound(SoundID.Item38, projectile.Center);
                Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(0, 0), mod.ProjectileType("Slagboom"), 65, 8.5f, projectile.owner);
            }
        }



    }
    class Slagboom : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.damage = 65;
            projectile.width = 100;
            projectile.height = 100;
            projectile.friendly = true;
            projectile.penetrate = 200;
            projectile.timeLeft = 5;
            



        }
    }
}
