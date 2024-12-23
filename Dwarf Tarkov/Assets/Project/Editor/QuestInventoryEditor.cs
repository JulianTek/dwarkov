using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCQuestInventory))]
public class QuestInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Add item quest to list"))
        {
            NPCQuestInventory inventory = (NPCQuestInventory)target;
            inventory.quests.Add(new ItemQuest());
        }
        if (GUILayout.Button("Add new enemy quest to list"))
        {
            NPCQuestInventory inventory = (NPCQuestInventory)target;
            inventory.quests.Add(new EnemyQuest());
        }
        if (GUILayout.Button("Refresh unlocked quests"))
        {
            NPCQuestInventory inventory = (NPCQuestInventory)target;
            inventory.RefreshQuests();
        }
    }
}
