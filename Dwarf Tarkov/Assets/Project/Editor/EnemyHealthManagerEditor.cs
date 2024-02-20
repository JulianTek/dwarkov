using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnemyHealthManager))]
public class EnemyHealthManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Kill enemy"))
        {
            EnemyHealthManager manager = (EnemyHealthManager)target;
            manager.TakeDamage(manager.GetEnemyHealth());
        }
    }
}
