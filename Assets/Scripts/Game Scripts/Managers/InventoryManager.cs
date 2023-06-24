using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    //These are the two objects that hold all the inventory UI and swapping UI respectively, used to quickly show or hide those UI elements
    private GameObject inventoryUIElements;
    public GameObject swappingUIElements;

    //This is the text that displays the object being swapped
    public Text swappingObject;

    //These are the buttons that you use during swapping to choose what item to take out of your inventory
    public Button[] inventoryButtons = new Button[3];

    //This is the int that stores what item from the container is being swapped out and where the object you take out of your inventory should go
    private int itemWaitingToSwap;

    //This is the bool that stores if we are currently swapping or not
    private bool areSwapping;

    //These are the three texts that display what items are in your inventory
    private Text[] inventoryTexts = new Text[3];


    //This is the public array of the players inventory that other objects can acess
    public string[] inventory = new string[3];

    //The location manager for the scene so the Inventory Manager can communicate with it
    private LocationManager locationManager;
    


    void Awake ()
    {
        //First grab the location manager, then grab all the UI elements like text... etc
        locationManager = GameObject.Find("LocationManager").GetComponent<LocationManager>();
        GetUIElements();

        //Clear the inventory to be empty
        int i;
        for (i = 0; i < 3; i++)
        {
            inventory[i] = "Empty";
        }

        //Just in case, we close the swapping panel on start
        HideSwappingUI();
    }

    private void GetUIElements()
    {
        inventoryUIElements = GameObject.Find("Inventory");
        swappingUIElements = GameObject.Find("Swapping");
        swappingObject = swappingUIElements.transform.Find("SwappingObject").GetComponent<Text>();

        int i;
        for (i = 0; i < 3; i++)
        {
            inventoryButtons[i] = GameObject.Find("InventoryButton (" + i + ")").GetComponent<Button>();
            inventoryTexts[i] = GameObject.Find("InventoryButton (" + i + ")").GetComponent<Text>();
        }
    }

    public void PickUpItem (int item)
    {
        bool complete = false;
        int i;
        //Does a loop to see if there is an empty slot in the inventory, or if the item you selected is "Empty" it realizes you're trying to switch something so it automatically goes to swapping
        for (i = 0; i < 3; i++)
        {
            if (inventory[i] == "Empty" && complete == false && locationManager.currentContainer.items[item] != "Empty")
            {
                //We get here if we find an open slot in the inventory and are swapping in an object that isn't called "Empty", so first we put that item in the found empty slot and replace that container slot
                //with "Empty"
                inventory[i] = locationManager.currentContainer.items[item];
                locationManager.currentContainer.items[item] = "Empty";

                //It needs to Update the UI so that the screen displays the inventory change
                locationManager.UpdateUI();
                complete = true;
            }
        }
        //If it didn't manage to find an empty slot, or if you tried to pick up an "Empty" object, it enters the swapping mode where you choose an item to swap out
        if (complete)
        {
            return;
        }
        else
        {
            itemWaitingToSwap = item;
            ShowSwappingUI();
        }
    }

    private void ShowSwappingUI()
    {
        //First we set that we are in swapping mode
        areSwapping = true;

        //Then it opens the specific panel and text for an inventory swap to happen
        swappingUIElements.SetActive(true);

        //We want the player to be able to click on one of the inventory items to choose it as the one to switch out
        EnableInventoryButtons();

        //Sets the swapping object text to the item we are waiting to swap so you know what you are swapping for
        swappingObject.text = locationManager.currentContainer.items[itemWaitingToSwap];

        //Now we just wait till the player clicks one of the buttons, from there we head to Inventory Button Swap with the input as the button/inventory item that was clicked
    }

    public void EnableInventoryButtons()
    {
        //Turns inventory text objects into buttons to click on, so now if one of them is clicked we head over to InventoryButtonInput with an input of what item was clicked
        int i;
        for (i = 0; i < 3; i++)
        {
            inventoryButtons[i].enabled = true;
        }
    }

    public void DisableInventoryButtons()
    {
        //Turns inventory text objects back into only text objects, no longer buttons
        int i;
        for (i = 0; i < 3; i++)
        {
            inventoryButtons[i].enabled = false;
        }
    }

    public void InventoryButtonInput (int inventoryItem)
    {
        //There's two ways to get button input: you either are swapping objects or are using objects. Let's find out, then move on from there
        if (areSwapping)
        {
            SwapItem(inventoryItem);
        }
        else
        {
            //First check if it's not an empty slot
            
           if (inventory[inventoryItem] != "Empty")
            {
                //Try to use that item on the location manager
                if (locationManager.currentInteraction.succesfulItems.Contains(inventory[inventoryItem]))
                {
                    //Success! The item clicked is one of the allowed ones! Let's tell the location manager that
                    locationManager.SuccessfulInteraction();
                }
                else
                {
                    //Well, that didn't work! The item clicked has no effect! Let's tell the location manager that
                    locationManager.UnsuccessfulInteraction();
                }
            }
        }
    }

    private void SwapItem (int inventoryItemToSwapOut)
    {
        //First we save the item we just selected as the item we are going to take out of the inventory and replace in the container
        string swapingItem = inventory[inventoryItemToSwapOut];
        //Then we replace that slot in the inventory with the item we had tried to take out of the container with a full inventory
        inventory[inventoryItemToSwapOut] = locationManager.currentContainer.items[itemWaitingToSwap];
        //Then we replace that item in the container with the item we had in our inventory to switch it out with
        locationManager.currentContainer.items[itemWaitingToSwap] = swapingItem;

        //Then it reverts the UI back to how it was before
        HideSwappingUI();

        //Then we Update the UI so we can see the changes
        locationManager.UpdateUI();
    }

    private void HideSwappingUI()
    {
        //First we set that we are no longer swapping
        areSwapping = false;

        //Hides all the swapping panel and text
        swappingUIElements.SetActive(false);

        //We no longer want the player randomly clicking on the inventory items as buttons, so we disable them as buttons
        DisableInventoryButtons();
    }




    
    public void ShowInventoryUI()
    {
        //It also updates the Inventory UI: It sets the inventory texts to display what's in your inventory
        int i;
        for (i=0; i < 3; i++)
        {
            inventoryTexts[i].text = locationManager.AddSpaces(inventory[i]);

        }

        inventoryUIElements.SetActive(true);
    }

    public void HideInventoryUI()
    {
        DisableInventoryButtons();
        inventoryUIElements.SetActive(false);
    }
}
