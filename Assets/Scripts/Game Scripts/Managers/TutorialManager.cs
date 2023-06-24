using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private GameObject tutorialStuff;
    private Text instructionText;

    private bool openedMap, hasClicked, closedMap;
    // Start is called before the first frame update
    void Start()
    {
        tutorialStuff = GameObject.Find("Tutorial");
        tutorialStuff.SetActive(true);
        openedMap = closedMap = hasClicked = false;

        instructionText = GameObject.Find("InstructionsDisplay").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!openedMap)
        {
            instructionText.text = "Press M to open the Map!";
            //Flash
            if (Input.GetKeyDown(KeyCode.M))
            {
                openedMap = true;
            }
        }
        else if (!hasClicked)
        {
            instructionText.text = "This is the map. It will be updated as you explore. Use the arrow keys to navigate it. (Click to proceed)";
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hasClicked = true;
            }
        }
        else if (!closedMap)
        {
            instructionText.text = "Press M to close the Map!";
            if (Input.GetKeyDown(KeyCode.M))
            {
                closedMap = true;
            }
        } else
        {
            tutorialStuff.SetActive(false);
        }
        //First flash "Press M to open the Map" until you open map

        //Then say "This is the map. It will be updated as you explore. Use the arrow keys to navigate it."
        //Wait for click

        //Say "Press M to close the map" and wait until you are on game screen
        
        //Begin game, probably
    }
}
