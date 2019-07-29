
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ItemLevelTest.Projectiles
{
    class Slagaura:ModProjectile
    {
        public override string Texture
        {
            get { return "ItemLevelTest/Projectiles/Invisible"; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aura of Cinders");
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 300;
            projectile.height = 300;
            projectile.friendly = true;
            projectile.penetrate = 999;
            projectile.timeLeft = 1;
            projectile.tileCollide = false;



        }

        Color dustc = new Color(255,100,0);
       
        public override void AI()
        {

            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(new Vector2(projectile.Center.X - projectile.width / 2, projectile.Center.Y - projectile.height / 2), projectile.width, projectile.height, 264, 0, 0, 0, dustc, 0.5f);
            }
            for(int k = 0; k<= 200; k++)
            {
                if (projectile.getRect().Intersects(Main.npc[k].getRect()) && Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {              
                    Main.npc[k].AddBuff(mod.BuffType("Slagmelt"), 120);
                }
            }

             
            
        }


        public override void Kill(int timeLeft)
        {

        }

   



    }

}
