using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PlayerExperienceHandler))]
public class PlayerExperienceHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  

        if (GUILayout.Button("Level up player"))
        {
            PlayerExperienceHandler handler = (PlayerExperienceHandler)target;
            handler.LevelUp();
        }
    }
}
