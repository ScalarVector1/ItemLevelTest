using ItemLevelTest.UI;
using Terraria;
using Terraria.ModLoader;

public class UImanager : ModPlayer
{
    public override void PostUpdate()
    {
        //this section handles the cooldown UI
        Item holding = player.HeldItem;
        if (player.HeldItem.type == mod.ItemType("Koranithus") && !Main.playerInventory)//when holding the sword
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
        //bow UI
        if (player.HeldItem.type == mod.ItemType("Testbow") && !Main.playerInventory)//when holding the bow
        {
            if (player.HeldItem != holding)//failsafe if holding nothing
            {
                CHUI.visible = false;
            }
            CHUI.visible = true; //handles the visibility of the UI
        }
        else
        {
            CHUI.visible = false;
        }


    }
}