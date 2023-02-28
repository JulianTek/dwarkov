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
            if (g.GetComponent<SellboxSlotHandler>())
                slots.Add(g.gameObject);
        }
        EventChannels.BarteringEvents.OnPlayerMovesItemToSellBox += AddItemToBox;
        EventChannels.BarteringEvents.OnPlayerTryToSellItem += CanAddItem;
    }

    private void OnDestroy()
    {
        EventChannels.BarteringEvents.OnPlayerMovesItemToSellBox -= AddItemToBox;
        EventChannels.BarteringEvents.OnPlayerTryToSellItem -= CanAddItem;
    }

    public bool CanAddItem()
    {
        return currentAmountInBox < capacity;
    }

    public void AddItemToBox(Item item)
    {
        itemsInBox.Add(item);
        slots[GetFirstFreeIndex()].GetComponent<SellboxSlotHandler>().SetSlot(item);
        currentAmountInBox++;
        value += item.data.SellPrice * item.amount;
        valueText.text = $"Value: {value} credits";
    }

    public void RemoveItemFromBox(GameObject slot)
    {
        Item item = slot.GetComponent<SellboxSlotHandler>().GetItem();
        if (itemsInBox.Contains(item))
        {
            slots[slots.IndexOf(slot)].GetComponent<SellboxSlotHandler>().ClearSlot();
            itemsInBox.Remove(item);
            currentAmountInBox--;
            value -= item.data.SellPrice * item.amount;
            valueText.text = $"Value: {value} credits";
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

    public void SellItems()
    {
        EventChannels.BarteringEvents.OnPlayerSellsItems?.Invoke(value);
        for (int i = 0; i < capacity; i++)
        {
            slots[i].GetComponent<SellboxSlotHandler>().ClearSlot();
        }
        valueText.text = $"Value: 0 credits";
    }
}
