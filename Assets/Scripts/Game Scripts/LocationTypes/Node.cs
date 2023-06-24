using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // The whole "Text Area" things just make it so you get a text box instead of a single line, to make it easier to see the text and edit it
    //The name of the node, modern
    public string modernName;

    //The description of the node, modern
    [TextArea(1, 20)]
    public string modernDescription;

    //The 6 locations you can move to from this node, modern
    public string[] modernMovements = new string[6];



    //------------------------


    //The name of the node, historic
    public string historicName;

    //The description of the node, historic
    [TextArea(1, 20)]
    public string historicDescription;

    //The 6 locations you can move to from this node, historic
    public string[] historicMovements = new string[6];
}
