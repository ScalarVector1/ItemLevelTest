using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;
using ItemLevelTest.Items;

namespace ItemLevelTest.Projectiles
{
    class Healblast : ModProjectile
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healing blast");
        }

        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 200;
            projectile.height = 200;
            projectile.friendly = true;
            projectile.penetrate = 200;
            projectile.timeLeft = 30;
            
        }
        bool healed = false;
        public Testgun instance = null;
        public override void AI()
        {
            if (!healed)
            {
                for (int k = 0; k <= 255; k++)
                {
                    if (projectile.getRect().Intersects(Main.player[k].getRect()))
                    {
                        Main.player[k].statLife += (20 + instance.level);
                        Main.player[k].HealEffect((20 + instance.level), true);
                    }
                }

                for (int dustcounter = 150; dustcounter >= 0; dustcounter--)
                {
                    float yvel = 0;
                    float xvel = 0;
                    float hyp = 0;
                    hyp = Main.rand.Next(0, 150) * 0.1f;
                    xvel = Main.rand.Next(-400, 400) * .01f;
                    if (Main.rand.Next(2) == 0)
                    {
                        yvel = (float)Math.Sqrt(hyp - xvel * xvel);
                    }
                    else
                    {
                        yvel = (float)Math.Sqrt(hyp - xvel * xvel) * -1;
                    }
                    Dust.NewDustPerfect(projectile.Center, mod.DustType("Gundust"), new Vector2(xvel, yvel), 0, default(Color), Main.rand.Next(14, 18) * 0.1f);
                }
                healed = true;
            }
        }
    }
}
        
