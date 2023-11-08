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
    private List<Item> inventory;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = new List<Item>(InventoryCapacity);
        EventChannels.ItemEvents.OnAddItemToInventory += AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromInventory += RemoveItem;
        EventChannels.ExtractionEvents.OnGetInventoryValue += ReturnInventoryValue;
        EventChannels.ItemEvents.OnCheckIfListFits += CanAddItems;
        EventChannels.ItemEvents.OnCheckIfItemQuestCompleted += CheckIfItemQuestCompleted;
        EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits += CheckIfEnoughCredits;
        EventChannels.ItemEvents.OnCheckIfItemInInventory += CheckIfItemInInventory;
        EventChannels.ItemEvents.OnCreateStack += CreateNewStack;
        EventChannels.ItemEvents.OnRemoveFromStack += RemoveSpecificItem;
    }

    private void OnDestroy()
    {
        EventChannels.ItemEvents.OnAddItemToInventory -= AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromInventory -= RemoveItem;
        EventChannels.ExtractionEvents.OnGetInventoryValue -= ReturnInventoryValue;
        EventChannels.ItemEvents.OnCheckIfListFits -= CanAddItems;
        EventChannels.ItemEvents.OnCheckIfItemQuestCompleted -= CheckIfItemQuestCompleted;
        EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits -= CheckIfEnoughCredits;
        EventChannels.ItemEvents.OnCheckIfItemInInventory -= CheckIfItemInInventory;
        EventChannels.ItemEvents.OnCreateStack -= CreateNewStack;
        EventChannels.ItemEvents.OnRemoveFromStack -= RemoveSpecificItem;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(ItemData item, int amount)
    {
        if (inventory.Count != 0)
        {
            if (item.IsStackable)
            {
                foreach (Item itemInInventory in inventory)
                {
                    if (item == itemInInventory.data)
                    {
                        if (itemInInventory.amount < item.MaxStackAmount)
                        {
                            itemInInventory.amount += amount;
                        }
                        else
                        {
                            inventory.Add(new Item(item, amount));
                        }
                        return;
                    }
                }
                inventory.Add(new Item(item, amount));
            }
            else
            {
                inventory.Add(new Item(item, amount));
            }
        }
        else
        {
            if (inventory.Count < InventoryCapacity)
                inventory.Add(new Item(item, amount));
        }
        ClearEmptySlots();
    }

    public void CreateNewStack(ItemData item, int amount)
    {
        inventory.Add(new Item(item, amount));
        ClearEmptySlots();
    }

    void RemoveItem(ItemData item, int amount)
    {
        // make sure that 'amount' is never more than the total amount of items in a player's inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            Item itemInInventory = inventory[i];
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
                    inventory.Remove(itemInInventory);
                    RemoveItem(item, amount);
                }
            }
        }
        ClearEmptySlots();
    }
    
    void RemoveSpecificItem(ItemData item, int amount, int amountToRemove)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Item itemInInventory = inventory[i];
            if (itemInInventory.data == item && itemInInventory.amount == amount)
            {
                itemInInventory.amount -= amountToRemove;
            }
        }
        ClearEmptySlots();
    }
    public int GetCapacity()
    {
        return InventoryCapacity;
    }

    public List<Item> GetItems()
    {
        return inventory;
    }


    public int GetAmountOfItem(ItemData itemToFind)
    {
        foreach (Item item in inventory)
        {
            if (item.data == itemToFind)
            {
                return item.amount;
            }
        }
        return -1;
    }

    private void ReturnInventoryValue()
    {
        EventChannels.ExtractionEvents.OnSetInventoryValue?.Invoke(GetInventoryValue());
    }

    private int GetInventoryValue()
    {
        int value = 0;
        foreach (Item item in inventory)
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
        int currentSize = inventory.Count;
        foreach (Item itemToAdd in itemsToAdd)
        {
            bool added = false;
            foreach (Item item in inventory)
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
            foreach (Item inventoryItem in inventory)
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

    public bool CheckIfSlotFree()
    {
        for (int i = 0; i < InventoryCapacity; i++)
        {
            try
            {
                Item item = inventory[i];
                if (item.data != null)
                    continue;
            }
            catch (ArgumentOutOfRangeException)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckIfEnoughCredits(int requiredAmount)
    {
        int amountOfCredits = 0;

        foreach (Item item in inventory)
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
        foreach (Item item in inventory)
        {
            if (item.data.Name == "Credit")
            {
                if (item.amount > cost)
                {
                    item.amount -= cost;
                }
                else
                {
                    inventory.Remove(item);
                    cost -= item.amount;
                }
            }
        }
    }

    private void ClearEmptySlots()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Item item = inventory[i];
            if (item.amount <= 0)
                inventory.Remove(item);
        }
    }

    private bool CheckIfItemInInventory(ItemData item)
    {
        foreach (Item itemInInventory in inventory)
        {
            if (itemInInventory.data == item)
                return true;
        }
        return false;
    }
}
