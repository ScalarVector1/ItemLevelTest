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
    class Typefinder : ModWorld
    {
        static public int vorbtype = 0;
        public override void PostUpdate()
        {
            vorbtype = mod.ItemType("Vorb");
        }
        
    }
class Upgradeui : UIState
{
    public UIPanel backdrop;
    public static bool visible = false;
    public UIPanel statwindow;
    public UIPanel settingswindow;

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
    public static Texture2D vfxtoggleon = ModLoader.GetTexture("ItemLevelTest/UI/vfxtoggleon");
        public static Texture2D thornsimage = ModLoader.GetTexture("ItemLevelTest/UI/Spears");
        public static Texture2D greatspearimage = ModLoader.GetTexture("ItemLevelTest/UI/Whirl");
        public static Texture2D energyvampireimage = ModLoader.GetTexture("ItemLevelTest/UI/Energy");
        public static Texture2D wingsimage = ModLoader.GetTexture("ItemLevelTest/UI/Wings");
        public static Texture2D boltimage = ModLoader.GetTexture("ItemLevelTest/UI/Bolt");
        public static Texture2D dareimage = ModLoader.GetTexture("ItemLevelTest/UI/Daredevil");
        public static Texture2D sniperimage = ModLoader.GetTexture("ItemLevelTest/UI/Sniper");
        public static Texture2D shotgunimage = ModLoader.GetTexture("ItemLevelTest/UI/Shotgun");
        public static Texture2D vfxtoggleoff = ModLoader.GetTexture("ItemLevelTest/UI/vfxtoggleoff");
        public static Texture2D vorb = ModLoader.GetTexture("ItemLevelTest/UI/Vorb");

        public UIImageButton passive1 = new UIImageButton(burningstrikeimage);
    public UIImageButton passive2 = new UIImageButton(fireboltsimage);

    public UIImageButton active1 = new UIImageButton(slagbusterimage);
    public UIImageButton active2 = new UIImageButton(slagbusterimage);
    public UIImageButton active3 = new UIImageButton(slagbusterimage);

    public UIImageButton ultimate1 = new UIImageButton(cinderauraimage);

    public UIImageButton vfxtoggle = new UIImageButton(vfxtoggleon);
        public UIImageButton reset = new UIImageButton(vorb);


    public UIText line1 = new UIText("null");
    public UIText line2 = new UIText("null");
    public UIText line3 = new UIText("null");
    public UIText line4 = new UIText("null");
    public UIText line5 = new UIText("null");
    public UIText line6 = new UIText("null");
    public UIText line7 = new UIText("null");
        public UIText settingstext = new UIText("Fire Trail");
        public UIText resettext = new UIText("x1   Reset Abilities");

        //frames

        public UIImage active1frame = new UIImage(frame);
    public UIImage active2frame = new UIImage(frame);
    public UIImage active3frame = new UIImage(frame);
    public UIImage passive1frame = new UIImage(frame);
    public UIImage passive2frame = new UIImage(frame);
    public UIImage ultimate1frame = new UIImage(frame);


    public static int passiveselect = 0; //highlighted item
    public static int activeselect = 0;
    public static int ultimateselect = 0;

    public static int ab1; //set by the sword that calls this UI each time
    public static int ab2;
    public static int ab3;
    public static int level;

    public static Koranithus swordinstance = null;
    public static Testspear spearinstance = null;
    public static Testbow bowinstance = null;
    public static Testgun guninstance = null;


        const int passive1ability = 1; //constants to deobfuscate
    const int passive2ability = 2;

    const int active1ability = 1;
    const int active2ability = 2;
    const int active3ability = 3;

    const int ultimate1ability = 1;

