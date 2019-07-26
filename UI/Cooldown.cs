using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using ItemLevelTest.Items;

namespace ItemLevelTest.UI
{
    class CDUI : UIState
    {
        public UIPanel abicon;
        public static bool visible = false;
        public static int ability = 0;
        public static int maxcd = 0;
        public static int maxcdgun = 0;
        const int none = 0;
        const int slagbuster = 1;
        const int slagburst = 2;
        const int slagward = 3;
        //static Texture2D iconimg = ModContent.GetTexture("ItemLevelTest/UI/Slagbuster");
        UIImage Icon1 = new UIImage(ModContent.GetTexture("ItemLevelTest/UI/Blank"));
        Shade shade = new Shade();
        Exp exp = new Exp();
        public static Koranithus swordinstance;
        public static Testbow bowinstance;
        public static Testspear spearinstance;
        public static Testgun guninstance;


        public override void OnInitialize()
        {
            abicon = new UIPanel();
            abicon.SetPadding(0);
            abicon.Left.Set(465f, 0f);
            abicon.Top.Set(20f, 0f);
            abicon.Width.Set(50f, 0f);
            abicon.Height.Set(50f, 0f);
            abicon.BackgroundColor = new Color(0, 0, 0, 0);
            abicon.BorderColor = new Color(0, 0, 0, 0);
            base.Append(abicon);

            //Texture2D frame = ModContent.GetTexture("ItemLevelTest/UI/Frame");
            UIImage Frame = new UIImage(ModContent.GetTexture("ItemLevelTest/UI/Frame"));
            Frame.Left.Set(0, 0f);
            Frame.Top.Set(0, 0f);
            Frame.Width.Set(66, 0f);
            Frame.Height.Set(50, 0f);
            abicon.Append(Frame);

            Icon1.Left.Set(6, 0f);
            Icon1.Top.Set(6, 0f);
            Icon1.Width.Set(38, 0f);
            Icon1.Height.Set(38, 0f);
            abicon.Append(Icon1);

            shade.Left.Set(0, 0);
            shade.Top.Set(0, 0);
            shade.Width.Set(38, 0);
            shade.Height.Set(38, 0);
            Icon1.Append(shade);

            exp.Left.Set(58, 0);
            exp.Top.Set(4,0);
            exp.Width.Set(4, 0);
            exp.Height.Set(42, 0);
            Frame.Append(exp);



        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (swordinstance != null)
            {
                if (ability == none)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Blank"));
                }
                else if (ability == slagbuster)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Slagbuster"));

                }
                else if (ability == slagburst)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Slagburst"));

                }
                else if (ability == slagward)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Slagward"));

                }
                if (Koranithus.cd >= 1)
                {
                    shade.Height.Set((float)(Koranithus.cd * 38) / maxcd, 0);
                    Recalculate();
                }
                else
                {
                    shade.Height.Set(0, 0);
                    Recalculate();
                }

                exp.Height.Set(((swordinstance.expRequired - swordinstance.exp) * 42) / swordinstance.expRequired, 0);
                Recalculate();
            }
            
            if (bowinstance != null)
            {
                if (ability == none)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Default"));
                }
                else if (ability == slagbuster)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Phantombolt"));
                }

                if (Testbow.charging)
                {
                    shade.Height.Set(38 - ((38 * Testbow.charge)), 0);
                    Recalculate();
                }
                else
                {
                    shade.Height.Set(38, 0);
                    Recalculate();
                }

                exp.Height.Set(((bowinstance.expRequired - bowinstance.exp) * 42) / bowinstance.expRequired, 0);
                Recalculate();
            }

            if(spearinstance != null)
            {
                if (ability == none)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Blank"));
                }
                else if (ability == 1)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Spears"));

                }
                else if (ability == 2)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Whirl"));

                }
                /*else if (ability == slagward)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Slagward"));

                }
                */

                //shade.Height.Set(0, (float)(Koranithus.cd / maxcd) * 100);
                shade.Height.Set((float)((Testspear.maxenergy - Testspear.energy) * 38) / Testspear.maxenergy, 0);
                Recalculate();

                if (Testspear.energy == Testspear.maxenergy)
                {
                    shade.Height.Set(0, 0);
                    Recalculate();
                }

                exp.Height.Set(((spearinstance.expRequired - spearinstance.exp) * 42) / spearinstance.expRequired, 0);
                Recalculate();
            }

            if(guninstance != null)
            {
                if (ability == 0)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Default2"));
                    maxcdgun = 120;
                }
                else if (ability == 1)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Wings"));
                    maxcdgun = 240;

                }
                else if (ability == 2)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Bolt"));
                    maxcdgun = 180;

                }
                else if (ability == 3)
                {
                    Icon1.SetImage(ModContent.GetTexture("ItemLevelTest/UI/Daredevil"));
                    maxcdgun = 180;

                }
                if (Flashhandler.cooldown >= 1)
                {
                    shade.Height.Set((float)(Flashhandler.cooldown * 38) / maxcdgun, 0);
                    Recalculate();
                }
                else
                {
                    shade.Height.Set(0, 0);
                    Recalculate();
                }

                exp.Height.Set(((guninstance.expRequired - guninstance.exp) * 42) / guninstance.expRequired, 0);
                Recalculate();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Recalculate();
        }
    }
    class Shade : UIElement
    {
        public Color shadecolor = new Color(220, 220, 220, 255);
        //private static Texture2D shadetexture = ModContent.GetTexture("ItemLevelTest/UI/Shade");

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(ModContent.GetTexture("ItemLevelTest/UI/Shade"), new Rectangle((int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height), shadecolor);
        }
    }

    class Exp : UIElement
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(ModContent.GetTexture("ItemLevelTest/UI/EXP"), new Rectangle((int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height), new Color(56,40,54));
        }
    }  
}