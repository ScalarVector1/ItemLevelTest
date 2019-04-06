﻿using Microsoft.Xna.Framework;
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
        int value = 1;
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
                value = 1800;
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

    class Upgradeui : UIState
    {
        public UIPanel backdrop;
        public static bool visible = false;
        public UITextBox statwindow;
        public UIImageButton slagbuster;

        public override void OnInitialize()
        {
            backdrop = new UIPanel();
            backdrop.SetPadding(0);
            backdrop.Left.Set(750f, 0f);
            backdrop.Top.Set(250f, 0f);
            backdrop.Width.Set(400f, 0f);
            backdrop.Height.Set(500f, 0f);
            backdrop.BackgroundColor = new Color(120, 40, 20, 170);
            backdrop.BorderColor = new Color(60, 20, 10, 230);
            base.Append(backdrop);

            statwindow = new UITextBox("NULL");
            statwindow.SetPadding(10);
            statwindow.Left.Set(401, 0);
            statwindow.Top.Set(0, 0f);
            statwindow.Height.Set(160, 0);
            statwindow.Width.Set(200, 0);
            statwindow.BackgroundColor = new Color(120, 40, 20, 170);
            statwindow.BorderColor = new Color(60, 20, 10, 230);
            backdrop.Append(statwindow);

            slagbuster = new UIImageButton(ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster"));
            slagbuster.Left.Set(100, 0);
            slagbuster.Top.Set(100, 0);
            slagbuster.Height.Set(38, 0);
            slagbuster.Width.Set(38, 0);
            slagbuster.OnClick += new MouseEvent(Buster);
            backdrop.Append(slagbuster);



        }

        private void Buster(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            statwindow.SetText("test");
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
          
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            Recalculate();

            base.Draw(spriteBatch);
        }

    }

  
}