    public override void OnInitialize()
    {
        backdrop = new UIPanel();
        backdrop.SetPadding(0);
        backdrop.Left.Set(750f, 0f);
        backdrop.Top.Set(250f, 0f);
        backdrop.Width.Set(400f, 0f);
        backdrop.Height.Set(438f, 0f);
        base.Append(backdrop);

        statwindow = new UIPanel();
        statwindow.SetPadding(0);
        statwindow.Left.Set(401, 0);
        statwindow.Top.Set(0, 0);
        statwindow.Height.Set(220, 0);
        statwindow.Width.Set(200, 0);
            backdrop.Append(statwindow);

            settingswindow = new UIPanel();
            settingswindow.SetPadding(0);
            settingswindow.Left.Set(401 + 750, 0);
            settingswindow.Top.Set(221 + 250, 0);
            settingswindow.Height.Set(36, 0);
            settingswindow.Width.Set(125, 0);
            base.Append(settingswindow);

            reset.Left.Set(10, 0);
            reset.Top.Set(408, 0);
            reset.Width.Set(24, 0);
            reset.Height.Set(24, 0);
            reset.OnClick += new MouseEvent(Reset);
            backdrop.Append(reset);


            UIImageButton selector = new UIImageButton(check);
        selector.Left.Set(10, 0);
        selector.Top.Set(10, 0);
        selector.Height.Set(46, 0);
        selector.Width.Set(46, 0);
        selector.OnClick += new MouseEvent(Select);
        backdrop.Append(selector);

        UIImageButton exit = new UIImageButton(ex);
        exit.Left.Set(60, 0);
        exit.Top.Set(10, 0);
        exit.Height.Set(46, 0);
        exit.Width.Set(46, 0);
        exit.OnClick += new MouseEvent(Exit);
        backdrop.Append(exit);

            
            vfxtoggle.Left.Set(10, 0);
            vfxtoggle.Top.Set(10, 0);
            vfxtoggle.Height.Set(16, 0);
            vfxtoggle.Width.Set(16, 0);
            vfxtoggle.OnClick += new MouseEvent(VFXtoggle);
            settingswindow.Append(vfxtoggle);

        passive1.Left.Set(150 - 19, 0);
        passive1.Top.Set(100, 0);
        passive1.Height.Set(38, 0);
        passive1.Width.Set(38, 0);
        passive1.OnClick += new MouseEvent(Strike);
        backdrop.Append(passive1);

        passive2.Left.Set(250 - 19, 0);
        passive2.Top.Set(100, 0);
        passive2.Height.Set(38, 0);
        passive2.Width.Set(38, 0);
        passive2.OnClick += new MouseEvent(Bolts);
        backdrop.Append(passive2);

        //UIImageButton slagbuster = new UIImageButton(slagbusterimage);
        active1.Left.Set(100 - 19, 0);
        active1.Top.Set(200, 0);
        active1.Height.Set(38, 0);
        active1.Width.Set(38, 0);
        active1.OnClick += new MouseEvent(Buster);
        backdrop.Append(active1);

        //UIImageButton active2 = new UIImageButton(active2image);
        active2.Left.Set(200 - 19, 0);
        active2.Top.Set(200, 0);
        active2.Height.Set(38, 0);
        active2.Width.Set(38, 0);
        active2.OnClick += new MouseEvent(Burst);
        backdrop.Append(active2);

        //UIImageButton active3 = new UIImageButton(active3image);
        active3.Left.Set(300 - 19, 0);
        active3.Top.Set(200, 0);
        active3.Height.Set(38, 0);
        active3.Width.Set(38, 0);
        active3.OnClick += new MouseEvent(Ward);
        backdrop.Append(active3);

        ultimate1.Left.Set(200 - 19, 0);
        ultimate1.Top.Set(300, 0);
        ultimate1.Height.Set(38, 0);
        ultimate1.Width.Set(38, 0);
        ultimate1.OnClick += new MouseEvent(Aura);
        backdrop.Append(ultimate1);

        //------------------------------------------------------------------------
        //Frames

        active1frame.Left.Set(-4, 0);
        active1frame.Top.Set(-4, 0);
        active1frame.Height.Set(46, 0);
        active1frame.Width.Set(46, 0);
        active1.Append(active1frame);

        active2frame.Left.Set(-4, 0);
        active2frame.Top.Set(-4, 0);
        active2frame.Height.Set(46, 0);
        active2frame.Width.Set(46, 0);
        active2.Append(active2frame);

        active3frame.Left.Set(-4, 0);
        active3frame.Top.Set(-4, 0);
        active3frame.Height.Set(46, 0);
        active3frame.Width.Set(46, 0);
        active3.Append(active3frame);

        passive1frame.Left.Set(-4, 0);
        passive1frame.Top.Set(-4, 0);
        passive1frame.Height.Set(46, 0);
        passive1frame.Width.Set(46, 0);
        passive1.Append(passive1frame);

        passive2frame.Left.Set(-4, 0);
        passive2frame.Top.Set(-4, 0);
        passive2frame.Height.Set(46, 0);
        passive2frame.Width.Set(46, 0);
        passive2.Append(passive2frame);

        ultimate1frame.Left.Set(-4, 0);
        ultimate1frame.Top.Set(-4, 0);
        ultimate1frame.Height.Set(46, 0);
        ultimate1frame.Width.Set(46, 0);
        ultimate1.Append(ultimate1frame);

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

            settingstext.Left.Set(36, 0);
            settingstext.Top.Set(10, 0);
            settingswindow.Append(settingstext);

            resettext.Left.Set(38,0);
            resettext.Top.Set(410, 0);
            backdrop.Append(resettext);


        }
        bool eatenorb = false;
    private void Reset(UIMouseEvent evt, UIElement ListeningElement)
        {
            Player player = Main.LocalPlayer;
            for (int z = 0; z <= 50; z++)
            {
 
                if (player.inventory[z].type == Typefinder.vorbtype && !eatenorb)
                {
                    player.inventory[z].stack--;
                    ab1 = 0;
                    ab2 = 0;
                    ab3 = 0;
                    if (swordinstance != null)
                    {
                        swordinstance.ab1 = 0;
                        swordinstance.ab2 = 0;
                        swordinstance.ab3 = 0;
                    }
                    if (spearinstance != null)
                    {
                        spearinstance.ab1 = 0;
                        spearinstance.ab2 = 0;
                        spearinstance.ab3 = 0;
                    }
                    if (guninstance != null)
                    {
                        guninstance.ab1 = 0;
                        guninstance.ab2 = 0;
                        guninstance.ab3 = 0;
                    }
                    eatenorb = true;
                }

            }
            eatenorb = false;
        }

