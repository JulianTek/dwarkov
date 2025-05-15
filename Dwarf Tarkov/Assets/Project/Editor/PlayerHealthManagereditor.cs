using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EventSystem;

[CustomEditor(typeof(PlayerHealthManager))]
public class PlayerHealthManagereditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Take 10 damage!"))
            EventChannels.EnemyEvents.OnEnemyAttack?.Invoke(10f);
        if (GUILayout.Button("Heal 10 damage!"))
            EventChannels.PlayerEvents.OnPlayerHeal?.Invoke(10f, 0, false);
        if (GUILayout.Button("Heal 10 damage over 10 seconds!"))
            EventChannels.PlayerEvents.OnPlayerHeal?.Invoke(10f, 10f, true);
    }
}
