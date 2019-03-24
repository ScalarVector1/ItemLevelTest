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
        public UserInterface customResources;
        public ItemLevelTest()
		{

		}

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
      
                int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
                if (MouseTextIndex != -1)
                    layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer("[PH]MODNAME: Cooldown",
                    delegate
                        {
                            if (CDUI.visible == true)
                            {                                
                                customResources.Update(Main._drawInterfaceGameTime);
                                cdui.Draw(Main.spriteBatch);
                            }
                            return true;
                        }));

             
        }

        public override void Load()
        {

            if (!Main.dedServ)
            {
                customResources = new UserInterface();
                cdui = new CDUI();
                CDUI.visible = true;
                customResources.SetState(cdui);

            }
        }

    }
}
