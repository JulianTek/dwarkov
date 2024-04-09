using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellboxHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI valueText;

    private const int capacity = 9;
    private int currentAmountInBox;
    private List<Item> itemsInBox = new List<Item>();
    private List<GameObject> slots = new List<GameObject>();
    private int value;
    private void Start()
    {
        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            // if the child object has a SellBoxSlotHandler component, add it to the "slots" list.
            if (g.GetComponent<SellboxSlotHandler>())
                slots.Add(g.gameObject);
        }
        EventChannels.BarteringEvents.OnPlayerMovesItemToSellBox += AddItemToBox;
        EventChannels.BarteringEvents.OnPlayerTryToSellItem += CanAddItem;
        EventChannels.BarteringEvents.OnPlayerMovesQuantityToSellbox += AddQuantityToBox;
    }

    private void OnDestroy()
    {
        EventChannels.BarteringEvents.OnPlayerMovesItemToSellBox -= AddItemToBox;
        EventChannels.BarteringEvents.OnPlayerTryToSellItem -= CanAddItem;
        EventChannels.BarteringEvents.OnPlayerMovesQuantityToSellbox -= AddQuantityToBox;
    }

    public bool CanAddItem()
    {
        return currentAmountInBox < capacity;
    }

    public void AddItemToBox(Item item)
    {
        // add item to list
        itemsInBox.Add(item);
        // Set the first free slot's item to the item you're selling
        slots[GetFirstFreeIndex()].GetComponent<SellboxSlotHandler>().SetSlot(item);
        // add to currentAmountInBox
        currentAmountInBox++;
        // add item's value to total value
        value += item.data.SellPrice * item.amount;
        // update value text
        valueText.text = $"Value: {value} credits";
    }

    public void AddQuantityToBox(ItemData item, int amount)
    {
        // this method does the same as AddItemToBox, but it uses a custom amount
        itemsInBox.Add(new Item(item, amount));
        slots[GetFirstFreeIndex()].GetComponent<SellboxSlotHandler>().SetSlot(new Item(item, amount));
        currentAmountInBox++;
        value += item.SellPrice * amount;
        valueText.text = $"Value: {value} credits";
    }

    public void RemoveItemFromBox(GameObject slot)
    {
        // Get the item from the slot the player clicked
        Item item = slot.GetComponent<SellboxSlotHandler>().GetItem();
        // safeguarding, if item isnt null
        if (item != null)
        {
            // clear the slot
            slots[slots.IndexOf(slot)].GetComponent<SellboxSlotHandler>().ClearSlot();
            // remove the item from the list
            itemsInBox.Remove(item);
            // add the item back to the player's inventory
            EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(item.data, item.amount);
            currentAmountInBox--;
            // lower value
            value -= item.data.SellPrice * item.amount;
            // update the value text
            valueText.text = $"Value: {value} credits";
            // force the player's inventory UI to update
            EventChannels.UIEvents.OnOpenBarteringMenu?.Invoke(new ShopkeepInventory()) ;
        }

    }

    /// <summary>
    /// Gets the index of the first free slot
    /// </summary>
    /// <returns>index of the first free slot, or int.MaxValue if none are free</returns>
    public int GetFirstFreeIndex()
    {
        // loop through the list, and return the index of the first instance where the isTaken variable of SellBoxSlotHandler is false
        for (int i = 0; i < slots.Count; i++)
        {
            GameObject slot = slots[i];
            if (!slot.GetComponent<SellboxSlotHandler>().isTaken)
                return i;
        }
        return int.MaxValue;
    }

    /// <summary>
    /// Sells all items in the sell box
    /// </summary>
    public void SellItems()
    {
        EventChannels.BarteringEvents.OnPlayerSellsItems?.Invoke(value);
        // Loops through the list, clears the items from all slots and adds the correct amount of credits to the player inventory
        for (int i = 0; i < capacity; i++)
        {
            slots[i].GetComponent<SellboxSlotHandler>().ClearSlot();
        }
        valueText.text = $"Value: 0 credits";
        EventChannels.UIEvents.OnCloseBarteringMenu?.Invoke();
    }

    public void ClearSellbox()
    {
        foreach (GameObject slot in slots)
        {
            RemoveItemFromBox(slot);
        }
    }

    public bool CheckIfItemInList(ItemData data)
    {
        foreach (Item item in itemsInBox)
        {
            if (data == item.data)
                return true;
        }
        return false;
    }
}
