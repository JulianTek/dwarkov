using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EventSystem;
[CustomEditor(typeof(ExperiencePointsPoolHandler))]
public class ExperiencePoolHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Add 10 experience points to pool"))
        {
            EventChannels.PlayerEvents.OnExperienceGained?.Invoke(10);
        }
        if (GUILayout.Button("Add 100 experience points to pool"))
        {
            EventChannels.PlayerEvents.OnExperienceGained?.Invoke(100);
        }
        if (GUILayout.Button("Award pooled experience"))
        {
            ExperiencePointsPoolHandler handler = (ExperiencePointsPoolHandler)target;
            EventChannels.PlayerEvents.OnExperienceGiven?.Invoke(handler.GetPooledXP());
        }
    }
}
