using ItemLevelTest.Items;
using ItemLevelTest.UI;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

public class Effecthandler : ModPlayer
{
    bool VFXactive = false;
    public bool swordVFX = false;
    public bool swordVFXforce = false;
    public bool bowVFX = false;
    public override void PostUpdate()
    {
        if (swordVFX || bowVFX)
        {
            VFXactive = true;
        }
        else
        {
            VFXactive = false;
        }

        swordVFX = false;

        for (int z = 0; z <= 50; z++)
        {
            Item thisinstance = player.inventory[z];
            Koranithus koranithus = thisinstance.modItem as Koranithus;
            if ((thisinstance.type == mod.ItemType("Koranithus") && !VFXactive && koranithus.VFXstate) || (swordVFXforce && !VFXactive))
            {
                swordVFX = true;
                swordVFXforce = false;
            }
        }

        for (int z = 2; z <= 16 + player.extraAccessorySlots * 2; z++)
        {
            if (player.armor[z].type == mod.ItemType("SwordAccessory"))
            {
                swordVFXforce = true; 
            }
        }

        bowVFX = false;
        for (int z = 0; z <= 50; z++)
        {
            if (player.inventory[z].type == mod.ItemType("Testbow") && !VFXactive)
            {
                bowVFX = true;
            }
        }

        if (swordVFX)
        {
            if (Math.Abs(player.velocity.X) > 1 || Math.Abs(player.velocity.Y) > 1)
            {
                Dust.NewDust(new Vector2(player.position.X - 2, player.position.Y + player.height - 4), player.width + 4, 4, mod.DustType("Sworddust"));
            }
        }

    }
}