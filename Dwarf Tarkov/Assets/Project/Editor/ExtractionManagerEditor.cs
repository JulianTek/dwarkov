using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EventSystem;

[CustomEditor(typeof(ExtractionManager))]
public class ExtractionManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Extract"))
        {
            EventChannels.ExtractionEvents.OnExtractionTimerFinished?.Invoke();
        }
    }
}
