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
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        EventChannels.PlayerInputEvents.OnInventoryOpened += UpdateInventory;
        for (int i = 0; i < inventory.GetCapacity(); i++)
        {
            GameObject slot = Instantiate(inventorySlot, transform);
            inventorySlots.Add(slot);
            slot.SetActive(true);
        }
    }

    void UpdateInventory()
    {
        List<Item> items = inventory.GetItems();
        if (items.Count > 0)
        {
            foreach (Item item in items)
            {
                foreach (GameObject slot in inventorySlots)
                {
                    if (!slot.GetComponent<InventorySlotHandler>().GetIsTaken())
                    {
                        slot.GetComponent<InventorySlotHandler>().SetSlot(item);
                        return;
                    }
                }
            }
        }
    }
}
