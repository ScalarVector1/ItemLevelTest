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

            }

            
        }

    }
}
