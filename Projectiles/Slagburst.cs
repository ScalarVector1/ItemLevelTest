
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
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(0,0), mod.ProjectileType("Slagburstspawner"), projectile.damage ,0);
        }
    }
    class Slagburstspawner : ModProjectile
    {
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
        public override void AI()
        {
            timer++;
            if (timer >= 10)
            {
                Projectile.NewProjectile(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(Main.rand.Next(-2, 2) * .1f, -7), mod.ProjectileType("Slagburstouch"), projectile.damage, .02f, Main.myPlayer);
                timer = 0;
            }
            
    
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 6, Main.rand.Next(-2, 2) * 0.1f, -15);

        }


        }
    class Slagburstouch : ModProjectile
    {
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

                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 6);
            }

        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 6);
        }
    }
}
