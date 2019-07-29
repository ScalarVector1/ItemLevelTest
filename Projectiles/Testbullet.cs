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
    class Testbullet : ModProjectile
    {
        public Testgun instance;
        bool hit = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Testbullet");
        }
        public override void SetDefaults()
        {
            projectile.damage = 10;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 60;
            projectile.ranged = true;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
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
            else if(instance.level >= 10)
            {
                instance.exp = 0;
            }
        }

        public override void Kill(int timeLeft)
        {
            if (!hit)
            {
                instance.hits = 0;
                instance.snipertarget = null;
            }
            hit = false;
        }
        

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
          


            if (instance.snipertarget != null && target == instance.snipertarget)
            {
                if (instance.hits < 5)
                {
                    instance.hits++;
                }
                hit = true;
            }
            else
            {
                instance.hits = 0;
                instance.snipertarget = null;
            }

            if (instance.ab1 == 2)
            {
                instance.snipertarget = target;
                damage += ((15 + instance.level * 2) * instance.hits);
                hit = true;
            }           
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2;
            projectile.velocity *= 1.003f;     

            for (int dustcounter = 0; dustcounter <= 3; dustcounter++)
            {
                Dust.NewDust(projectile.position, 10, 10, mod.DustType("Gundust3"), 0, 0, 0, new Color(255, 255, 255), 0.7f);
            }

            if(instance.ab1 == 2)
            {
                projectile.extraUpdates = 2;
            }
        }
    }
}