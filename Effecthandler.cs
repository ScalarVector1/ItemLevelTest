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
    public bool spearVFX = false;
    public static bool spearSpawned = false;
    public bool spearfailed = false;
    public bool spearVFXforce = false;
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
            if ((thisinstance.type == mod.ItemType("Koranithus") && !VFXactive && koranithus.VFXstate && !spearVFX) || (swordVFXforce && !VFXactive && !spearVFX))
            {
                swordVFX = true;
                VFXactive = true;
                swordVFXforce = false;
            }
        }

        for (int z = 2; z <= 16 + player.extraAccessorySlots * 2; z++)
        {
            if (player.armor[z].type == mod.ItemType("SwordAccessory"))
            {
                swordVFXforce = true;
                VFXactive = true;
            }
        }

        bowVFX = false;

        for (int z = 0; z <= 50; z++)
        {
            if (player.inventory[z].type == mod.ItemType("Testbow") && !VFXactive && !spearVFX)
            {
                bowVFX = true;
                VFXactive = true;
            }
        }


        spearfailed = true;

        for (int z = 0; z <= 50; z++)
        {
            Item thisinstance = player.inventory[z];
            Testspear spear = thisinstance.modItem as Testspear;
            if ((thisinstance.type == mod.ItemType("Testspear") && !VFXactive && spear.VFXstate) || (spearVFXforce && !VFXactive))
            {
                spearVFX = true;
                spearVFXforce = false;
                spearfailed = false;
            }
        }

        if (!spearfailed)
        {
            
        }
        else 
        {
            spearVFX = false;
            spearfailed = false;
            spearSpawned = false;
        }



        if (swordVFX)
        {
            if (Math.Abs(player.velocity.X) > 1 || Math.Abs(player.velocity.Y) > 1)
            {
                Dust.NewDust(new Vector2(player.position.X - 2, player.position.Y + player.height - 4), player.width + 4, 4, mod.DustType("Sworddust"));
            }
        }

        if(spearVFX && !spearSpawned)
        {
            Dust root = Dust.NewDustPerfect(player.MountedCenter, mod.DustType("Scarftestroot"));
            root.customData = player;
            for (int u = 0; u <= 29; u++)
            {
                Dust segment = Dust.NewDustPerfect(new Vector2(player.MountedCenter.X - u * 3, player.MountedCenter.Y), mod.DustType("Scarftest"));
                segment.customData = u;
            }
            spearSpawned = true;

        }

    }
}