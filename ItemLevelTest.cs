using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.UI;
using ItemLevelTest.UI;
using ItemLevelTest.Items;

namespace ItemLevelTest
{
	class ItemLevelTest : Mod
	{
        public CDUI cdui;
        public Upgradeui upui;
        private UserInterface customResources;
        private UserInterface customResourcesupgrade;
        

        public ItemLevelTest()
		{

		}

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
      
                int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("[PH]MODNAME: Cooldown",
                delegate
                {
                    if (CDUI.visible)
                    {
                        customResources.Update(Main._drawInterfaceGameTime);
                        cdui.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));

                layers.Insert(MouseTextIndex + 1, new LegacyGameInterfaceLayer("[PH]MODNAME: Upgrade",
                delegate
                {
                    if (Upgradeui.visible)
                    {
                        customResourcesupgrade.Update(Main._drawInterfaceGameTime);
                        upui.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));


            }


        }

        public override void Load()
        {
            if (!Main.dedServ)
            {

                    Upgradeui.frame = ModContent.GetTexture("ItemLEvelTest/UI/Frame2");
                    Upgradeui.check = ModContent.GetTexture("ItemLEvelTest/UI/check");
                    Upgradeui.ex = ModContent.GetTexture("ItemLEvelTest/UI/ex");
                    Upgradeui.frame2 = ModContent.GetTexture("ItemLEvelTest/UI/Frame3");
                    Upgradeui.slagbusterimage = ModContent.GetTexture("ItemLevelTest/UI/Slagbuster");
                    Upgradeui.slagburstimage = ModContent.GetTexture("ItemLevelTest/UI/Slagburst");
                    Upgradeui.slagwardimage = ModContent.GetTexture("ItemLevelTest/UI/Slagward");
                    Upgradeui.burningstrikeimage = ModContent.GetTexture("ItemLevelTest/UI/burningstrike");
                    Upgradeui.fireboltsimage = ModContent.GetTexture("ItemLevelTest/UI/firebolts");
                    Upgradeui.cinderauraimage = ModContent.GetTexture("ItemLevelTest/UI/cinderaura");
                    Upgradeui.lockimage = ModContent.GetTexture("ItemLevelTest/UI/Blank");
                    Upgradeui.vfxtoggleon = ModContent.GetTexture("ItemLevelTest/UI/vfxtoggleon");
                    Upgradeui.thornsimage = ModContent.GetTexture("ItemLevelTest/UI/Spears");
                    Upgradeui.greatspearimage = ModContent.GetTexture("ItemLevelTest/UI/Whirl");
                    Upgradeui.energyvampireimage = ModContent.GetTexture("ItemLevelTest/UI/Energy");
                    Upgradeui.wingsimage = ModContent.GetTexture("ItemLevelTest/UI/Wings");
                    Upgradeui.boltimage = ModContent.GetTexture("ItemLevelTest/UI/Bolt");
                    Upgradeui.dareimage = ModContent.GetTexture("ItemLevelTest/UI/Daredevil");
                    Upgradeui.sniperimage = ModContent.GetTexture("ItemLevelTest/UI/Sniper");
                    Upgradeui.shotgunimage = ModContent.GetTexture("ItemLevelTest/UI/Shotgun");
                    Upgradeui.vfxtoggleoff = ModContent.GetTexture("ItemLevelTest/UI/vfxtoggleoff");
                    Upgradeui.vorb = ModContent.GetTexture("ItemLevelTest/UI/Vorb");


        customResources = new UserInterface();
                customResourcesupgrade = new UserInterface();
                cdui = new CDUI();
                upui = new Upgradeui();

                CDUI.visible = true;

                Upgradeui.visible = false;
                customResources.SetState(cdui);
                customResourcesupgrade.SetState(upui);

            }
        }


        public override void Unload()
        {
            if (!Main.dedServ)
            {
                    Upgradeui.frame = null;
                    Upgradeui.check = null;
                    Upgradeui.ex = null;
                    Upgradeui.frame2 = null;
                    Upgradeui.slagbusterimage = null;
                    Upgradeui.slagburstimage = null;
                    Upgradeui.slagwardimage = null;
                    Upgradeui.burningstrikeimage = null;
                    Upgradeui.fireboltsimage = null;
                    Upgradeui.cinderauraimage = null;
                    Upgradeui.lockimage = null;
                    Upgradeui.thornsimage = null;
                    Upgradeui.greatspearimage = null;
                    Upgradeui.energyvampireimage = null;
                    Upgradeui.wingsimage = null;
                    Upgradeui.boltimage = null;
                    Upgradeui.dareimage = null;
                    Upgradeui.sniperimage = null;
                    Upgradeui.shotgunimage = null;
                    Upgradeui.vfxtoggleoff = null;
                    Upgradeui.vfxtoggleon = null;
                    Upgradeui.vorb = null;

                customResources = null;
                customResourcesupgrade = null;
                cdui = null;
                upui = null;

                CDUI.swordinstance = null;
                CDUI.bowinstance = null;
                CDUI.spearinstance = null;
                CDUI.guninstance = null;
                Upgradeui.swordinstance = null;
                Upgradeui.bowinstance = null;
                Upgradeui.guninstance = null;
                Upgradeui.spearinstance = null;

                Flashhandler.instance = null;
                Terraria.DataStructures.TileEntity.ByID = null;
                Terraria.DataStructures.TileEntity.ByPosition = null;



            }         
        }

    }
}
