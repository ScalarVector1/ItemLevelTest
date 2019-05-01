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
        public static bool coolingdown = false;
        const int none = 0;
        const int slagbuster = 1;
        const int slagburst = 2;
        const int slagward = 3;
        //static Texture2D iconimg = ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster");
        UIImage Icon1 = new UIImage(ModLoader.GetTexture("ItemLevelTest/UI/Blank"));
        Shade shade = new Shade();


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

            //Texture2D frame = ModLoader.GetTexture("ItemLevelTest/UI/Frame");
            UIImage Frame = new UIImage(ModLoader.GetTexture("ItemLevelTest/UI/Frame"));
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

            shade.Left.Set(0,0);
            shade.Top.Set(0,0);
            shade.Width.Set(38, 0);
            shade.Height.Set(38, 0);
            Icon1.Append(shade);



        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            
            if (ability == none)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Blank"));
            }
            else if (ability == slagbuster)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster"));
   
            }
            else if (ability == slagburst)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Slagburst"));

            }
            else if (ability == slagward)
            {
                Icon1.SetImage(ModLoader.GetTexture("ItemLevelTest/UI/Slagward"));

            }
            if (coolingdown)
            {
                //shade.Height.Set(0, (float)(Koranithus.cd / maxcd) * 100);
                shade.Height.Set((float)(Koranithus.cd * 38)/maxcd, 0);
                Recalculate();
            }
            else
            {
                shade.Height.Set(0, 0);
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
        //private static Texture2D shadetexture = ModLoader.GetTexture("ItemLevelTest/UI/Shade");

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            spriteBatch.Draw(ModLoader.GetTexture("ItemLevelTest/UI/Shade"), new Rectangle((int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height), shadecolor);
        }
        
    }


    class Upgradeui : UIState
    {
        public UIPanel backdrop;
        public static bool visible = false;
        public UIPanel statwindow;

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

        public UIImageButton burningstrike = new UIImageButton(burningstrikeimage);
        public UIImageButton firebolts = new UIImageButton(fireboltsimage);

        public UIImageButton slagbuster = new UIImageButton(slagbusterimage);
        public UIImageButton slagburst = new UIImageButton(slagbusterimage);
        public UIImageButton slagward = new UIImageButton(slagbusterimage);

        public UIImageButton cinderaura = new UIImageButton(cinderauraimage);

        public UIText line1 = new UIText("null");
        public UIText line2 = new UIText("null");
        public UIText line3 = new UIText("null");
        public UIText line4 = new UIText("null");
        public UIText line5 = new UIText("null");
        public UIText line6 = new UIText("null");
        public UIText line7 = new UIText("null");

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
        public static int level;

        public static Koranithus instance = null;

        const int burningstrikeability = 1; //constants to deobfuscate
        const int fireboltsability = 2;

        const int slagbusterability = 1; 
        const int slagburstability = 2;
        const int slagwardability = 3;

        const int cinderauraability = 1;

        public override void OnInitialize()
        {

            backdrop = new UIPanel();
            backdrop.SetPadding(0);
            backdrop.Left.Set(750f, 0f);
            backdrop.Top.Set(250f, 0f);
            backdrop.Width.Set(400f, 0f);
            backdrop.Height.Set(438f, 0f);
            backdrop.BackgroundColor = new Color(110, 40, 20, 170);
            backdrop.BorderColor = new Color(60, 20, 10, 230);
            base.Append(backdrop);

            statwindow = new UIPanel();
            statwindow.SetPadding(0);
            statwindow.Left.Set(401, 0);
            statwindow.Top.Set(0, 0f);
            statwindow.Height.Set(220, 0);
            statwindow.Width.Set(200, 0);
            statwindow.BackgroundColor = new Color(100, 50, 20, 140);
            statwindow.BorderColor = new Color(50, 30, 10, 230);
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

            //------------------------------------------------------------------------
            //Text

            line1.Left.Set(10, 0);
            line1.Top.Set(10, 0);
            statwindow.Append(line1);

            line2.Left.Set(10, 0);
            line2.Top.Set(40, 0);
            statwindow.Append(line2);

            line3.Left.Set(10, 0);
            line3.Top.Set(60, 0);
            statwindow.Append(line3);

            line4.Left.Set(10, 0);
            line4.Top.Set(80, 0);
            statwindow.Append(line4);

            line5.Left.Set(10, 0);
            line5.Top.Set(100, 0);
            statwindow.Append(line5);

            line6.Left.Set(10, 0);
            line6.Top.Set(120, 0);
            statwindow.Append(line6);

            line7.Left.Set(10, 0);
            line7.Top.Set(150, 0);
            statwindow.Append(line7);


        }

        private void Exit(UIMouseEvent evt, UIElement listeningElement)
        {
            visible = false;
            passiveselect = 0;
            activeselect = 0;
            ultimateselect = 0;
            Main.PlaySound(SoundID.MenuClose);
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
                    line1.SetText("Burning Strike");
                    line2.SetText("Your melee strikes");
                    line3.SetText("inflict burning");
                    line4.SetText("for " + level * 1 + "-" + ((level * 1) + 1) + " seconds.");
                    line5.SetText("");
                    line6.SetText("");
                    line7.SetText("");

                    burningstrikeframe.SetImage(frame2);
                    fireboltsframe.SetImage(frame);
                }
                else if (passiveselect == fireboltsability)
                {
                    line1.SetText("Firebolts");
                    line2.SetText("Your melee strikes");
                    line3.SetText("have a chance to");
                    line4.SetText("summon burning bolts");
                    line5.SetText("from the sky,");
                    line6.SetText("dealing " + (10+level*5) +" damage.");
                    line7.SetText("");


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
                    line1.SetText("Slag Buster");
                    line2.SetText("Shoot a projectile,");
                    line3.SetText("dealing " + (3*(10 + level * 5)) + " damage");
                    line4.SetText("and exploding on");
                    line5.SetText("impact for 65 danage.");
                    line6.SetText("");
                    line7.SetText("3.5 second cooldown");

                    slagbusterframe.SetImage(frame2);
                    slagburstframe.SetImage(frame);
                    slagwardframe.SetImage(frame);
                }
                else if (activeselect == slagburstability)
                {
                    line1.SetText("Slagburst");
                    line2.SetText("Creates a pillar of");
                    line3.SetText("flame, dealing " + ((10 + level * 5) / 3) * 6 );
                    line4.SetText("damage per second");
                    line5.SetText("at your cursor.");
                    line6.SetText("");
                    line7.SetText("30 second cooldown");

                    slagbusterframe.SetImage(frame);
                    slagburstframe.SetImage(frame2);
                    slagwardframe.SetImage(frame);
                }
                else if (activeselect == slagwardability)
                {
                    line1.SetText("Slag Ward");
                    line2.SetText("Creates 3 shields");
                    line3.SetText("around you, blocking");
                    line4.SetText("projectiles and");
                    line5.SetText("exploding on impact");
                    line6.SetText("for "+ ((10 + level * 5) * 5) + " damage.");
                    line7.SetText("30 second cooldown");

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
                    line1.SetText("Aura of Cinders");
                    line2.SetText("Creates a burning");
                    line3.SetText("aura around you, ");
                    line4.SetText("dealing 10 damage");
                    line5.SetText("per second.");
                    line6.SetText("");
                    line7.SetText("");

                    cinderauraframe.SetImage(frame2);
                }
            }
            else
            {
                cinderauraframe.SetImage(frame);
            }


            if (activeselect == 0 && passiveselect == 0 && ultimateselect == 0)
            {
                line1.SetText("Koranithus Upgrades");
                line2.SetText("Click an ability to");
                line3.SetText("see details, click");
                line4.SetText("the check button");
                line5.SetText("to confirm your");
                line6.SetText("selection.");
                line7.SetText("");
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

    class CHUI : UIState
    {
        public UIPanel abicon;
        public static bool visible = false;
        public static int ability = 0;
        float maxcharge = 1;
        const int none = 0;
        const int slagbuster = 1;
        const int slagburst = 2;
        const int slagward = 3;
        //static Texture2D iconimg = ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster");
        UIImage Icon1 = new UIImage(ModLoader.GetTexture("ItemLevelTest/UI/default"));
        Shade shade = new Shade();


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

            //Texture2D frame = ModLoader.GetTexture("ItemLevelTest/UI/Frame");
            UIImage Frame = new UIImage(ModLoader.GetTexture("ItemLevelTest/UI/Frame"));
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

            shade.Left.Set(0, 0);
            shade.Top.Set(0, 0);
            shade.Width.Set(38, 0);
            shade.Height.Set(38, 0);
            Icon1.Append(shade);


        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (Testbow.charging)
            {
                //shade.Height.Set(0, (float)(Koranithus.cd / maxcd) * 100);
                shade.Height.Set(38-((38 * Testbow.charge)),0);
                Recalculate();
            }
            else
            {
                shade.Height.Set(38, 0);
                Recalculate();
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Recalculate();
        }

    }
    



}
