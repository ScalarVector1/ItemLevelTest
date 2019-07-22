
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;
using ItemLevelTest.Items;
using ItemLevelTest.Dusts;

namespace ItemLevelTest.Projectiles
{
    class Testspearproj : ModProjectile
    {
        public Testspear instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testspear");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 116;
            projectile.height = 116;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.tileCollide = false;

        }

        int timer = 0;
        
        public override void AI()
        {



            Player player = Main.LocalPlayer;
            projectile.position.X += player.velocity.X;
            projectile.position.Y += player.velocity.Y;
            if (timer > 0)
            {
                timer--;
            }

            if (timer == 0)
            {
                timer = 30;
            }
           
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2 + (float)Math.PI / 4;

            if (timer == 15)
            {
                projectile.velocity.X = -projectile.velocity.X;
                projectile.velocity.Y = -projectile.velocity.Y;            
            }
            if(timer < 15)
            {
                projectile.rotation += (float)Math.PI;
            }
            if(timer == 1)
            {
                projectile.timeLeft = 0;
            }
        }
    }

    

        class Testspearproj2 : ModProjectile
        {
            public Testspear instance;
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Testspear");
            }
            public override void SetDefaults()
            {
                projectile.damage = 20;
                projectile.width = 14;
                projectile.height = 14;
                projectile.friendly = true;
                projectile.penetrate = -1;
                projectile.melee = true;
                projectile.tileCollide = false;

        }

            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                if (instance.level < 10)
                {
                    if (damage >= 10)
                    {
                        instance.exp += (damage / 10);
                    }
                    else
                    {
                        instance.exp++;
                    }
                    instance.Expcalc();
                }
                if(instance.ab3 == 1)
                {
                Testspear.energy += 30;

                for(int d = 0; d < 15; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, mod.DustType("Speardust"));
                }
                
                }
            }

            int timer = 0;

            public override void AI()
            {
                Player player = Main.LocalPlayer;
                projectile.position.X += player.velocity.X;
                projectile.position.Y += player.velocity.Y;
                if (timer > 0)
                {
                    timer--;
                }

                if (timer == 0)
                {
                    timer = 30;
                }

                if (timer == (15))
                {
                    projectile.velocity.X = -projectile.velocity.X;
                    projectile.velocity.Y = -projectile.velocity.Y;
                }

                if (timer == 1)
                {
                    projectile.timeLeft = 0;
                }
            }
        }

    class Testspearproj3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testspear");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.tileCollide = false;

        }

        int timer = 0;

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            projectile.position.X += player.velocity.X;
            projectile.position.Y += player.velocity.Y;
            if (timer > 0)
            {
                timer--;
            }

            if (timer == 0)
            {
                timer = 30;
            }

            if (timer == (15))
            {
                projectile.velocity.X = -projectile.velocity.X;
                projectile.velocity.Y = -projectile.velocity.Y;
            }

            if (timer == 1)
            {
                projectile.timeLeft = 0;
            }
        }
    }

    class Testspearproj4 : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testspear");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 116;
            projectile.height = 116;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.tileCollide = false;

        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * 0.5f;
        }
        int timer = 0;

        public override void AI()
        {



            Player player = Main.LocalPlayer;
            projectile.position.X += player.velocity.X;
            projectile.position.Y += player.velocity.Y;
            if (timer > 0)
            {
                timer--;
            }

            if (timer == 0)
            {
                timer = 30;
            }

            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2 + (float)Math.PI / 4;

            if (timer == 15)
            {
                projectile.velocity.X = -projectile.velocity.X;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            if (timer < 15)
            {
                projectile.rotation += (float)Math.PI;
            }
            if (timer == 1)
            {
                projectile.timeLeft = 0;
            }
        }
    }

}