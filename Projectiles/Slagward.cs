
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ItemLevelTest.Projectiles
{
    class Slagward : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slag ward");
        }

        public override string Texture
        {
            get
            {
                return "ItemLevelTest/Projectiles/Slagward";
            }
        }
        public override void SetDefaults()
        {
            projectile.damage = 2;
            projectile.width = 80;
            projectile.height = 80;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 15 * 60;
            projectile.tileCollide = false;



        }
        public float x = 0;
        public float y = 0;
        public float r = 80;

        public override void AI()
        {
            projectile.rotation += (float)(Math.PI * 2 / 120);
            //double rot = projectile.rotation;
            Player player = Main.player[Main.myPlayer];
            projectile.position = new Vector2(player.MountedCenter.X - projectile.width / 2 + x, player.MountedCenter.Y - projectile.height / 2 + y);
            y = (float)Math.Sin(projectile.rotation) * r;
            x = (float)Math.Cos(projectile.rotation) * r;

            Dust.NewDust(new Vector2(projectile.Center.X - (projectile.width) / 2 - 15, projectile.Center.Y - projectile.height / 2 - 15), projectile.width + 15, projectile.height + 15, 6, 0, 0, 0, default(Color), 0.8f);

            for (int k = 0; k <= 1000; k++)
            {
                if (projectile.getRect().Intersects(Main.projectile[k].getRect()) && !Main.projectile[k].friendly)
                {
                    Main.projectile[k].active = false;
                    projectile.timeLeft = 0;
                }
            }




        }



        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];
            Main.PlaySound(SoundID.Item38, player.Center);
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
                Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), 6, new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(20, 24) * 0.1f);
                if (Main.rand.Next(3) == 0)
                {
                    Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Slagdust"), new Vector2(xvel * 1.1f, yvel * 1.1f), 0, default(Color), Main.rand.Next(15, 18) * 0.1f);
                }
            }
            }





    }
    class Slagward2 : Slagward
    {
        public override void SetDefaults()
        {
            projectile.damage = 2;
            projectile.width = 80;
            projectile.height = 80;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 15 * 60;
            projectile.tileCollide = false;
            projectile.rotation = (float)(Math.PI * 2 / 3);



        }
    }
    class Slagward3 : Slagward
    {
        public override void SetDefaults()
        {
            projectile.damage = 2;
            projectile.width = 80;
            projectile.height = 80;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 15 * 60;
            projectile.tileCollide = false;
            projectile.rotation = (float)(Math.PI * 2 / 3 * 2);



        }
    }
}
