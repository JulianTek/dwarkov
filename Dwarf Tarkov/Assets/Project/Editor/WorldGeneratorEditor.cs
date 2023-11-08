using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WorldGeneration;

[CustomEditor(typeof(RoomGenerator))]
public class WorldGeneratorEditor : Editor
{
    RoomGenerator generator;

    private void Awake()
    {
        generator = (RoomGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate world"))
        {
            generator.Generate();
        }

        if (GUILayout.Button("Clear inspector"))
        {
            generator.Clear();
        }
    }

}