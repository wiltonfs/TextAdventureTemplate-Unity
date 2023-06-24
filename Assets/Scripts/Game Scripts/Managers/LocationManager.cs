using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    //The int that records the current time. 0 is past, 1 is modern
    public static int time;

    //Just the public string to show what the name of the current location is and what the last location visited is, with an int to show if it's a node, container, or interaction
    public static string location, lastLocation;
    public static int locationType;

    //The Inventory Manager so this Location Manager can talk to it
    private InventoryManager inventoryManager;

    //Node UI Elements
    private GameObject nodeUIElements;
    private Text nodeName, nodeDescription;
    private Text[] movementButtons = new Text[6];

    //Container UI Elements
    private GameObject containerUIElements;
    private Image containerPanel;
    private Text containerName;
    private Text[] containerButtons = new Text[6];

    //Interaction UI Elements
    private GameObject interactionUIElements;
    private Image interactionPanel;
    private Text interactionName;
    private Text interactionDescription;

    //Still part of the interaction UI, but the UI for displaying correct try and incorrect try messages
    private GameObject correctUIElements, incorrectUIElements;
    private Text correctText, incorrectText;

    //TODO put interaction UI here

    //Depending on what the current location is, node, container, or interaction, it will have one of these three specific types of script on it. We grab the one specific to it and set it to its respective type:
    //For example, if the current location is a container, we store that container's specific Container script as the currentContainer and whenever we need to get information about the current location we
    //check what the current location type is and since it is a container type we go and grab the information we need from the currentContainer script
    public Node currentNode;
    public Container currentContainer;
    public Interaction currentInteraction;

    // Start is called before the first frame update
    void Start()
    {
        //First we grab out inventory manager
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        //Then, the location manager finds all the text and buttons it needs to display the 3 screens for a Node, Container, or Interaction type of location
        GetUIElements();

        //This here just sets the default initial location, and time to modern
        time = 1;
        ChangeLocation("Field");
        
    }

    private void GetUIElements()
    {
        //This gets the text and buttons for the Node UI, to display the Node locations
        nodeUIElements = GameObject.Find("Node");
        nodeName = GameObject.Find("NodeName").GetComponent<Text>();
        nodeDescription = GameObject.Find("NodeDescription").GetComponent<Text>();

        int i;
        for (i = 0; i < 6; i++)
        {
            movementButtons[i] = nodeUIElements.transform.Find("NodeButton (" + i + ")").GetComponent<Text>();
        }

        //This gets the text, buttons, and panel for the Container UI, to display any Container locations
        containerUIElements = GameObject.Find("Container");
        containerName = containerUIElements.transform.Find("ContainerName").GetComponent<Text>();
        containerPanel = containerUIElements.transform.Find("ContainerPanel").GetComponent<Image>();

        for (i = 0; i < 6; i++)
        {
            containerButtons[i] = containerUIElements.transform.Find("ContainerButton (" + i + ")").GetComponent<Text>();
        }

        //This gets the text and panel for the Interaction UI, to display any Interaction locations
        interactionUIElements = GameObject.Find("Interaction");
        interactionName = GameObject.Find("InteractionName").GetComponent<Text>();
        interactionDescription = GameObject.Find("InteractionDescription").GetComponent<Text>();

        //We also get the UI elements that display a correct or incorrect try of an interaction here:
        correctUIElements = GameObject.Find("Correct");
        incorrectUIElements = GameObject.Find("Incorrect");
        correctText = GameObject.Find("CorrectDescription").GetComponent<Text>();
        incorrectText = GameObject.Find("IncorrectDescription").GetComponent<Text>();


    }

    private void ChangeLocation(string newLocation)
    {
        //First, we set the lastLocation variable to the current location before we move on ONLY IF IT IS A NODE, so if at this new location the player hits "back" we know what to send the player back to
        if (locationType == 0)
        {
            lastLocation = location;
        }

        //This method sets the location to the new input, and finds if its a Node, Container, or Interaction, and gets the appropriate script from it
        location = newLocation;

        //It tries to grab all three type of scripts and then goes and checks to find which one was found succesfully, that's how we know what type of location it is
        currentNode = GameObject.Find(newLocation).GetComponent<Node>();
        currentContainer = GameObject.Find(newLocation).GetComponent<Container>();
        currentInteraction = GameObject.Find(newLocation).GetComponent<Interaction>();

        if (currentNode != null)
        {
            locationType = 0;
        }
        else if (currentContainer != null)
        {
            locationType = 1;
        }
        else if (currentInteraction != null)
        {
            locationType = 2;
        }
        else
        {
            Debug.Log("The location you are trying to move to does not have any of the 3 default location scripts attached to it");
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        //These screens should never be immediately visible, so we hide them every time we switch locations
        correctUIElements.SetActive(false);
        incorrectUIElements.SetActive(false);


        switch (locationType)
        {
            //This is a switch construction, it takes the current type of location and runs one of three paths depending on if it's a node, container, or interaction: 0, 1, or 2
            case 0:
                //First it closes the container or interaction panels as well as the inventory
                containerUIElements.SetActive(false);
                inventoryManager.HideInventoryUI();
                interactionUIElements.SetActive(false);

                //Updates all the UI elements for the Node UI stuff with the current Node's information
                
                if (time == 1)
                {
                    nodeName.text = AddSpaces(currentNode.modernName);
                    nodeDescription.text = currentNode.modernDescription;
                }
                else
                {
                    nodeName.text = AddSpaces(currentNode.historicName);
                    nodeDescription.text = currentNode.historicDescription;
                }

                int i;
                for (i = 0; i < 6; i++)
                {
                    if (time == 1)
                    {
                        if (currentNode.modernMovements[i] != "")
                        {
                            movementButtons[i].gameObject.SetActive(true);

                            if (GameObject.Find(currentNode.modernMovements[i]).GetComponent<Node>() != null)
                            {
                                movementButtons[i].text = AddSpaces(GameObject.Find(currentNode.modernMovements[i]).GetComponent<Node>().modernName);
                            }
                            else
                            {
                                movementButtons[i].text = AddSpaces(currentNode.modernMovements[i]);
                            }
                        }
                        else
                        {
                            movementButtons[i].gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (currentNode.historicMovements[i] != "")
                        { 
                            movementButtons[i].gameObject.SetActive(true);

                            if (GameObject.Find(currentNode.historicMovements[i]).GetComponent<Node>() != null)
                            {
                                movementButtons[i].text = AddSpaces(GameObject.Find(currentNode.historicMovements[i]).GetComponent<Node>().historicName);
                            }
                            else
                            {
                                movementButtons[i].text = AddSpaces(currentNode.historicMovements[i]);
                            }
                        }
                        else
                        {
                            movementButtons[i].gameObject.SetActive(false);
                        }
                    }
                    
                }
                break;

            case 1:
                //First it opens the container panel and inventory panel, as well as closing any interaction panel
                containerUIElements.SetActive(true);
                interactionUIElements.SetActive(false);
                inventoryManager.ShowInventoryUI();

                //Then it sets the container texts to their appropriate values and items from the current Container's information
                containerName.text = AddSpaces(currentContainer.name.ToString());
                containerPanel.CrossFadeAlpha(1f, 0f, false);
                for (i = 0; i < 6; i++)
                {
                    if (currentContainer.items[i] != "")
                    {
                        containerButtons[i].text = AddSpaces(currentContainer.items[i]);
                        containerButtons[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        containerButtons[i].gameObject.SetActive(false);
                    }
                }
                break;

            case 2:
                //Opens the Interaction panel and inventory panel, as well as closing any container panel
                interactionUIElements.SetActive(true);
                containerUIElements.SetActive(false);
                inventoryManager.ShowInventoryUI();

                //Then it sets the interaction texts to their appropriate values from the current Interaction's information
                interactionName.text = AddSpaces(currentInteraction.name.ToString());
                interactionDescription.text = currentInteraction.description;

                //It also enables the inventory list as buttons so you can try inventory items
                inventoryManager.EnableInventoryButtons();

                //If it's already unlocked, however, it just skips straight to a correct interaction
                if (currentInteraction.setLock)
                {
                    SuccessfulInteraction();
                }

                
                break;

            default:
                Debug.Log("Updating the UI, the location Type was not one of the 3 expected, 0-2, which are Node, Container, and interaction. It's likely the GameObject name you supplied is misspelled or that the GameObject does not have one of those three location type scripts on it.");
                break;
        }
    }

    public void NodeButtonInput(int button)
    {
        //This method runs if you click on one of the movement buttons on a Node screen. It just takes the button you pushed, grabs the location to move to associated with it, and switches locations to that one
        if (time == 1)
        {
            ChangeLocation(currentNode.modernMovements[button]);
        }
        else
        {
            ChangeLocation(currentNode.historicMovements[button]);
        }
        
    }

    public void BackInput()
    {
        //This is the method that runs when you hit back on Interactions or Containers. It sends you back to the last location, usually the node that sent you to that interaction or container
        if (locationType == 1 || locationType == 2)
        {
            //You can only go back for containers or interactions, usually you can't just hit "Back" on a Node
            ChangeLocation(lastLocation);
        }
        else
        {
            Debug.Log("You tried to go back on a location that does not have a back condition attached");
        }
    }

    public void NextInput(bool correct)
    {
        //A correct value means that you are heading forward, and if correct is false it means you hit a try again button and need to go back
        //This is the method that runs when you hit Next or Try Again on Interactions. It sends you on to the next location that you've unlocked, or if incorrect, back to the interaction
        if (locationType == 2)
        {
            //You can only go next for interactions, usually you can't just hit "Next" or "Try Again" on any screen
            if (correct)
            {
                //If we have the Next button we head on, but if we have it double sided we check what side we are on and make a descicion from there

                if (!currentInteraction.doubleSided)
                {
                    ChangeLocation(currentInteraction.location1);
                }
                else
                {
                    if (lastLocation == currentInteraction.location1)
                    {
                        ChangeLocation(currentInteraction.location2);
                    }
                    else if (lastLocation == currentInteraction.location2)
                    {
                        ChangeLocation(currentInteraction.location1);
                    }
                    else
                    {
                        Debug.LogWarning("Uh Oh, this interaction is marked as double sided and accessed from a location that is not registered as one of it's sides. Sending you to side 1.");
                        ChangeLocation(currentInteraction.location1);
                    }
                }
                
            } 
            else
            {
                //If we have the Try Again button we just load the interaction again
                incorrectUIElements.SetActive(false);
                inventoryManager.ShowInventoryUI();
                inventoryManager.EnableInventoryButtons();
            }
            
        }
        else
        {
            Debug.Log("You tried to go 'Next' on a location that does not have a next condition attached!");
        }
    }

    public void SuccessfulInteraction()
    {
        //The player used a correct item during an interaction! We should let them know, then move on!
        //Also, if it's an unlockable interaction, we unlock it
        if (currentInteraction.unlockable)
        {
            currentInteraction.setLock = true;
        }

        //We hide the inventory and open the "Correct" panel and wait for the NextInput method to be run
        inventoryManager.HideInventoryUI();
        correctUIElements.SetActive(true);
        correctText.text = currentInteraction.correctMessage;
    }

    public void UnsuccessfulInteraction()
    {
        //The player used an incorrect item during an interaction! We should let them know, then send them back to the interaction

        //We hide the inventory and open the "Incorrect" panel and wait for the NextInput method to be run
        inventoryManager.HideInventoryUI();
        incorrectUIElements.SetActive(true);
        incorrectText.text = currentInteraction.incorrectMessage;
    }

    public void SwitchTime()
    {
        if (time == 1)
        {
            time = 0;
        }
        else
        {
            time = 1;
        }
        //Switch the current node over to the new time
        UpdateUI();
    }






    // ----------
    // Everything below this is just part of the AddSpaces method to make it easier to understand
    // ----------
    public string AddSpaces(string input)
    {
        string stringToModify = input;
        int i;
        for (i=1; i < stringToModify.Length; i++)
        {
            //char = character
            //It goes through each char and adds a space before it if these conditions are met:
            //If the char is a number and the char before it is a letter of some sort
            //If the char is an uppercase letter and the char before it is a number or a lowercase letter
            //If the char is an uppercase letter and not the last one in the string (If I try to check characters past the last one it errors) and there's a lowercase char after it
           
            if (IsCharDigit(stringToModify[i]) == true && IsCharLetter(stringToModify[i - 1]) == true)
            {
                stringToModify = stringToModify.Insert(i, " ");
            }
            else if (IsCharUppercase(stringToModify[i]) == true)
            {
                if (IsCharDigit(stringToModify[i-1]) == true || IsCharLowercase(stringToModify[i-1]) == true)
                {
                    stringToModify = stringToModify.Insert(i, " ");
                }
                else if (i < (stringToModify.Length - 1) && IsCharLowercase (stringToModify[i+1]) == true && IsCharLetter (stringToModify[i-1]) == true)
                {
                    stringToModify = stringToModify.Insert(i, " ");
                }
            }
        }
        return stringToModify;
    }
    
    private bool IsCharUppercase(char input)
    {
        //Returns true if the input char is a letter that is uppercase
        if (!System.Char.IsDigit(input) && input.ToString().ToUpper() == input.ToString() && input.ToString() != " ")
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool IsCharLowercase (char input)
    {
        //Returns true if the input char is a letter that is lowercase
        if (!System.Char.IsDigit(input) && input.ToString().ToLower() == input.ToString() && input.ToString() != " ")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsCharDigit (char input)
    {
        //Returns true if the input char is a digit
        if (System.Char.IsDigit(input))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsCharLetter (char input)
    {
        //Returns true if the input char is a letter
        if (!System.Char.IsDigit(input) && input.ToString() != " ")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
