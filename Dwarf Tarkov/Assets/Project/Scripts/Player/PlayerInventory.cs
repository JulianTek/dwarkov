using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using System;
using Data; 

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int InventoryCapacity;
    [SerializeField]
    private List<Item> items;
    // Start is called before the first frame update
    void Awake()
    {
        items = new List<Item>(InventoryCapacity);
        EventChannels.ItemEvents.OnAddItemToInventory += AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromInventory += RemoveItem;
        EventChannels.ExtractionEvents.OnGetInventoryValue += ReturnInventoryValue;
        EventChannels.ItemEvents.OnCheckIfListFits += CanAddItems;
        EventChannels.ItemEvents.OnCheckIfItemQuestCompleted += CheckIfItemQuestCompleted;
        EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits += CheckIfEnoughCredits;
    }

    private void OnDestroy()
    {
        EventChannels.ItemEvents.OnAddItemToInventory -= AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromInventory -= RemoveItem;
        EventChannels.ExtractionEvents.OnGetInventoryValue -= ReturnInventoryValue;
        EventChannels.ItemEvents.OnCheckIfListFits -= CanAddItems;
        EventChannels.ItemEvents.OnCheckIfItemQuestCompleted -= CheckIfItemQuestCompleted;
        EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits -= CheckIfEnoughCredits;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(ItemData item, int amount)
    {
        if (items.Count != 0)
        {
            if (item.IsStackable)
            {
                foreach (Item itemInInventory in items)
                {
                    if (item == itemInInventory.data)
                    {
                        if (itemInInventory.amount < item.MaxStackAmount)
                        {
                            itemInInventory.amount += amount;
                        }
                        else
                        {
                            items.Add(new Item(item, amount));
                        }
                        return;
                    }
                }
                items.Add(new Item(item, amount));
            }
            else
            {
                items.Add(new Item(item, amount));
            }
        }
        else
        {
            if (items.Count < InventoryCapacity)
                items.Add(new Item(item, amount));
        }
    }

    void RemoveItem(ItemData item, int amount)
    {
        // make sure that 'amount' is never more than the total amount of items in a player's inventory
        for (int i = 0; i < items.Count; i++)
        {
            Item itemInInventory = items[i];
            if (itemInInventory.data == item)
            {
                if (item.IsStackable)
                {
                    if (amount < item.MaxStackAmount)
                    {
                        itemInInventory.amount -= amount;
                        return;
                    }
                    amount -= item.MaxStackAmount;
                    items.Remove(itemInInventory);
                    RemoveItem(item, amount);
                }
            }
        }
    }
    public int GetCapacity()
    {
        return InventoryCapacity;
    }

    public List<Item> GetItems()
    {
        return items;
    }


    public int GetAmountOfItem(ItemData itemToFind)
    {
        foreach (Item item in items)
        {
            if (item.data == itemToFind)
            {
                return item.amount;
            }
        }
        return 0;
    }

    private void ReturnInventoryValue()
    {
        EventChannels.ExtractionEvents.OnSetInventoryValue?.Invoke(GetInventoryValue());
    }

    private int GetInventoryValue()
    {
        int value = 0;
        foreach (Item item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                value += item.data.SellPrice;
            }
        }
        return value;
    }

    public bool CanAddItems(List<Item> itemsToAdd)
    {
        int currentSize = items.Count;
        foreach (Item itemToAdd in itemsToAdd)
        {
            bool added = false;
            foreach (Item item in items)
            {
                if (item.amount < item.data.MaxStackAmount)
                {
                    int room = item.data.MaxStackAmount - item.amount;
                    if (room >= itemToAdd.amount)
                    {
                        added = true;
                        break;
                    }
                }
            }
            if (!added)
            {
                currentSize++;
                if (currentSize > InventoryCapacity)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool CheckIfItemQuestCompleted(List<Item> requiredItems)
    {
        int requirementsMet = 0;
        foreach (Item item in requiredItems)
        {
            int amountRequired = item.amount;
            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.data == item.data)
                {
                    amountRequired -= inventoryItem.amount;
                    if (amountRequired <= 0)
                    {
                        requirementsMet++;
                    }
                }
            }
        }
        return requirementsMet == requiredItems.Count;
    }

    public bool CheckIfEnoughCredits(int requiredAmount)
    {
        int amountOfCredits = 0;

        foreach (Item item in items)
        {
            if (item.data.Name == "Credit")
            {
                amountOfCredits += item.amount;
            }
        }
        
        return amountOfCredits >= requiredAmount;
    }

    public void BuyItem(int cost)
    {
        foreach (Item item in items)
        {
            if (item.data.Name == "Credit")
            {
                if (item.amount > cost)
                {
                    item.amount -= cost;
                }
                else
                {
                    items.Remove(item);
                    cost -= item.amount;
                }
            }
        }
    }
} 
