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
    class Spearwhirl1 : ModProjectile
    {

        float Initialvel = 3;
        float hyp = 0;
        float y = 0;
        float x = 0;

        public void Getvel(float vel)
        {
            Initialvel = vel;
        }

        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Spearwhirl"; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testspear");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 100;
            projectile.height = 100;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.tileCollide = false;
        }

        public int timer = 0;

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];


            if (timer == 0)
            {
                projectile.velocity.Y = Initialvel;
            }
            if (timer < 60)
            {
                if (projectile.velocity.Y < 0)
                {
                    projectile.velocity.Y += 0.25f;
                }
                if (projectile.velocity.Y > 0)
                {
                    projectile.velocity.Y -= 0.25f;
                }
                projectile.position.X += player.velocity.X;
                projectile.position.Y += player.velocity.Y;
                projectile.rotation = (float)Math.PI / 2 + (float)Math.PI / 4;
            }

            if(timer == 60)
            {
                hyp = (player.Center.Y - projectile.Center.Y);
            }

            if (timer >= 60)
            {
                projectile.rotation -= ((float)Math.PI / 4 + (float)Math.PI);
                projectile.rotation += (float)(Math.PI * 2 / 90) ;

                y = (float)Math.Sin(projectile.rotation) * hyp;
                x = (float)Math.Cos(projectile.rotation) * hyp;
                projectile.position = new Vector2(player.MountedCenter.X - projectile.width / 2 + x, player.MountedCenter.Y - projectile.height / 2 + y);
                projectile.rotation += (float)Math.PI / 4 + (float)Math.PI;

            }
            if (timer >= 120)
            {
                timer = 60;
            }
            for(int d = 0; d<2; d++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Speardust"),0,0,0,default(Color),0.5f);
            }

            if (!Main.mouseRight || Testspear.energy <= 0 || player.HeldItem.type != mod.ItemType("Testspear"))
            {
                projectile.timeLeft = 0;
                for (int d = 0; d < 40; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Sworddust2"), 0, 0, 0, default(Color), 0.5f);
                }
            }
            timer++;
        }
        public override bool PreAI()
        {
            Getvel(5);
            return true;
        }
    }
    class Spearwhirl2 : Spearwhirl1
    {
        public override bool PreAI()
        {
            Getvel(8.5f);
            return true;
        }    
    }

    class Spearwhirl3 : Spearwhirl1
    {
        public override bool PreAI()
        {
            Getvel(11);
            return true;
        }
    }

    class Spearwhirl4 : Spearwhirl1
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Spearwhirlback"; }
        }
        float y1;
        float x1;
        public override bool PreAI()
        {
            Player player = Main.player[Main.myPlayer];
            Getvel(13);
            if (timer >= 60)
            {

                    y1 = (float)Math.Sin(projectile.rotation - Math.PI / 4) * 382;
                    x1 = (float)Math.Cos(projectile.rotation - Math.PI / 4) * 382;
                    Dust.NewDustPerfect(new Vector2(player.MountedCenter.X + x1, player.MountedCenter.Y + y1), mod.DustType("Speardust"), new Vector2(Main.rand.Next(-10, 10) * 0.1f, Main.rand.Next(-10, 10) * 0.1f), 0, default(Color), 1.1f);
                
            }
            return true;
        }
    }

    class Spearwhirl5 : Spearwhirl1
    {
        public override bool PreAI()
        {
            Getvel(-5);
            return true;
        }
    }

    class Spearwhirl6 : Spearwhirl1
    {
        public override bool PreAI()
        {
            Getvel(-8.5f);
            return true;
        }
    }

    class Spearwhirl7 : Spearwhirl1
    {
        public override bool PreAI()
        {
            Getvel(-11);
            return true;
        }
    }

    class Spearwhirl8 : Spearwhirl1
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Spearwhirlfront"; }
        }
        float y1;
        float x1;
        public override bool PreAI()
        {
            Player player = Main.player[Main.myPlayer];
            Getvel(-13);
            if (timer >= 60)
            {

                    y1 = (float)Math.Sin(projectile.rotation - Math.PI / 4) * -382;
                    x1 = (float)Math.Cos(projectile.rotation - Math.PI / 4) * -382;
                    Dust.NewDustPerfect(new Vector2(player.MountedCenter.X + x1, player.MountedCenter.Y + y1), mod.DustType("Speardust"), new Vector2(Main.rand.Next(-10, 10) * 0.1f, Main.rand.Next(-10, 10) * 0.1f), 0, default(Color), 1.1f);
                
            }
                return true;
        }
    }
}
