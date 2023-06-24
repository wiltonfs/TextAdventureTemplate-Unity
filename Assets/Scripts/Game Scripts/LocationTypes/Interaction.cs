using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //The description/prompt for the interaction
    public string description;

    //The items that succeed in the interaction, for now you can set the array size to what you need
    public string[] succesfulItems;

    //The message that is displayed when an incorrect item is attempted
    public string incorrectMessage;

    //The message that is displayed when a correct item is attempted
    public string correctMessage;

    //If the interaction should be accesible from both sides or just one. If you leave this false, then only destination1 is where you go.
    //If true, you can go to Destination 2 from Destination 1 and vice versa
    public bool doubleSided;

    //Where to go if the interaction is succesful, or where to go from destination 2
    public string location1;
    public string location2;



    //Determines if the interaction is unlockable. If the bool is set to true, a succesful interaction will lock the interaction open, if the bool is left on false, each time you return to the interaction you will
    //have to succeed at it again
    public bool unlockable;

    //If the interaction is unlockable, this bool measures if it's been unlocked or not
    private bool isUnlocked;

    //This is a bit of a funky thing called a Property, it allows other scripts to modify the isUnlockable variable without me having to make it public. I'm doing it this way for two reasons
    // 1) To practice Properties
    // 2) To not have the variable public so that the inspector is as clean as possible
    public bool setLock
    {
        get
        {
            return isUnlocked;
        }
        set
        {
            isUnlocked = value;
        }

    }

    private void Awake()
    {
        isUnlocked = false;
    }
}