    private void VFXtoggle(UIMouseEvent evt, UIElement ListeningElement)
        {
            if (swordinstance != null)
            {
                if (swordinstance.VFXstate == false)
                {
                    swordinstance.VFXstate = true;
                }
                else if (swordinstance.VFXstate == true)
                {
                    swordinstance.VFXstate = false;
                }
            }

            if (spearinstance != null)
            {
                if (spearinstance.VFXstate == false)
                {
                    spearinstance.VFXstate = true;
                }
                else if (spearinstance.VFXstate == true)
                {
                    spearinstance.VFXstate = false;
                }
            }

            if (guninstance != null)
            {
                if (guninstance.VFXstate == false)
                {
                    guninstance.VFXstate = true;
                }
                else if (guninstance.VFXstate == true)
                {
                    guninstance.VFXstate = false;
                }
            }
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
        if (ab1 == 0)
        {
            ab1 = passiveselect;
        }
        if (ab2 == 0)
        {
            ab2 = activeselect;
        }
        if (ab3 == 0)
        {
            ab3 = ultimateselect;
        }

            if (swordinstance != null)
            {
                swordinstance.ab1 = ab1;
                swordinstance.ab2 = ab2;
                swordinstance.ab3 = ab3;
            }

            if (spearinstance != null)
            {
                spearinstance.ab1 = ab1;
                spearinstance.ab2 = ab2;
                spearinstance.ab3 = ab3;
            }

            if (guninstance != null)
            {
                guninstance.ab1 = ab1;
                guninstance.ab2 = ab2;
                guninstance.ab3 = ab3;
            }

            passiveselect = 0;
        activeselect = 0;
        ultimateselect = 0;
    }

