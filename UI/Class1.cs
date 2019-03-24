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
        const int none = 0;
        const int slagbuster = 1;
        const int slagburst = 2;
        const int slagward = 3;
        static Texture2D iconimg = ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster");
        UIImage Icon1 = new UIImage(iconimg);
       
     
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

            Texture2D frame = ModLoader.GetTexture("ItemLevelTest/UI/Frame");
            UIImage Frame = new UIImage(frame);
            Frame.Left.Set(0, 0f);
            Frame.Top.Set(0, 0f);
            Frame.Width.Set(50, 0f);
            Frame.Height.Set(50, 0f);
            abicon.Append(Frame);
        
            Icon1.Left.Set(6, 0f);
            Icon1.Top.Set(6, 0f);
            Icon1.Width.Set(38, 0f);
            Icon1.Height.Set(38, 0f);
            abicon.Append(Icon1);

            Shade shade = new Shade();
            shade.Left.Set(0,0);
            shade.Top.Set(0,0);
            shade.Width.Set(38, 0);
            shade.Height.Set(38, 0);
            Icon1.Append(shade);



        }
        public int value = 1;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            
            if (ability == none)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Blank"));
            }
            else if (ability == slagbuster)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster"));
                value = 210;
            }
            else if (ability == slagburst)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Slagburst"));
                value = 1800;
            }
            else if (ability == slagward)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Slagward"));
                value = 1;//PH
            }
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //shade.Height.Set((Testsword.cd / value) * 38, 0f);
            Recalculate();

            base.Draw(spriteBatch);
        }
    }
    class Shade : UIElement
    {
        public Color shadecolor = new Color(155, 155, 155, 100);
        private static Texture2D shadetexture = ModLoader.GetTexture("ItemLevelTest/UI/Shade");

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(shadetexture, new Rectangle((int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height), shadecolor);
        }
        
    }
  
}
