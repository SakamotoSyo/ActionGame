using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(ItemData))]
public class InspectorCustomization : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Add"))
        {
            Resources.Load<ItemData>("ItemBase").Item.Add(new ItemState(0, default, 0, null));
        }
    }
}
