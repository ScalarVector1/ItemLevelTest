using ItemLevelTest.UI;
using Terraria;
using Terraria.ModLoader;

public class UImanager : ModPlayer
{
    public override void PostUpdate()
    {
        //this handles closing the upgrade UI with escape
        if (Upgradeui.visible && player.controlInv)
        {
            Upgradeui.visible = false;
        }


        //this section handles the cooldown UI
        Item holding = player.HeldItem;
        if ((player.HeldItem.type == mod.ItemType("Koranithus") 
            || player.HeldItem.type == mod.ItemType("Testbow") 
            || player.HeldItem.type == mod.ItemType("Testspear")
            || player.HeldItem.type == mod.ItemType("Testgun")
            || player.HeldItem.type == mod.ItemType("Teststaff")
            ) && !Main.playerInventory)//when holding a legendary item
        {
            if (player.HeldItem != holding)//failsafe if holding nothing
            {
                CDUI.visible = false;
            }
            CDUI.visible = true; //handles the visibility of the UI
        }
        else
        {
            CDUI.visible = false;
        }
       

    }
}