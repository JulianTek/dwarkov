using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class BaseOutpostInventoryGridHandler : MonoBehaviour
{
    protected List<GameObject> inventorySlots = new List<GameObject>();
    [SerializeField]
    protected GameObject inventoryObject;
    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    protected GameObject buttonToSpawn;
    // Start is called before the first frame update
    protected void Start()
    {
        inventorySlots.Clear();
    }

    protected void SpawnSlots(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate(buttonToSpawn, transform);
            inventorySlots.Add(slot);
            slot.SetActive(true);
            slot.GetComponent<OutpostInventorySlotHandler>().SetIsPlayer(isPlayer);
        }
    }

    protected virtual void UpdateInventory() { }

    protected void UpdateSlots(List<Item> items)
    {
        if (items.Count > 0)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                GameObject slot = inventorySlots[i];
                OutpostInventorySlotHandler slotHandler = slot.GetComponent<OutpostInventorySlotHandler>();
                slotHandler.ClearSlot();
                if (i < items.Count)
                {
                    Item item = items[i];
                    slotHandler.SetSlot(item);
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