    private void Strike(UIMouseEvent evt, UIElement listeningElement)
    {
                    if (ab1 == 0 || ab1 == 1 && level >= 2)
                    {
                        Main.PlaySound(SoundID.MenuTick);
                        passiveselect = passive1ability;
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

                if (ab1 == 0 || ab1 == 2 && level >= 2)
                {
                    Main.PlaySound(SoundID.MenuTick);
                    passiveselect = passive2ability;
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

                if ((ab2 == 0 || ab2 == 1) && level >= 5)
                {
                    Main.PlaySound(SoundID.MenuTick);
                    activeselect = active1ability;
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
                if ((ab2 == 0 || ab2 == 2) && level >= 5)
                {
                    Main.PlaySound(SoundID.MenuTick);
                    activeselect = active2ability;
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

                if ((ab2 == 0 || ab2 == 3) && level >= 5)
                {
                    Main.PlaySound(SoundID.MenuTick);
                    activeselect = active3ability;
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

            if ((ab3 == 0 || ab3 == 1) && level >= 8)
            {
                Main.PlaySound(SoundID.MenuTick);
                activeselect = 0;
                passiveselect = 0;
                ultimateselect = ultimate1ability;
            }
            else
            {
                Main.PlaySound(SoundID.Unlock);
            }

        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (swordinstance != null)
            {

                settingstext.SetText("Fire Trail");

                backdrop.BackgroundColor = new Color(110, 40, 20, 170);
                backdrop.BorderColor = new Color(60, 20, 10, 230);

                statwindow.BackgroundColor = new Color(100, 50, 20, 140);
                statwindow.BorderColor = new Color(50, 30, 10, 230);

                settingswindow.BackgroundColor = new Color(100, 50, 20, 140);
                settingswindow.BorderColor = new Color(50, 30, 10, 230);


                if (swordinstance.VFXstate)
                {
                    vfxtoggle.SetImage(vfxtoggleon);
                }

                if (!swordinstance.VFXstate)
                {
                    vfxtoggle.SetImage(vfxtoggleoff);
                }

                if (passiveselect != 0)
                {
                    if (passiveselect == passive1ability) //passive text sets
                    {
                        line1.SetText("Burning Strike");
                        line2.SetText("Your melee strikes");
                        line3.SetText("inflict burning");
                        line4.SetText("for " + level * 1 + "-" + ((level * 1) + 1) + " seconds.");
                        line5.SetText("");
                        line6.SetText("");
                        line7.SetText("");

                        passive1frame.SetImage(frame2);
                        passive2frame.SetImage(frame);
                    }
                    else if (passiveselect == passive2ability)
                    {
                        line1.SetText("Firebolts");
                        line2.SetText("Your melee strikes");
                        line3.SetText("have a chance to");
                        line4.SetText("summon burning bolts");
                        line5.SetText("from the sky,");
                        line6.SetText("dealing " + (10 + level * 5) + " damage.");
                        line7.SetText("");


                        passive1frame.SetImage(frame);
                        passive2frame.SetImage(frame2);
                    }
                }
                else
                {
                    passive1frame.SetImage(frame);
                    passive2frame.SetImage(frame);
                }

                if (activeselect != 0)
                {
                    if (activeselect == active1ability) //active text sets
                    {
                        line1.SetText("Slag Buster");
                        line2.SetText("Shoot a projectile,");
                        line3.SetText("dealing " + (3 * (10 + level * 5)) + " damage");
                        line4.SetText("and exploding on");
                        line5.SetText("impact for 65 danage.");
                        line6.SetText("");
                        line7.SetText("3.5 second cooldown");

                        active1frame.SetImage(frame2);
                        active2frame.SetImage(frame);
                        active3frame.SetImage(frame);
                    }
                    else if (activeselect == active2ability)
                    {
                        line1.SetText("Slagburst");
                        line2.SetText("Creates a pillar of");
                        line3.SetText("flame, dealing " + ((10 + level * 5) / 3) * 6);
                        line4.SetText("damage per second");
                        line5.SetText("at your cursor.");
                        line6.SetText("");
                        line7.SetText("30 second cooldown");

                        active1frame.SetImage(frame);
                        active2frame.SetImage(frame2);
                        active3frame.SetImage(frame);
                    }
                    else if (activeselect == active3ability)
                    {
                        line1.SetText("Slag Ward");
                        line2.SetText("Creates 3 shields");
                        line3.SetText("around you, blocking");
                        line4.SetText("projectiles and");
                        line5.SetText("exploding on impact");
                        line6.SetText("for " + ((10 + level * 5) * 5) + " damage.");
                        line7.SetText("30 second cooldown");

                        active1frame.SetImage(frame);
                        active2frame.SetImage(frame);
                        active3frame.SetImage(frame2);
                    }
                }
                else
                {
                    active1frame.SetImage(frame);
                    active2frame.SetImage(frame);
                    active3frame.SetImage(frame);
                }

                if (ultimateselect != 0)
                {
                    if (ultimateselect == ultimate1ability) //ultimate text sets
                    {
                        line1.SetText("Aura of Cinders");
                        line2.SetText("Creates a burning");
                        line3.SetText("aura around you, ");
                        line4.SetText("dealing 10 damage");
                        line5.SetText("per second.");
                        line6.SetText("");
                        line7.SetText("");

                        ultimate1frame.SetImage(frame2);
                    }
                }
                else
                {
                    ultimate1frame.SetImage(frame);
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

                if (ab1 == 0 && swordinstance.level >= 2)
                {
                    passive1.SetImage(burningstrikeimage);
                    passive2.SetImage(fireboltsimage);
                }

                else if (ab1 == 0 && swordinstance.level < 2)
                {
                    passive1.SetImage(lockimage);
                    passive2.SetImage(lockimage);
                }

                else if (ab1 == passive1ability)
                {
                    passive1.SetImage(burningstrikeimage);
                    passive2.SetImage(lockimage);
                }

                else if (ab1 == passive2ability)
                {
                    passive1.SetImage(lockimage);
                    passive2.SetImage(fireboltsimage);
                }

                //------------------------------------------------------------------------

                if (ab2 == 0 && swordinstance.level >= 5)
                {
                    active1.SetImage(slagbusterimage);
                    active2.SetImage(slagburstimage);
                    active3.SetImage(slagwardimage);
                }
                else if (ab2 == 0 && swordinstance.level < 5)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(lockimage);
                }
                else if (ab2 == active1ability)
                {
                    active1.SetImage(slagbusterimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(lockimage);
                }

                else if (ab2 == active2ability)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(slagburstimage);
                    active3.SetImage(lockimage);
                }

                else if (ab2 == active3ability)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(slagwardimage);
                }

                //------------------------------------------------------------------------

                if (ab3 == 0 && swordinstance.level >= 8)
                {
                    ultimate1.SetImage(cinderauraimage);
                }

                else if (ab3 == 0 && swordinstance.level < 8)
                {
                    ultimate1.SetImage(lockimage);
                }

                else if (ab3 == 1)
                {
                    ultimate1.SetImage(cinderauraimage);
                }

                //------------------------------------------------------------------------
            }

            if (spearinstance != null)
            {
                backdrop.BackgroundColor = new Color(35, 60, 120, 190);
                backdrop.BorderColor = new Color(20, 10, 60, 230);

                statwindow.BackgroundColor = new Color(50, 30, 110, 170);
                statwindow.BorderColor = new Color(20, 5, 50, 230);

                settingswindow.BackgroundColor = new Color(50, 30, 110, 170);
                settingswindow.BorderColor = new Color(20, 5, 50, 230);

                settingstext.SetText("Scarf");

                if (spearinstance.VFXstate)
                {
                    vfxtoggle.SetImage(vfxtoggleon);
                }

                if (!spearinstance.VFXstate)
                {
                    vfxtoggle.SetImage(vfxtoggleoff);
                }

                if (passiveselect != 0)
                {
                    if (passiveselect == passive1ability) //passive text sets
                    {
                        line1.SetText("Burning Strike");
                        line2.SetText("Your melee strikes");
                        line3.SetText("inflict burning");
                        line4.SetText("for " + level * 1 + "-" + ((level * 1) + 1) + " seconds.");
                        line5.SetText("");
                        line6.SetText("");
                        line7.SetText("");

                        passive1frame.SetImage(frame2);
                        passive2frame.SetImage(frame);
                    }
                    else if (passiveselect == passive2ability)
                    {
                        line1.SetText("Firebolts");
                        line2.SetText("Your melee strikes");
                        line3.SetText("have a chance to");
                        line4.SetText("summon burning bolts");
                        line5.SetText("from the sky,");
                        line6.SetText("dealing " + (10 + level * 5) + " damage.");
                        line7.SetText("");


                        passive1frame.SetImage(frame);
                        passive2frame.SetImage(frame2);
                    }
                }
                else
                {
                    passive1frame.SetImage(frame);
                    passive2frame.SetImage(frame);
                }

                if (activeselect != 0)
                {
                    if (activeselect == active1ability) //active text sets
                    {
                        line1.SetText("One-thousand Thorns");
                        line2.SetText("Lash out with many");
                        line3.SetText("spears, dealing" );
                        line4.SetText(((40 + level * 8)) + " damage each");
                        line5.SetText("");
                        line6.SetText("");
                        line7.SetText("uses X energy /s");

                        active1frame.SetImage(frame2);
                        active2frame.SetImage(frame);
                        active3frame.SetImage(frame);
                    }
                    else if (activeselect == active2ability)
                    {
                        line1.SetText("Tyrant's Greatspear");
                        line2.SetText("Channel energy into");
                        line3.SetText("your spear, extending");
                        line4.SetText("it and spinning it");
                        line5.SetText("for " + (40 + level * 4) + " damage");
                        line6.SetText("per hit");
                        line7.SetText("uses 120 energy /s");

                        active1frame.SetImage(frame);
                        active2frame.SetImage(frame2);
                        active3frame.SetImage(frame);
                    }
                    else if (activeselect == active3ability)
                    {
                        line1.SetText("Slag Ward");
                        line2.SetText("Creates 3 shields");
                        line3.SetText("around you, blocking");
                        line4.SetText("projectiles and");
                        line5.SetText("exploding on impact");
                        line6.SetText("for " + ((10 + level * 5) * 5) + " damage.");
                        line7.SetText("30 second cooldown");

                        active1frame.SetImage(frame);
                        active2frame.SetImage(frame);
                        active3frame.SetImage(frame2);
                    }
                }
                else
                {
                    active1frame.SetImage(frame);
                    active2frame.SetImage(frame);
                    active3frame.SetImage(frame);
                }

                if (ultimateselect != 0)
                {
                    if (ultimateselect == ultimate1ability) //ultimate text sets
                    {
                        line1.SetText("Energy Vampire");
                        line2.SetText("Each regular jab");
                        line3.SetText("of your spear grants");
                        line4.SetText("30 energy per hit.");
                        line5.SetText("");
                        line6.SetText("");
                        line7.SetText("");

                        ultimate1frame.SetImage(frame2);
                    }
                }
                else
                {
                    ultimate1frame.SetImage(frame);
                }


                if (activeselect == 0 && passiveselect == 0 && ultimateselect == 0)
                {
                    line1.SetText("Lunarin Upgrades");
                    line2.SetText("Click an ability to");
                    line3.SetText("see details, click");
                    line4.SetText("the check button");
                    line5.SetText("to confirm your");
                    line6.SetText("selection.");
                    line7.SetText("");
                }

                //------------------------------------------------------------------------
                //------------------------------------------------------------------------

                if (ab1 == 0 && spearinstance.level >= 2)
                {
                    passive1.SetImage(burningstrikeimage);
                    passive2.SetImage(fireboltsimage);
                }

                else if (ab1 == 0 && spearinstance.level < 2)
                {
                    passive1.SetImage(lockimage);
                    passive2.SetImage(lockimage);
                }

                else if (ab1 == passive1ability)
                {
                    passive1.SetImage(burningstrikeimage);
                    passive2.SetImage(lockimage);
                }

                else if (ab1 == passive2ability)
                {
                    passive1.SetImage(lockimage);
                    passive2.SetImage(fireboltsimage);
                }

                //------------------------------------------------------------------------

                if (ab2 == 0 && spearinstance.level >= 5)
                {
                    active1.SetImage(thornsimage);
                    active2.SetImage(greatspearimage);
                    active3.SetImage(slagwardimage);
                }
                else if (ab2 == 0 && spearinstance.level < 5)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(lockimage);
                }
                else if (ab2 == active1ability)
                {
                    active1.SetImage(thornsimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(lockimage);
                }

                else if (ab2 == active2ability)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(greatspearimage);
                    active3.SetImage(lockimage);
                }

                else if (ab2 == active3ability)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(slagwardimage);
                }

                //------------------------------------------------------------------------

                if (ab3 == 0 && spearinstance.level >= 8)
                {
                    ultimate1.SetImage(energyvampireimage);
                }

                else if (ab3 == 0 && spearinstance.level < 8)
                {
                    ultimate1.SetImage(lockimage);
                }

                else if (ab3 == 1)
                {
                    ultimate1.SetImage(energyvampireimage);
                }

                //------------------------------------------------------------------------
            }

            if (guninstance != null)
            {
                backdrop.BackgroundColor = new Color(90, 85, 40, 190);
                backdrop.BorderColor = new Color(20, 20, 10, 230);

                statwindow.BackgroundColor = new Color(100, 95, 60, 170);
                statwindow.BorderColor = new Color(30, 30, 25, 230);

                settingswindow.BackgroundColor = new Color(100, 95, 60, 170);
                settingswindow.BorderColor = new Color(30, 30, 25, 230);

                settingstext.SetText("Circle");

                if (guninstance.VFXstate)
                {
                    vfxtoggle.SetImage(vfxtoggleon);
                }

                if (!guninstance.VFXstate)
                {
                    vfxtoggle.SetImage(vfxtoggleoff);
                }

                if (passiveselect != 0)
                {
                    if (passiveselect == passive1ability) //passive text sets
                    {
                        line1.SetText("Config: Shotgun");
                        line2.SetText("Your shots deal less");
                        line3.SetText("damage, but split");
                        line4.SetText("into 4 pellets");
                        line5.SetText("");
                        line6.SetText("");
                        line7.SetText("");

                        passive1frame.SetImage(frame2);
                        passive2frame.SetImage(frame);
                    }
                    else if (passiveselect == passive2ability)
                    {
                        line1.SetText("Config: Sniper");
                        line2.SetText("Your shots travel");
                        line3.SetText("faster and hitting");
                        line4.SetText("the same target again");
                        line5.SetText("deals " + (10 + guninstance.level) +" extra damage,");
                        line6.SetText("stacking up to 5 times");
                        line7.SetText("");


                        passive1frame.SetImage(frame);
                        passive2frame.SetImage(frame2);
                    }
                }
                else
                {
                    passive1frame.SetImage(frame);
                    passive2frame.SetImage(frame);
                }

                if (activeselect != 0)
                {
                    if (activeselect == active1ability) //active text sets
                    {
                        line1.SetText("Angel's Wings");
                        line2.SetText("Increase your flash");
                        line3.SetText("Range to 50 blocks");
                        line4.SetText("and heal " + (20 + level) + " HP");
                        line5.SetText("in an area on");
                        line6.SetText("arrival");
                        line7.SetText("4 second cooldown");

                        active1frame.SetImage(frame2);
                        active2frame.SetImage(frame);
                        active3frame.SetImage(frame);
                    }
                    else if (activeselect == active2ability)
                    {
                        line1.SetText("Piercing Bolt");
                        line2.SetText("Become invincible");
                        line3.SetText("and deal " + (250 + level * 10)+ " damage");
                        line4.SetText("to all enemies in");
                        line5.SetText("your way when you");
                        line6.SetText("flash");
                        line7.SetText("3 second cooldown");

                        active1frame.SetImage(frame);
                        active2frame.SetImage(frame2);
                        active3frame.SetImage(frame);
                    }
                    else if (activeselect == active3ability)
                    {
                        line1.SetText("Daredevil");
                        line2.SetText("Store up to 2 quick");
                        line3.SetText("recharging flashes");
                        line4.SetText("at a time, which");
                        line5.SetText("INSERT TEXT HERE");
                        line6.SetText("INSERT TEXT HERE");
                        line7.SetText("1.5 second cooldown each");

                        active1frame.SetImage(frame);
                        active2frame.SetImage(frame);
                        active3frame.SetImage(frame2);
                    }
                }
                else
                {
                    active1frame.SetImage(frame);
                    active2frame.SetImage(frame);
                    active3frame.SetImage(frame);
                }

                if (ultimateselect != 0)
                {
                    if (ultimateselect == ultimate1ability) //ultimate text sets
                    {
                        line1.SetText("Energy Vampire");
                        line2.SetText("Each regular jab");
                        line3.SetText("of your spear grants");
                        line4.SetText("30 energy per hit.");
                        line5.SetText("");
                        line6.SetText("");
                        line7.SetText("");

                        ultimate1frame.SetImage(frame2);
                    }
                }
                else
                {
                    ultimate1frame.SetImage(frame);
                }


                if (activeselect == 0 && passiveselect == 0 && ultimateselect == 0)
                {
                    line1.SetText("HX-17 Sigma Upgrades");
                    line2.SetText("Click an ability to");
                    line3.SetText("see details, click");
                    line4.SetText("the check button");
                    line5.SetText("to confirm your");
                    line6.SetText("selection.");
                    line7.SetText("");
                }

                //------------------------------------------------------------------------
                //------------------------------------------------------------------------

                if (ab1 == 0 && guninstance.level >= 2)
                {
                    passive1.SetImage(shotgunimage);
                    passive2.SetImage(sniperimage);
                }

                else if (ab1 == 0 && guninstance.level < 2)
                {
                    passive1.SetImage(lockimage);
                    passive2.SetImage(lockimage);
                }

                else if (ab1 == passive1ability)
                {
                    passive1.SetImage(shotgunimage);
                    passive2.SetImage(lockimage);
                }

                else if (ab1 == passive2ability)
                {
                    passive1.SetImage(lockimage);
                    passive2.SetImage(sniperimage);
                }

                //------------------------------------------------------------------------

                if (ab2 == 0 && guninstance.level >= 5)
                {
                    active1.SetImage(wingsimage);
                    active2.SetImage(boltimage);
                    active3.SetImage(dareimage);
                }
                else if (ab2 == 0 && guninstance.level < 5)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(lockimage);
                }
                else if (ab2 == active1ability)
                {
                    active1.SetImage(wingsimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(lockimage);
                }

                else if (ab2 == active2ability)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(boltimage);
                    active3.SetImage(lockimage);
                }

                else if (ab2 == active3ability)
                {
                    active1.SetImage(lockimage);
                    active2.SetImage(lockimage);
                    active3.SetImage(dareimage);
                }

                //------------------------------------------------------------------------

                if (ab3 == 0 && guninstance.level >= 8)
                {
                    ultimate1.SetImage(energyvampireimage);
                }

                else if (ab3 == 0 && guninstance.level < 8)
                {
                    ultimate1.SetImage(lockimage);
                }

                else if (ab3 == 1)
                {
                    ultimate1.SetImage(energyvampireimage);
                }

                //------------------------------------------------------------------------
            }
        }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Recalculate();
        base.Draw(spriteBatch);

    }

}



    



}
