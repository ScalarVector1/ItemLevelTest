using ItemLevelTest.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ItemLevelTest.Buffs
{
    public class Slagmelt : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }



        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<Buffhandler>(mod).slagmelt = true;
        }
    }

    public class Slagward : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Slag ward");
            Description.SetDefault("Increase defense by 20");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }



        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<Buffhandlerplayer>(mod).ward = true;
        }
    }
}
