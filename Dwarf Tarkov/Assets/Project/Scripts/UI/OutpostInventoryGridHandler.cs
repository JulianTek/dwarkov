using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class OutpostInventoryGridHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject inventorySlot;
    private List<GameObject> inventorySlots = new List<GameObject>();
    private PlayerInventory inventory;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        EventChannels.PlayerInputEvents.OnInventoryOpened += UpdateInventory;
        EventChannels.OutpostEvents.OnShowOutpostInventory += UpdateInventory;
        EventChannels.ItemEvents.OnUpdateOutpostInventory += UpdateInventory;
        for (int i = 0; i < inventory.GetCapacity(); i++)
        {
            GameObject slot = Instantiate(inventorySlot, transform);
            inventorySlots.Add(slot);
            slot.SetActive(true);
            slot.GetComponent<OutpostInventorySlotHandler>().SetIsPlayer(true);
        }
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnInventoryOpened -= UpdateInventory;
        EventChannels.OutpostEvents.OnShowOutpostInventory -= UpdateInventory;
        EventChannels.ItemEvents.OnUpdateInventory -= UpdateInventory;
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
                    if (items[i] == null && slot.GetComponent<OutpostInventorySlotHandler>().GetIsTaken())
                    {
                        slot.GetComponent<OutpostInventorySlotHandler>().ClearSlot();
                    }
                    Item item = items[i];
                    slot.GetComponent<OutpostInventorySlotHandler>().SetSlot(item);
                }
                else
                {
                    // Clear the slot if there is no corresponding item
                    slot.GetComponent<OutpostInventorySlotHandler>().ClearSlot();
                }
            }
        }
        else
        {
            foreach (GameObject slot in inventorySlots)
            {
                slot.GetComponent<OutpostInventorySlotHandler>().ClearSlot();
            }
        }
    }
}
