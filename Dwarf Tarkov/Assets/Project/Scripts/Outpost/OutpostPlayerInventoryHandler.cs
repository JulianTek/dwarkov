using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class OutpostPlayerInventoryHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject inventorySlot;
    private PlayerInventory inventory;
    private List<GameObject> inventorySlots = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.OutpostEvents.OnShowOutpostInventory += UpdateOutpostInventory;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
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
        EventChannels.OutpostEvents.OnShowOutpostInventory -= UpdateOutpostInventory;
    }

    void UpdateOutpostInventory()
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
                    slot.GetComponent<OutpostInventorySlotHandler>().SetSlot(item);
                }
                else
                {
                    // Clear the slot if there is no corresponding item
                    slot.GetComponent<OutpostInventorySlotHandler>().ClearSlot();
                }
            }
        }
    }
}
