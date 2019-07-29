
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ItemLevelTest.Projectiles
{
    class Slagburst:ModProjectile
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slagburst");
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.penetrate = int.MaxValue;
        }

        public override void AI()
        {
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 6); 
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft = 0;

            for (int dustcounter = 50; dustcounter >= 0; dustcounter--)
            {
                float yvel = 0;
                float xvel = 0;
                float hyp = 0;
                hyp = Main.rand.Next(20, 90) * 0.1f;
                xvel = Main.rand.Next(-400, 400) * .01f; 
                yvel = (float)Math.Sqrt(hyp - xvel * xvel) * -1; 
                Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Slagdust"), new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(20, 23) * 0.1f);

                    Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Sworddust2"), new Vector2(xvel * 1.1f, yvel * 1.1f), 0, default(Color), Main.rand.Next(6, 10) * 0.1f);
                
            }
                    
                return false;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(0,0), mod.ProjectileType("Slagburstspawner"), projectile.damage ,0);
            Main.PlaySound(SoundID.Item38, projectile.Center);
        }
    }
    class Slagburstspawner : ModProjectile
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }

        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.penetrate = int.MaxValue;
            projectile.timeLeft = 900;
     
        }
        int timer = 0;
        int timer2 = 0;
        public override void AI()
        {
            timer++;
            if (timer >= 10)
            {
                Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.position.Y + 1), new Vector2(Main.rand.Next(-2, 2) * .1f, -7), mod.ProjectileType("Slagburstouch"), projectile.damage, .02f, Main.myPlayer);
                Main.PlaySound(SoundID.Item34, projectile.Center);
                
                timer = 0;
            }

            
    
            Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, 6, Main.rand.Next(-2, 2) * 0.1f, -15);
            if (Main.rand.Next(20) == 0)
            {
                Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Slagdust"), new Vector2(Main.rand.Next(-30, 30) * 0.01f, Main.rand.Next(-70, -60) * 0.1f), 0, default(Color), Main.rand.Next(15, 23) * 0.1f);
            }
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), 6, new Vector2(Main.rand.Next(-180, 180) * 0.01f, Main.rand.Next(-50, -40) * 0.1f), 0, default(Color), Main.rand.Next(25, 30) * 0.1f);
            }

        }

  


    }
    class Slagburstouch : ModProjectile
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }

        public override void SetDefaults()
        {
            projectile.damage = 15;
            projectile.width = 16;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = int.MaxValue;
            projectile.timeLeft = 90 - (int)(Math.Abs(projectile.velocity.X) * 10);
        }
       
        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {

                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 6,0,0,0,default(Color),1.5f);
            }

        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 6,0, 0, 0, default(Color), 1.7f);
        }
    }
}
