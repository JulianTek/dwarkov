using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellboxHandler : MonoBehaviour
{
    private const int capacity = 9;
    private int currentAmountInBox;
    private List<Item> itemsInBox = new List<Item>();
    private List<GameObject> slots = new List<GameObject>();
    private void Start()
    {
        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            slots.Add(g.gameObject);
        }
        EventChannels.BarteringEvents.OnPlayerMovesItemToSellBox += AddItemToBox;
    }

    public bool CanAddItem()
    {
        return currentAmountInBox < capacity;
    }

    public void AddItemToBox(Item item)
    {
        itemsInBox.Add(item);
        slots[GetFirstFreeIndex()].GetComponent<SellboxSlotHandler>().SetSlot(item);
    }

    public void RemoveItemFromBox(GameObject slot)
    {
        Item item = slot.GetComponent<SellboxSlotHandler>().GetItem();
        if (itemsInBox.Contains(item))
        {
            slots[slots.IndexOf(slot)].GetComponent<SellboxSlotHandler>().ClearSlot();
            itemsInBox.Remove(item);
        }

    }

    public int GetFirstFreeIndex()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            GameObject slot = slots[i];
            if (!slot.GetComponent<SellboxSlotHandler>().isTaken)
                return i;
        }
        return int.MaxValue;
    }
}
