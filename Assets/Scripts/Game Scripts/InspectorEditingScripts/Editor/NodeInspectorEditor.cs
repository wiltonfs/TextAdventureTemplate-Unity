using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeInspectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        Node currentNode = (Node)target;

    }
}
