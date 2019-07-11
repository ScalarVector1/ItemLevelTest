using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ItemLevelTest.Items
{
    class SwordAccessory:ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Trail");
            Tooltip.SetDefault("Gain the fire trail effect of the sword Koranithus");
        }

        public override void SetDefaults()
        {
            item.alpha = 0;
            item.width = 32;
            item.height = 26;
            item.rare = -11;
            item.accessory = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Effecthandler>(mod).swordVFXforce = true;
        }

    }
}
