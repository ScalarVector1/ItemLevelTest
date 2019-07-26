using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System.Linq;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

class Dropworld : ModWorld
{
    public static bool swordoredrop = false;
    public bool swordorefailed = false;
    public static bool swordspiritdrop = false;
    public static bool swordspiritfailed = false;
    public static bool swordfragdrop = false;

    public static bool gunoredrop = false;
    public bool gunorefailed = false;
    public static bool gunspiritdrop = false;
    public static bool gunspiritfailed = false;
    public static bool gunfragdrop = false;
    public override void PreUpdate()
    {

        Player player = Main.LocalPlayer;
        if (((NPC.AnyNPCs(NPCID.BrainofCthulhu)) || (NPC.AnyNPCs(NPCID.EaterofWorldsHead)) || (NPC.AnyNPCs(NPCID.EaterofWorldsTail)) || (NPC.AnyNPCs(NPCID.EaterofWorldsBody))) && swordorefailed == false)
        {
            swordoredrop = true;
            if (!player.armor.Where((x, i) => i > 2 && i < 8 + player.extraAccessorySlots).All(x => x.IsAir))
            {
                swordoredrop = false;
                swordorefailed = true;
            }
        }
        if (!((NPC.AnyNPCs(NPCID.BrainofCthulhu)) || (NPC.AnyNPCs(NPCID.EaterofWorldsHead)) || (NPC.AnyNPCs(NPCID.EaterofWorldsTail)) || (NPC.AnyNPCs(NPCID.EaterofWorldsBody))) && swordorefailed == true)
        {
            swordorefailed = false;
        }

        if ((NPC.AnyNPCs(NPCID.SkeletronHead)) && swordspiritfailed == false)
        {
            swordspiritdrop = true;
            if (!player.armor.Where((x, i) => i <= 2).All(X => X.IsAir))
            {
                swordspiritdrop = false;
                swordspiritfailed = true;
            }
        }
        if (!(NPC.AnyNPCs(NPCID.SkeletronHead)) && swordspiritfailed == true)
        {
            swordspiritfailed = false;
        }

        swordfragdrop = false;
        gunfragdrop = false;

        for (int z = 0; z <= 50; z++)
        {
            if (player.inventory[z].type == mod.ItemType("Swordspirit1"))
            {
                swordfragdrop = true;
            }

            if (player.inventory[z].type == mod.ItemType("Gun2sub"))
            {
                gunfragdrop = true;
            }
        }

        



    }
}

class Drop : GlobalNPC
{
    public override void NPCLoot(NPC npc)
    {
        int playerIndex = npc.lastInteraction;
        if (!Main.player[playerIndex].active || Main.player[playerIndex].dead)
        {
            playerIndex = npc.FindClosestPlayer(); // Since lastInteraction might be an invalid player fall back to closest player.
        }
        Player player = Main.player[playerIndex];


        if ((npc.type == NPCID.BrainofCthulhu) || ((npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.EaterofWorldsBody) && npc.boss && Dropworld.swordoredrop))

        {
            Item.NewItem((int)player.position.X, (int)player.position.Y - 100, player.width, player.height, mod.ItemType("Swordore1"));
        }

        if (npc.type == NPCID.SkeletronHead && Dropworld.swordspiritdrop)
        {
            Item.NewItem(npc.getRect(), mod.ItemType("Swordspirit1"));
        }

        if (Dropworld.swordfragdrop)
        {
            Item.NewItem(npc.getRect(), mod.ItemType("Swordbit1"));

            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Swordbit1"));
            }
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Swordbit1"));
            }
        }

        if (Dropworld.gunfragdrop)
        {
            if (npc.boss == true)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Gun2sub2"), Main.rand.Next(40,100));
            }            
        }
    }
}