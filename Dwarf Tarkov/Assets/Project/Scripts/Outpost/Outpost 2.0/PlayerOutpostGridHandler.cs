using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class PlayerOutpostGridHandler : BaseOutpostInventoryGridHandler
{
    private PlayerInventory inventory;

    private new void Start()
    {
        inventorySlots.Clear();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        SpawnSlots(inventory.GetCapacity());
        EventChannels.OutpostEvents.OnShowOutpostInventory += UpdateInventory;
        EventChannels.ItemEvents.OnUpdateOutpostInventory += UpdateInventory;
        UpdateInventory();
    }


    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnShowOutpostInventory -= UpdateInventory;
        EventChannels.ItemEvents.OnUpdateOutpostInventory -= UpdateInventory;
    }

    protected override void UpdateInventory()
    {
        UpdateSlots(inventory.GetItems());
    }
}
