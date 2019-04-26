using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.UI;
using ItemLevelTest.UI;


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

                Upgradeui.frame = ModLoader.GetTexture("ItemLEvelTest/UI/Frame2");
                Upgradeui.check = ModLoader.GetTexture("ItemLEvelTest/UI/check");
                Upgradeui.ex = ModLoader.GetTexture("ItemLEvelTest/UI/ex");
                Upgradeui.frame2 = ModLoader.GetTexture("ItemLEvelTest/UI/Frame3");
                Upgradeui.slagbusterimage = ModLoader.GetTexture("ItemLevelTest/UI/Slagbuster");
                Upgradeui.slagburstimage = ModLoader.GetTexture("ItemLevelTest/UI/Slagburst");
                Upgradeui.slagwardimage = ModLoader.GetTexture("ItemLevelTest/UI/Slagward");
                Upgradeui.burningstrikeimage = ModLoader.GetTexture("ItemLevelTest/UI/burningstrike");
                Upgradeui.fireboltsimage = ModLoader.GetTexture("ItemLevelTest/UI/firebolts");
                Upgradeui.cinderauraimage = ModLoader.GetTexture("ItemLevelTest/UI/cinderaura");
                Upgradeui.lockimage = ModLoader.GetTexture("ItemLevelTest/UI/Blank");

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

                customResources = null;
                customResourcesupgrade = null;
                cdui = null;
                upui = null;

            }         
        }

    }
}
