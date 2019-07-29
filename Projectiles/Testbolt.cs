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
    class Testbolt : ModProjectile
    {
        public bool invert = false;
        public Teststaff instance;

        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testbolt");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 3;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (instance.level < 10 && target.type != NPCID.TargetDummy)
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
            else if (instance.level >= 10)
            {
                instance.exp = 0;
            }
        }
        float timer = (float)Math.PI / 2;

        public override void AI()
        {
            float rot = projectile.velocity.ToRotation();

            if (!invert)
            {
                projectile.position.X += (float)Math.Sin(rot) * ((float)Math.Sin(timer) * 8);
                projectile.position.Y += (float)Math.Cos(rot) * ((float)Math.Sin(timer) * -8);

                for (int dustcounter = 0; dustcounter <= 2; dustcounter++)
                {
                    Dust.NewDust(projectile.position, 16, 16, mod.DustType("Staffdust"), 0, 0, 0, new Color(255, 255, 255));

                    if(instance.ab1 == 2)
                    {
                        Dust.NewDust(projectile.position, 16, 16, mod.DustType("Staffdust3"), 0, 0, 0, new Color(255, 255, 255), 0.4f);
                        projectile.penetrate = 1;
                    }
                    if (instance.ab1 == 1)
                    {
                        projectile.penetrate = 7;
                    }
                }
            }
            else
            {
                projectile.position.X += (float)Math.Sin(rot) * ((float)Math.Sin(timer) * -8);
                projectile.position.Y += (float)Math.Cos(rot) * ((float)Math.Sin(timer) * 8);
                projectile.penetrate = 7;

                for (int dustcounter = 0; dustcounter <= 2; dustcounter++)
                {
                    Dust.NewDust(projectile.position, 16, 16, mod.DustType("Staffdust3"), 0, 0, 0, new Color(255, 255, 255), 0.8f);
                }
            }




            timer += ((float)Math.PI * 2) / 15;

            if(timer >= Math.PI * 2)
            {
                timer = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            if(instance.ab1 == 2)
            {
                for (int dustcounter = 120; dustcounter >= 0; dustcounter--)
                {
                    float yvel = 0;
                    float xvel = 0;
                    float hyp = 0;
                    hyp = Main.rand.Next(0, 170) * 0.1f;
                    xvel = Main.rand.Next(-400, 400) * .01f;
                    if (Main.rand.Next(2) == 0)
                    {
                        yvel = (float)Math.Sqrt(hyp - xvel * xvel);
                    }
                    else
                    {
                        yvel = (float)Math.Sqrt(hyp - xvel * xvel) * -1;
                    }
                    Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Staffdust"), new Vector2(xvel*1.8f, yvel * 1.8f), 0, default, Main.rand.Next(12, 16) * 0.1f);
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDustPerfect(new Vector2(projectile.Center.X, projectile.Center.Y), mod.DustType("Staffdust3"), new Vector2(xvel * 2, yvel * 2), 0, default, Main.rand.Next(6, 8) * 0.1f);
                    }
                    Main.PlaySound(SoundID.Item38, projectile.Center);               
                }

                Projectile.NewProjectile(projectile.position, new Vector2(0, 0), mod.ProjectileType("Boltboom"), 50 + instance.level * 5, 2.5f, projectile.owner);
            }
        }
    }

    class Boltboom : ModProjectile
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }
        public override void SetDefaults()
        {
            projectile.damage = 65;
            projectile.width = 100;
            projectile.height = 100;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 5;
        }
    }
}
