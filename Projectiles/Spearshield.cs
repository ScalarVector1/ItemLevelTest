using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;
using ItemLevelTest.Items;

namespace ItemLevelTest.Projectiles
{
    class Spearshield1: ModProjectile
    {

        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Spearshield"; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crescent Bastion");
        }

        public override void SetDefaults()
        {
            projectile.damage = 2;
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;



        }
        public float x = 0;
        public float y = 0;
        public float r = 80;
        public float off = 0;

        public override void AI()
        {
            projectile.timeLeft = 2;


            Player player = Main.player[Main.myPlayer];

            float x = (Main.screenPosition.X + Main.mouseX - 20) - player.position.X;
            float y = (Main.screenPosition.Y + Main.mouseY - 20) - player.position.Y;

            float xvel = (80 * x) / (float)Math.Sqrt(x * x + y * y);
            float yvel = (80 * y) / (float)Math.Sqrt(x * x + y * y);

            projectile.position = new Vector2(player.MountedCenter.X - projectile.width / 2 + xvel, player.MountedCenter.Y - projectile.height / 2 + yvel).RotatedBy(off, new Vector2(player.MountedCenter.X - projectile.width / 2, player.MountedCenter.Y - projectile.height / 2 ));


            for (int k = 0; k <= 1000; k++)
            {
                if (projectile.getRect().Intersects(Main.projectile[k].getRect()) && !Main.projectile[k].friendly)
                {
                    Main.projectile[k].active = false;
                    Main.projectile[k].position = new Vector2(0,0);
                    if (Testspear.energy >= 50)
                    {
                        Testspear.energy -= 50;
                    }
                    else
                    {
                        projectile.timeLeft = 0;
                        Testspear.energy = 0;
                    }                                    
                }
            }

            if (!Main.mouseRight || Testspear.energy <= 50 || player.HeldItem.type != mod.ItemType("Testspear"))
            {
                projectile.timeLeft = 0;
                for (int d = 0; d < 40; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Sworddust2"), 0, 0, 0, default, 0.5f);
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Testspear.energy >= 50)
            {
                Testspear.energy -= 50;
            }
            else
            {
                projectile.timeLeft = 0;
                Testspear.energy = 0;
            }
        }
    }
    class Spearshield2 : Spearshield1
    {
        public override bool PreAI()
        {
            off = (float)Math.PI / 8;
            return true;
        }     
    }
    class Spearshield3 : Spearshield1
    {
        public override bool PreAI()
        {
            off = (float)-Math.PI / 8;
            return true;
        }
    }
    class Spearshield4 : Spearshield1
    {
        public override bool PreAI()
        {
            off = (float)Math.PI / 4;
            return true;
        }
    }
    class Spearshield5 : Spearshield1
    {
        public override bool PreAI()
        {
            off = (float)-Math.PI / 4;
            return true;
        }
    }
}
