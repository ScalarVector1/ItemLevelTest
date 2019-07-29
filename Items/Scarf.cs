using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Items
{
  
  class Maxer : ModItem
  {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("DEV Item Maxer");
                Tooltip.SetDefault("Maxes all items, testing only");
             

            }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;
        }

        public override bool CanUseItem(Player player)
        {
            for(int k = 0; k <= 54; k++)
            {
                if(player.inventory[k].type == mod.ItemType("Koranithus"))
                {
                    Koranithus sword = player.inventory[k].modItem as Koranithus;
                    sword.level = 10;
                    sword.exp = 0;
                }

                if (player.inventory[k].type == mod.ItemType("Testbow"))
                {
                    Testbow bow = player.inventory[k].modItem as Testbow;
                    bow.level = 10;
                    bow.exp = 0;
                }

                if (player.inventory[k].type == mod.ItemType("Testspear"))
                {
                    Testspear spear = player.inventory[k].modItem as Testspear;
                    spear.level = 10;
                    spear.exp = 0;
                }

                if (player.inventory[k].type == mod.ItemType("Testgun"))
                {
                    Testgun gun = player.inventory[k].modItem as Testgun;
                    gun.level = 10;
                    gun.exp = 0;
                }
            }
                return true;
        }
        
  }
}
