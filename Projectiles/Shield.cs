
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ItemLevelTest.Projectiles
{
    class Shield:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 8;     
    }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 48;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.penetrate = 999;
            projectile.timeLeft = 40;
            projectile.tileCollide = false;
            




        }

        public override Color? GetAlpha(Color lightColor)

        {
            return Color.White;
        }


        public override void Kill(int timeLeft)
        {

        }
        public override void AI()

        {
            projectile.scale += 0.01f;
            projectile.alpha -= 255 / 40;
            projectile.position.X -= projectile.width * (projectile.scale - 1) * 0.01f;
            projectile.position.Y -= projectile.height * (projectile.scale - 1)* 0.01f;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.frame = 0;
                }
            }
      
        }





    }

}
