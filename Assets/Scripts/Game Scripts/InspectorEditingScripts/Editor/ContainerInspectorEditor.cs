using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Container))]
public class ContainerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Container currentContainer = (Container)target;

        int i;

        for (i = 0; i < currentContainer.items.Length; i++)
        {
            currentContainer.items[i] = EditorGUILayout.TextField("Item " + (i + 1).ToString() + ":", currentContainer.items[i]);
        }
    }
}
