
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;



namespace ItemLevelTest.Projectiles
{

    class Backdrop : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 208;
            projectile.height = 156;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;

        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * 0.5f;        
        }



        public override void AI()
        {

            if (instance.slot1 == 0 && instance.slot2 == 0 && instance.slot3 == 0 && instance.slot4 == 0)
            {
                if (instance.swordmade)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + 4, projectile.position.Y + 54), 4, 4, mod.DustType("Sworddustaltar"));
                }
                if (instance.bowmade)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + 28, projectile.position.Y + 28), 4, 4, mod.DustType("Bowdustaltar"));
                }
                if (instance.staffmade)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + 58, projectile.position.Y + 8), 4, 4, mod.DustType("Staffdustaltar"));
                }
                if (instance.spearmade)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + 142, projectile.position.Y + 8), 4, 4, mod.DustType("Speardustaltar"));
                }
                if (instance.gunmade)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + 172, projectile.position.Y + 28), 4, 4, mod.DustType("Gundustaltar"));
                }
                if (instance.orbmade)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + 196, projectile.position.Y + 54), 4, 4, mod.DustType("Orbdustaltar"));
                }
                if(instance.pickmade)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + 95, projectile.position.Y + 32), 14, 14, mod.DustType("Pickdustaltar"));
                }
            }

            projectile.timeLeft = 2;
            if ( instance.slot1 == 0 && instance.slot2 == 0 && instance.slot3 == 0 && instance.slot4 == 0)
            {
                projectile.frame = 0;
            }
            if (instance.slot1 == mod.ItemType("Swordsteel1") && instance.slot2 == mod.ItemType("Swordsoul1") && instance.slot3 == mod.ItemType("Swordlog1") && instance.slot4 == mod.ItemType("Swordlogadd1"))
            {
                projectile.frame = 1;
            }
        }
    }

    //------------------------------------------------------------------------------------------------

    class Sword1 : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 30;
            projectile.height = 192 / 8;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }



        float timer = 0;

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Sworddust"));
            }
            if (instance.slot1 == mod.ItemType("Swordsteel1") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot1 != mod.ItemType("Swordsteel1") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.frame = 0;
                }
            }

            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Sword2 : ModProjectile
        {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("");
                Main.projFrames[projectile.type] = 6;
            }
            public override void SetDefaults()
            {
                projectile.damage = 0;
                projectile.width = 36;
                projectile.height = 228 / 6;
                projectile.friendly = true;
                projectile.penetrate = -1;
                projectile.timeLeft = 2;
                projectile.tileCollide = false;
            }


            float timer = 0;
            public override void AI()
            {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Sworddust"));
            }
            if (instance.slot2 == mod.ItemType("Swordsoul1") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot2 != mod.ItemType("Swordsoul1") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;
                if (++projectile.frameCounter >= 5)
                {
                    projectile.frameCounter = 0;
                    if (++projectile.frame >= 6)
                    {
                        projectile.frame = 0;
                    }
                }

                timer += 0.1256f;
                if (timer >= 6.28)
                {
                    timer = 0;
                }
            }
        }

    class Sword3 : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            //Main.projFrames[projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }



        float timer = 0;
        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Sworddust"));
            }
            if (instance.slot3 == mod.ItemType("Swordlog1") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot3 != mod.ItemType("Swordlog1") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;
            /*if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }*/

            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Sword4 : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }


        float timer = 0;
        public override void AI()
        {
            if(Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Sworddust"));
            }
            
            if (instance.slot4 == mod.ItemType("Swordlogadd1") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot4 != mod.ItemType("Swordlogadd1") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }

            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    //------------------------------------------------------------------------------------------------

    class Spear1p : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 36;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }

        float timer = 0;

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Speardust"));
            }
            if (instance.slot1 == mod.ItemType("Spear1") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot1 != mod.ItemType("Spear1") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;


            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Spear2p : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");

        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 36;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }

        float timer = 0;

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Speardust"));
            }
            if (instance.slot2 == mod.ItemType("Spear2") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot2 != mod.ItemType("Spear2") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;


            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Spear3p : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");

        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 36;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }

        float timer = 0;

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Speardust"));
            }
            if (instance.slot3 == mod.ItemType("Spear3") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot3 != mod.ItemType("Spear3") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;


            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Spear4p : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");

        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 36;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }

        float timer = 0;

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Speardust"));
            }
            if (instance.slot4 == mod.ItemType("Spear4") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot4 != mod.ItemType("Spear4") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;


            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    //------------------------------------------------------------------------------------------------

    class Gun1 : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 30;
            projectile.height = 234 / 9;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }



        float timer = 0;

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Gundust"));
            }
            if (instance.slot1 == mod.ItemType("Gun1") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot1 != mod.ItemType("Gun1") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 9)
                {
                    projectile.frame = 0;
                }
            }

            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Gun2 : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 48;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }


        float timer = 0;
        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Gundust"));
            }
            if (instance.slot2 == mod.ItemType("Gun2") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot2 != mod.ItemType("Gun2") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.frame = 0;
                }
            }

            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Gun3 : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 10;
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 40;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }



        float timer = 0;
        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Gundust"));
            }
            if (instance.slot3 == mod.ItemType("Gun3") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot3 != mod.ItemType("Gun3") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 10)
                {
                    projectile.frame = 0;
                }
            }

            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }

    class Gun4 : ModProjectile
    {
        public Tiles.Altar.AltarEntity instance;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 34;
            projectile.height = 34;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }


        float timer = 0;
        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, 32, 32, mod.DustType("Gundust"));
            }

            if (instance.slot4 == mod.ItemType("Gun4") && !instance.crafting)
            {
                projectile.timeLeft = 30;
            }
            if (instance.slot4 != mod.ItemType("Gun4") || instance.crafting)
            {
                projectile.alpha += 255 / 30;
            }

            projectile.velocity.Y = (float)Math.Sin(timer) / 2;

            timer += 0.1256f;
            if (timer >= 6.28)
            {
                timer = 0;
            }
        }
    }
}
    
