using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class InventoryGridHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject inventorySlot;
    private List<GameObject> inventorySlots = new List<GameObject>();
    private PlayerInventory inventory;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        EventChannels.ItemEvents.OnUpdateInventory += UpdateInventory;
        EventChannels.PlayerInputEvents.OnInventoryOpened += UpdateInventory;
        EventChannels.UIEvents.OnOpenBarteringMenu += UpdateInventory;
        for (int i = 0; i < inventory.GetCapacity(); i++)
        {
            GameObject slot = Instantiate(inventorySlot, transform);
            inventorySlots.Add(slot);
            slot.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnInventoryOpened -= UpdateInventory;
        EventChannels.OutpostEvents.OnShowOutpostInventory -= UpdateInventory;
        EventChannels.ItemEvents.OnUpdateInventory -= UpdateInventory;
        EventChannels.UIEvents.OnOpenBarteringMenu -= UpdateInventory;
    }
    void UpdateInventory()
    {
        List<Item> items = inventory.GetItems();
        if (items.Count > 0)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                GameObject slot = inventorySlots[i];
                if (i < items.Count)
                {
                    Item item = items[i];
                    slot.GetComponent<InventorySlotHandler>().SetSlot(item);
                }
                else
                {
                    // Clear the slot if there is no corresponding item
                    slot.GetComponent<InventorySlotHandler>().ClearSlot();
                }
            }
        }
    }

    void UpdateInventory(ShopkeepInventory shop)
    {
        List<Item> items = inventory.GetItems();
        if (items.Count > 0)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                GameObject slot = inventorySlots[i];
                if (i < items.Count)
                {
                    Item item = items[i];
                    slot.GetComponent<PlayerBarterSlotHandler>().SetSlot(item);
                }
                else
                {
                    // Clear the slot if there is no corresponding item
                    slot.GetComponent<PlayerBarterSlotHandler>().ClearSlot();
                }
            }
        }
    }
}
