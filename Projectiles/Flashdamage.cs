using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ItemLevelTest.Projectiles
{
    class Flashdamage : ModProjectile
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flash");
        }

        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.penetrate = 200;
            projectile.timeLeft = 9;
        }
        
        public override void AI()
        {
            Player player = Main.LocalPlayer;
            projectile.Center = player.Center;
        }
    }
}

