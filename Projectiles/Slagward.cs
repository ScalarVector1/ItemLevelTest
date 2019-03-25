
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
        public override void SetDefaults()
        {
            projectile.damage = 2;
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 15 * 60;
            projectile.tileCollide = false;



        }
        public float x = 0;
        public float y = 0;
        public float r = 120;

        public override void AI()
        {
            projectile.rotation += (float)(Math.PI * 2 / 120);
            double rot = projectile.rotation;
            Player player = Main.player[Main.myPlayer];
            projectile.position = new Vector2(player.position.X + x, player.position.Y + y);
            y = (float)Math.Sin(projectile.rotation) * r;
            x = (float)Math.Cos(projectile.rotation) * r;
 



        }



        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];
            Main.PlaySound(SoundID.Item38, player.Center);
        }





    }
    class Slagward2 : Slagward
    {
        public override void SetDefaults()
        {
            projectile.damage = 2;
            projectile.width = 30;
            projectile.height = 30;
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
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 15 * 60;
            projectile.tileCollide = false;
            projectile.rotation = (float)(Math.PI * 2 / 3 * 2);



        }
    }
}
