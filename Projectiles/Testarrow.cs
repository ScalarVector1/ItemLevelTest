
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
    class Testarrow : ModProjectile
    {
        public Testbow instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testarrow");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 26;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = 1;

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
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.velocity.Y += 0.2f;
            for (int dustcounter = 0; dustcounter <= 3; dustcounter++)
            {
              Dust.NewDust(projectile.position, 16, 16, mod.DustType("Bowdust4"),0,0,0, new Color(255, 255, 255));
            }
        }
    }


    class Testarrow2 : ModProjectile
    {
        public Testbow instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testarrow2");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 26;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = 1;

        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.velocity.Y += 0.2f;
            for (int dustcounter = 0; dustcounter <= 4; dustcounter++)
            {
                Dust.NewDust(projectile.position, 20, 20, mod.DustType("Bowdust"), 0, 0, 0, new Color(255, 255, 255));
            }
        }
    }


    class Phantomarrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testarrow");
        }
        public override void SetDefaults()
        {
            projectile.damage = 20;
            projectile.width = 26;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = 10;
            projectile.tileCollide = false;
            projectile.scale = 1.5f;
            projectile.timeLeft = 300;

        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            for (int dustcounter = 0; dustcounter <= 6; dustcounter++)
            {
                Dust.NewDust(projectile.position, 24, 24, mod.DustType("Bowdust"), 0, 0, 0, new Color(255, 255, 255));
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

                target.AddBuff(mod.BuffType("Phantomburn"), 300);
                for (int dustcounter = 0; dustcounter <= 50; dustcounter++)
                {
                    Dust.NewDust(target.position, target.width, target.height, mod.DustType("Bowdust"), 0, 0, 0, new Color(255, 255, 255));
                }
            
        }
    }



}
