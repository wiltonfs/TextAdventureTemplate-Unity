using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Interaction))]
public class InteractionInspectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interaction currentInteraction = (Interaction)target;

        EditorGUILayout.LabelField("Description:");
        currentInteraction.description = EditorGUILayout.TextArea(currentInteraction.description);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Correct Message:");
        currentInteraction.correctMessage = EditorGUILayout.TextArea(currentInteraction.correctMessage);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Incorrect Message:");
        currentInteraction.incorrectMessage = EditorGUILayout.TextArea(currentInteraction.incorrectMessage);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        //EditorGUILayout.IntField("# of Succesful Items", currentInteraction.succesfulItems.Length);

        int f = EditorGUILayout.IntField("# of Succesful Items", currentInteraction.succesfulItems.Length);
        if (f != currentInteraction.succesfulItems.Length)
        {
            currentInteraction.succesfulItems = new string[f];
        }

        int i;

        for (i = 0; i < currentInteraction.succesfulItems.Length; i++)
        {
            currentInteraction.succesfulItems[i] = EditorGUILayout.TextField("Item " + (i + 1).ToString() + ":", currentInteraction.succesfulItems[i]);
        }

        

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        currentInteraction.unlockable = EditorGUILayout.Toggle("Unlockable", currentInteraction.unlockable);
        EditorGUILayout.Space();

        currentInteraction.doubleSided = EditorGUILayout.Toggle("Double Sided", currentInteraction.doubleSided);

        

        if (currentInteraction.doubleSided)
        {
            currentInteraction.location1 = EditorGUILayout.TextField("Location 1", currentInteraction.location1);

            currentInteraction.location2 = EditorGUILayout.TextField("Location 2", currentInteraction.location2);
        }
        else
        {
            currentInteraction.location1 = EditorGUILayout.TextField("Destination", currentInteraction.location1);
        }

        

    }
}
