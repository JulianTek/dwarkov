using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class BarterUIPlayerInventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventorySlot;
    private List<GameObject> inventorySlots = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.UIEvents.OnOpenBarteringMenu += UpdateInventory;
        for (int i = 0; i < EventChannels.ItemEvents.OnGetInventoryCapacity?.Invoke(); i++)
        {
            GameObject slot = Instantiate(inventorySlot, transform);
            inventorySlots.Add(slot);
            slot.SetActive(true);
            // really sloppy way to force the inventory to update at the beginning of the game
            UpdateInventory(EventChannels.OutpostEvents.OnGetShopInventory?.Invoke());
        }
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnOpenBarteringMenu -= UpdateInventory;
    }

    void UpdateInventory(ShopkeepInventory shop)
    {
        List<Item> items = EventChannels.ItemEvents.OnGetPlayerInventory?.Invoke();
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
