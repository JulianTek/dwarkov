using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class LostInventoryHandler : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    void Start()
    {
        items = ItemDataHandler.LoadInventory("items.dkd");
        if (items.Count == 0)
            gameObject.SetActive(false);

        EventChannels.ItemEvents.OnSetLostItems += SetLostItems;
    }   

    void SetLostItems(List<Item> items)
    {
        this.items = items;
    }
}
