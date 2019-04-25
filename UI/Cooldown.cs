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

        public UIImageButton burningstrike = new UIImageButton(burningstrikeimage);
        public UIImageButton firebolts = new UIImageButton(fireboltsimage);

        public UIImageButton slagbuster = new UIImageButton(slagbusterimage);
        public UIImageButton slagburst = new UIImageButton(slagbusterimage);
        public UIImageButton slagward = new UIImageButton(slagbusterimage);

        public UIImageButton cinderaura = new UIImageButton(cinderauraimage);

        //frames

        public UIImage slagbusterframe = new UIImage(frame);
        public UIImage slagburstframe = new UIImage(frame);
        public UIImage slagwardframe = new UIImage(frame);
        public UIImage burningstrikeframe = new UIImage(frame);
        public UIImage fireboltsframe = new UIImage(frame);
        public UIImage cinderauraframe = new UIImage(frame);


        public static int passiveselect = 0; //highlighted item
        public static int activeselect = 0;
        public static int ultimateselect = 0;

        public static int ab1; //set by the sword that calls this UI each time
        public static int ab2;
        public static int ab3;

        public static Koranithus instance = null;

        const int burningstrikeability = 1; //constants to deobfuscate
        const int fireboltsability = 2;

        const int slagbusterability = 1; 
        const int slagburstability = 2;
        const int slagwardability = 3;

        const int cinderauraability = 1;

        public static Texture2D frame = ModLoader.GetTexture("ItemLEvelTest/UI/Frame2");
        public static Texture2D check = ModLoader.GetTexture("ItemLEvelTest/UI/check");
        public static Texture2D ex = ModLoader.GetTexture("ItemLEvelTest/UI/ex");
        public static Texture2D frame2 = ModLoader.GetTexture("ItemLEvelTest/UI/Frame3");
        public static Texture2D slagbusterimage = ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster");
        public static Texture2D slagburstimage = ModLoader.GetTexture("ItemLevelTest/UI/Slagburst");
        public static Texture2D slagwardimage = ModLoader.GetTexture("ItemLevelTest/UI/Slagward");
        public static Texture2D burningstrikeimage = ModLoader.GetTexture("ItemLevelTest/UI/burningstrike");
        public static Texture2D fireboltsimage = ModLoader.GetTexture("ItemLevelTest/UI/firebolts");
        public static Texture2D cinderauraimage = ModLoader.GetTexture("ItemLevelTest/UI/cinderaura");
        public static Texture2D lockimage = ModLoader.GetTexture("ItemLevelTest/UI/Blank");

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

            statwindow = new UITextBox("null");
            statwindow.SetPadding(10);
            statwindow.Left.Set(401, 0);
            statwindow.Top.Set(0, 0f);
            statwindow.Height.Set(160, 0);
            statwindow.Width.Set(200, 0);
            statwindow.BackgroundColor = new Color(120, 40, 20, 170);
            statwindow.BorderColor = new Color(60, 20, 10, 230);
            backdrop.Append(statwindow);

            UIImageButton selector = new UIImageButton(check);
            selector.Left.Set(10, 0);
            selector.Top.Set(10, 0);
            selector.Height.Set(46, 0);
            selector.Width.Set(46, 0);
            selector.OnClick += new MouseEvent(Select);
            selector.OnClick += new MouseEvent(Exit);
            backdrop.Append(selector);

            UIImageButton exit = new UIImageButton(ex);
            exit.Left.Set(60, 0);
            exit.Top.Set(10, 0);
            exit.Height.Set(46, 0);
            exit.Width.Set(46, 0);
            exit.OnClick += new MouseEvent(Exit);
            backdrop.Append(exit);

            burningstrike.Left.Set(150 - 19, 0);
            burningstrike.Top.Set(100, 0);
            burningstrike.Height.Set(38, 0);
            burningstrike.Width.Set(38, 0);
            burningstrike.OnClick += new MouseEvent(Strike);
            backdrop.Append(burningstrike);

            firebolts.Left.Set(250 - 19, 0);
            firebolts.Top.Set(100, 0);
            firebolts.Height.Set(38, 0);
            firebolts.Width.Set(38, 0);
            firebolts.OnClick += new MouseEvent(Bolts);
            backdrop.Append(firebolts);

            //UIImageButton slagbuster = new UIImageButton(slagbusterimage);
            slagbuster.Left.Set(100 - 19, 0);
            slagbuster.Top.Set(200, 0);
            slagbuster.Height.Set(38, 0);
            slagbuster.Width.Set(38, 0);
            slagbuster.OnClick += new MouseEvent(Buster);
            backdrop.Append(slagbuster);

            //UIImageButton slagburst = new UIImageButton(slagburstimage);
            slagburst.Left.Set(200 - 19, 0);
            slagburst.Top.Set(200, 0);
            slagburst.Height.Set(38, 0);
            slagburst.Width.Set(38, 0);
            slagburst.OnClick += new MouseEvent(Burst);
            backdrop.Append(slagburst);

            //UIImageButton slagward = new UIImageButton(slagwardimage);
            slagward.Left.Set(300 - 19, 0);
            slagward.Top.Set(200, 0);
            slagward.Height.Set(38, 0);
            slagward.Width.Set(38, 0);
            slagward.OnClick += new MouseEvent(Ward);
            backdrop.Append(slagward);

            cinderaura.Left.Set(200 - 19, 0);
            cinderaura.Top.Set(300, 0);
            cinderaura.Height.Set(38, 0);
            cinderaura.Width.Set(38, 0);
            cinderaura.OnClick += new MouseEvent(Aura);
            backdrop.Append(cinderaura);

            //------------------------------------------------------------------------
            //Frames

            
            slagbusterframe.Left.Set(-4, 0);
            slagbusterframe.Top.Set(-4, 0);
            slagbusterframe.Height.Set(46, 0);
            slagbusterframe.Width.Set(46, 0);
            slagbuster.Append(slagbusterframe);

            slagburstframe.Left.Set(-4, 0);
            slagburstframe.Top.Set(-4, 0);
            slagburstframe.Height.Set(46, 0);
            slagburstframe.Width.Set(46, 0);
            slagburst.Append(slagburstframe);

            slagwardframe.Left.Set(-4, 0);
            slagwardframe.Top.Set(-4, 0);
            slagwardframe.Height.Set(46, 0);
            slagwardframe.Width.Set(46, 0);
            slagward.Append(slagwardframe);

            burningstrikeframe.Left.Set(-4, 0);
            burningstrikeframe.Top.Set(-4, 0);
            burningstrikeframe.Height.Set(46, 0);
            burningstrikeframe.Width.Set(46, 0);
            burningstrike.Append(burningstrikeframe);

            fireboltsframe.Left.Set(-4, 0);
            fireboltsframe.Top.Set(-4, 0);
            fireboltsframe.Height.Set(46, 0);
            fireboltsframe.Width.Set(46, 0);
            firebolts.Append(fireboltsframe);

            cinderauraframe.Left.Set(-4, 0);
            cinderauraframe.Top.Set(-4, 0);
            cinderauraframe.Height.Set(46, 0);
            cinderauraframe.Width.Set(46, 0);
            cinderaura.Append(cinderauraframe);


        }

        private void Exit(UIMouseEvent evt, UIElement listeningElement)
        {
            visible = false;
            passiveselect = 0;
            activeselect = 0;
            ultimateselect = 0;
        }

        private void Select(UIMouseEvent evt, UIElement listeningElement)
        {

            Main.PlaySound(SoundID.Item100);            
            if(ab1 == 0)
            {
                ab1 = passiveselect;
            }
            if(ab2 == 0)
            {
                ab2 = activeselect;
            }
            if(ab3 == 0)
            {
                ab3 = ultimateselect;
            }

            instance.ab1 = ab1;
            instance.ab2 = ab2;
            instance.ab3 = ab3;

            passiveselect = 0;
            activeselect = 0;
            ultimateselect = 0;
        }

        private void Strike(UIMouseEvent evt, UIElement listeningElement)
        {
            if ((ab1 == 0 || ab1 == 1) && instance.level >= 2)
            {
                Main.PlaySound(SoundID.MenuTick);
                passiveselect = burningstrikeability;
                activeselect = 0;
                ultimateselect = 0;
            }
            else
            {
                Main.PlaySound(SoundID.Unlock);
            }
        }

        private void Bolts(UIMouseEvent evt, UIElement listeningElement)
        {
            if ((ab1 == 0 || ab1 == 2) && instance.level >= 2)
            {
                Main.PlaySound(SoundID.MenuTick);
                passiveselect = fireboltsability;
                activeselect = 0;
                ultimateselect = 0;
            }
            else
            {
                Main.PlaySound(SoundID.Unlock);
            }
        }



        private void Buster(UIMouseEvent evt, UIElement listeningElement)
        {
            if ((ab2 == 0 || ab2 == 1) && instance.level >= 5)
            {
                Main.PlaySound(SoundID.MenuTick);
                activeselect = slagbusterability;
                passiveselect = 0;
                ultimateselect = 0;
            }
            else
            {
                Main.PlaySound(SoundID.Unlock);
            }
        }

        private void Burst(UIMouseEvent evt, UIElement listeningElement)
        {
            if ((ab2 == 0 || ab2 == 2) && instance.level >= 5)
            {
                Main.PlaySound(SoundID.MenuTick);
                activeselect = slagburstability;
                passiveselect = 0;
                ultimateselect = 0;
            }
            else
            {
                Main.PlaySound(SoundID.Unlock);
            }
        }

        private void Ward(UIMouseEvent evt, UIElement listeningElement)
        {
            if ((ab2 == 0 || ab2 == 3) && instance.level >= 5)
            {
                Main.PlaySound(SoundID.MenuTick);
                activeselect = slagwardability;
                passiveselect = 0;
                ultimateselect = 0;
            }
            else
            {
                Main.PlaySound(SoundID.Unlock);
            }

        }

        private void Aura(UIMouseEvent evt, UIElement listeningElement)
        {
            if ((ab3 == 0 || ab3 == 1) && instance.level >= 8)
            {
                Main.PlaySound(SoundID.MenuTick);
                activeselect = 0;
                passiveselect = 0;
                ultimateselect = cinderauraability;
            }
            else
            {
                Main.PlaySound(SoundID.Unlock);
            }

        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (passiveselect != 0)
            {
                if (passiveselect == burningstrikeability) //passive text sets
                {
                    statwindow.SetText("Burning strike");
                    burningstrikeframe.SetImage(frame2);
                    fireboltsframe.SetImage(frame);
                }
                else if (passiveselect == fireboltsability)
                {
                    statwindow.SetText("Firebolts");
                    burningstrikeframe.SetImage(frame);
                    fireboltsframe.SetImage(frame2);
                }
            }
            else
            {
                burningstrikeframe.SetImage(frame);
                fireboltsframe.SetImage(frame);
            }

            if (activeselect != 0)
            {
                if (activeselect == slagbusterability) //active text sets
                {
                    statwindow.SetText("Slag Buster");
                    slagbusterframe.SetImage(frame2);
                    slagburstframe.SetImage(frame);
                    slagwardframe.SetImage(frame);
                }
                else if (activeselect == slagburstability)
                {
                    statwindow.SetText("Slagburst");
                    slagbusterframe.SetImage(frame);
                    slagburstframe.SetImage(frame2);
                    slagwardframe.SetImage(frame);
                }
                else if (activeselect == slagwardability)
                {
                    statwindow.SetText("Slag Ward");
                    slagbusterframe.SetImage(frame);
                    slagburstframe.SetImage(frame);
                    slagwardframe.SetImage(frame2);
                }
            }
            else
            {
                slagbusterframe.SetImage(frame);
                slagburstframe.SetImage(frame);
                slagwardframe.SetImage(frame);
            }

            if (ultimateselect != 0)
            {
                if (ultimateselect == cinderauraability) //ultimate text sets
                {
                    statwindow.SetText("Aura of Cinders");
                    cinderauraframe.SetImage(frame2);
                }
            }
            else
            {
                cinderauraframe.SetImage(frame);
            }


            if (activeselect == 0 && passiveselect == 0 && ultimateselect == 0)
            {
                statwindow.SetText("Click an ability to see details");
            }

            //------------------------------------------------------------------------
            //------------------------------------------------------------------------

            if (ab1 == 0 && instance.level >= 2)
            {
                burningstrike.SetImage(burningstrikeimage);
                firebolts.SetImage(fireboltsimage);
            }

            else if (ab1 == 0 && instance.level < 2)
            {
                burningstrike.SetImage(lockimage);
                firebolts.SetImage(lockimage);
            }

            else if (ab1 == burningstrikeability)
            {
                burningstrike.SetImage(burningstrikeimage);
                firebolts.SetImage(lockimage);
            }

            else if (ab1 == fireboltsability)
            {
                burningstrike.SetImage(lockimage);
                firebolts.SetImage(fireboltsimage);
            }

            //------------------------------------------------------------------------

            if (ab2 == 0 && instance.level >= 5)
            {
                slagbuster.SetImage(slagbusterimage);
                slagburst.SetImage(slagburstimage);
                slagward.SetImage(slagwardimage);
            }
            else if (ab2 == 0 && instance.level < 5)
            {
                slagbuster.SetImage(lockimage);
                slagburst.SetImage(lockimage);
                slagward.SetImage(lockimage);
            }
            else if (ab2 == slagbusterability)
            {
                slagbuster.SetImage(slagbusterimage);
                slagburst.SetImage(lockimage);
                slagward.SetImage(lockimage);
            }

            else if (ab2 == slagburstability)
            {
                slagbuster.SetImage(lockimage);
                slagburst.SetImage(slagburstimage);
                slagward.SetImage(lockimage);
            }

            else if (ab2 == slagwardability)
            {
                slagbuster.SetImage(lockimage);
                slagburst.SetImage(lockimage);
                slagward.SetImage(slagwardimage);
            }

            //------------------------------------------------------------------------

            if (ab3 == 0 && instance.level >= 8)
            {
                cinderaura.SetImage(cinderauraimage);
            }

            else if (ab3 == 0 && instance.level < 8)
            {
                cinderaura.SetImage(lockimage);
            }

            else if (ab3 == burningstrikeability)
            {
                cinderaura.SetImage(cinderauraimage);
            }

            //------------------------------------------------------------------------

        }

        public override void Draw(SpriteBatch spriteBatch)
        {           
            Recalculate();
            base.Draw(spriteBatch);
        }

    }


  
}
