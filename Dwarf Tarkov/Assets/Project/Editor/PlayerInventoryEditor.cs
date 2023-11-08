using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerInventory))]
public class PlayerInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Add blank item to inventory to inventory"))
        {
            PlayerInventory inventory = (PlayerInventory)target;
            inventory.AddItem(Resources.Load<ItemData>("Credit"), 1);
        }
    }
}
