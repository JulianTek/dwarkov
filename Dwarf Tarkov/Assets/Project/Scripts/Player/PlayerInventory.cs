using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using System;
using Data;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int InventoryCapacity;
    [SerializeField]
    private List<Item> inventory;
    // Start is called before the first frame update
    void Awake()
    {

        EventChannels.ItemEvents.OnAddItemToInventory += AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromInventory += RemoveItem;
        EventChannels.ExtractionEvents.OnGetInventoryValue += ReturnInventoryValue;
        EventChannels.ItemEvents.OnCheckIfListFits += CanAddItems;
        EventChannels.ItemEvents.OnCheckIfItemQuestCompleted += CheckIfItemQuestCompleted;
        EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits += CheckIfEnoughCredits;
        EventChannels.ItemEvents.OnCheckIfItemInInventory += CheckIfItemInInventory;
        EventChannels.ItemEvents.OnCreateStack += CreateNewStack;
        EventChannels.ItemEvents.OnRemoveFromStack += RemoveSpecificItem;
        EventChannels.PlayerEvents.OnPlayerDeath += SetLostItems;
        EventChannels.DataEvents.OnGetPlayerInventory += GetItems;
        EventChannels.ItemEvents.OnGetItemCount += GetAmountOfItem;
        EventChannels.ItemEvents.OnGetPlayerInventory += GetItems;
        EventChannels.ItemEvents.OnGetInventoryCapacity += GetCapacity;
        EventChannels.BarteringEvents.OnPlayerBuysItem += BuyItem;

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
        EventChannels.PlayerEvents.OnPlayerDeath -= SetLostItems;
        EventChannels.DataEvents.OnGetPlayerInventory -= GetItems;
        EventChannels.ItemEvents.OnGetItemCount -= GetAmountOfItem;
        EventChannels.ItemEvents.OnGetPlayerInventory -= GetItems;
        EventChannels.ItemEvents.OnGetInventoryCapacity -= GetCapacity;
        EventChannels.BarteringEvents.OnPlayerBuysItem -= BuyItem;
    }

    private void Start()
    {
        LoadItems();
        EventChannels.ItemEvents.OnGetInventory?.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Adds an item to the player's inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="amount">The amount to add to the inventory</param>
    public void AddItem(ItemData item, int amount)
    {
        // If there are items in the inventory we check to make sure there's not an existing stack already
        if (inventory.Count != 0)
        {
            // if the item can be stack, we have to loop through every stack to make sure there's not an existing stack already
            if (item.IsStackable)
            {
                foreach (Item itemInInventory in inventory)
                {
                    // if the item matches the one in the inventory
                    if (item == itemInInventory.data)
                    {
                        // if the adding the amount does not exceed the max stack amount, add it to the stack
                        if (itemInInventory.amount + amount <= item.MaxStackAmount)
                        {
                            itemInInventory.amount += amount;
                        }
                        // otherwise, create a new stack
                        else
                        {
                            inventory.Add(new Item(item, amount));
                        }
                        return;
                    }
                }
                // if no matching item could be found, create a new stack
                inventory.Add(new Item(item, amount));
            }
            // if item is not stackable, create a new stack no matter what
            else
            {
                inventory.Add(new Item(item, amount));
            }
        }
        // if no items exist yet, create a new stack
        else
        {
            if (inventory.Count < InventoryCapacity)
                inventory.Add(new Item(item, amount));
        }
        // Clear out any slots that have removed all their items
        ClearEmptySlots();
    }

    /// <summary>
    /// Forcibly creates a new stack
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="amount">Amount to stack</param>
    public void CreateNewStack(ItemData item, int amount)
    {
        inventory.Add(new Item(item, amount));
        ClearEmptySlots();
    }

    /// <summary>
    /// Removes items from the player's inventory
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <param name="amount">Amount to remove</param>
    void RemoveItem(ItemData item, int amount)
    {
        // make sure that 'amount' is never more than the total amount of items in a player's inventory
        // Loop through the inventory, and find a matching items
        for (int i = 0; i < inventory.Count; i++)
        {
            Item itemInInventory = inventory[i];
            if (itemInInventory.data == item)
            {
                // If item is stackable
                if (item.IsStackable)
                {
                    // If the amount to remove is less than the amount in the inventory right now, remove the full amount from the stack
                    if (amount < itemInInventory.amount)
                    {
                        itemInInventory.amount -= amount;
                        return;
                    }
                    // Otherwise, completely remove the item from the list and keep looping through the list.
                    amount -= item.MaxStackAmount;
                    inventory.Remove(itemInInventory);
                    RemoveItem(item, amount);
                }
            }
        }
        // Clear out any slots that have removed all their items
        ClearEmptySlots();
    }

    /// <summary>
    /// Removes a specific item from the player's inventory & from a stack with a specified amount
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <param name="amount">Amount of that specific item in the player's inventory</param>
    /// <param name="amountToRemove">Amount to remove</param>
    void RemoveSpecificItem(ItemData item, int amount, int amountToRemove)
    {
        // Loop through the inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            // if items and amount match specified parameters, remove the amount to remove
            Item itemInInventory = inventory[i];
            if (itemInInventory.data == item && itemInInventory.amount == amount)
            {
                itemInInventory.amount -= amountToRemove;
            }
        }
        // Clear out any slots that have removed all their items
        ClearEmptySlots();
    }

    /// <summary>
    /// Gets the player's inventory capacity
    /// </summary>
    /// <returns>Player's inventory capacity</returns>
    public int GetCapacity()
    {
        return InventoryCapacity;
    }

    /// <summary>
    /// Gets the entire inventory
    /// </summary>
    /// <returns>Player inventory</returns>
    public List<Item> GetItems()
    {
        return inventory;
    }

    /// <summary>
    /// Gets the amount of a specified item
    /// </summary>
    /// <param name="itemToFind">Item to get the amount of</param>
    /// <returns>Amount of item in inventory, or -1 if item not in inventory</returns>
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
    
    /// <summary>
    /// Returns the inventory value and invokes the event to set it in the extraction scene
    /// </summary>
    private void ReturnInventoryValue()
    {
        EventChannels.ExtractionEvents.OnSetInventoryValue?.Invoke(GetInventoryValue());
    }

    /// <summary>
    /// Gets the inventory value
    /// </summary>
    /// <returns>Sell value of the player's inventory</returns>
    private int GetInventoryValue()
    {
        // Loops through the list, adds the sell price to a variable and returns the variable
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

    /// <summary>
    /// Checks if a list of items can be added to the inventory
    /// </summary>
    /// <param name="itemsToAdd">Items to possibly add</param>
    /// <returns></returns>
    public bool CanAddItems(List<Item> itemsToAdd)
    {
        // get amount of items
        int currentSize = inventory.Count;
        foreach (Item itemToAdd in itemsToAdd)
        {
            // check items to add and compare to items in inventory
            bool added = false;
            foreach (Item item in inventory)
            {
                if (item.data == itemToAdd.data)
                {
                    // if data is the same and is not at max capacity
                    if (item.amount < item.data.MaxStackAmount)
                    {
                        // if adding items fits
                        int room = item.data.MaxStackAmount - item.amount;
                        if (room >= itemToAdd.amount)
                        {
                            // swap added to true, meaning it can safely be added to the inventory
                            added = true;
                            break;
                        }
                    }
                }
            }
            // if the game has to check if there is a free slot
            if (!added)
            {
                // return true if adding to a new slot does not override capacity
                currentSize++;
                if (currentSize > InventoryCapacity)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Retrieves quantity of specific items, specified by an item quest
    /// </summary>
    /// <param name="requiredItems">Quest's required items</param>
    /// <returns>True if requirements met, false if not</returns>
    public bool CheckIfItemQuestCompleted(List<Item> requiredItems)
    {
        // Requirementsmet is a variable that counts the amount of items which the player has succesfully gathered
        int requirementsMet = 0;
        // Loop through the list
        foreach (Item item in requiredItems)
        {
            // Save the amount required for the specific item
            int amountRequired = item.amount;
            foreach (Item inventoryItem in inventory)
            {
                // Find matching item in player inventory
                if (inventoryItem.data == item.data)
                {
                    // Subtract amount of item the player has
                    amountRequired -= inventoryItem.amount;
                    if (amountRequired <= 0)
                    {
                        // add 1 to requirementsMet if player has enough items
                        requirementsMet++;
                    }
                }
            }
        }
        return requirementsMet == requiredItems.Count;
    }

    /// <summary>
    /// Checks if there is a single slot free
    /// </summary>
    /// <returns>True if there is a free slot</returns>
    public bool CheckIfSlotFree()
    {
        // Loop through the list
        for (int i = 0; i < InventoryCapacity; i++)
        {
            // This method tries to get the item data in every index of the inventory.
            // If the method catches an exception, it means the item data is null and therefore a slot is free
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
        // if the method can succesfully loop through the list, it means there is no free slot
        return false;
    }


    /// <summary>
    /// Checks if the player has enough credits to buy an item
    /// </summary>
    /// <param name="requiredAmount">Amount of credits required</param>
    /// <returns>True if the player has enough credits, false if the player does not</returns>
    public bool CheckIfEnoughCredits(int requiredAmount)
    {
        // This method loops through the inventory, and adds the 'amount' of any item that has the name "Credit."
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

    /// <summary>
    /// Sets the lost inventory upon player death, which can be retrieved next run in the mines
    /// </summary>
    private void SetLostItems()
    {
        EventChannels.ItemEvents.OnSetLostItems?.Invoke(inventory);
        inventory.Clear();
        ClearEmptySlots();
        ItemDataHandler.DeleteInventory();
        SceneManager.LoadScene(4);
    }

    /// <summary>
    /// Removes specified amount of credits, to be used when buying an item
    /// </summary>
    /// <param name="cost">The cost of an item</param>
    public void BuyItem(int cost)
    {
        // This method is only called if the player has enough credits, so there's no need to check if the player has enough
        // This method loops through the list and removes the appropriate amount of credits
        foreach (Item item in inventory)
        {
            if (item.data.Name == "Credit")
            {
                // This is stack logic, if the amount to be removed is less than what is in the inventory, decrease the 'amount' by the cost.
                if (item.amount > cost)
                {
                    item.amount -= cost;
                }
                else
                {
                    // Otherwise, completely remove the item from the list and decrease the "cost" by the stack amount. The method will continue through the list
                    inventory.Remove(item);
                    cost -= item.amount;
                }
            }
        }
    }

    /// <summary>
    /// Clears the "empty" slots
    /// </summary>
    private void ClearEmptySlots()
    {
        // This method loops through the list, checks if any "amount" of item is 0 (or less than, just in case) and fully removes it from the list
        for (int i = 0; i < inventory.Count; i++)
        {
            Item item = inventory[i];
            if (item.amount <= 0)
                inventory.Remove(item);
        }
    }

    /// <summary>
    /// Checks if a specified item is in the player's inventory
    /// </summary>
    /// <param name="item">Item to find</param>
    /// <returns>True if item already is in inventory, false if it's not</returns>
    private bool CheckIfItemInInventory(ItemData item)
    {
        // This method loops through the list and returns true if the item in the list matches the item you're trying to find
        foreach (Item itemInInventory in inventory)
        {
            if (itemInInventory.data == item)
                return true;
        }
        // if no item matches what you're trying to find, it returns false
        return false;
    }

    /// <summary>
    /// Loads the items from save data into the inventory
    /// </summary>
    void LoadItems()
    {
        // Tries to get data, and loads dev data if none can be found
        var data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (data == null)
        {
            // if data is null, that means no instance of datahandler was found, meaning the scene was loaded from the editor, load dev data
            data = DataSaver<SaveData>.Load("dev");
            if (data != null && data.PlayerInventory != null)
            {
                inventory = DTOConverter.ConvertItemDTOListToItemList(data.PlayerInventory);
            }
            else
            {
                // if no data exists, create a new instance
                data = new SaveData();
                AddItem(EventChannels.DatabaseEvents.OnGetItemData?.Invoke("Credit"), 99);
                AddItem(EventChannels.DatabaseEvents.OnGetSubtype?.Invoke("6.11x54mm Green-tip"), 99);
                AddItem(EventChannels.DatabaseEvents.OnGetSubtype?.Invoke("14G Buckshot"), 99);
                AddItem(EventChannels.DatabaseEvents.OnGetSubtype?.Invoke("9.22 DWS FMJ"), 99);
                StartCoroutine(data.SaveDevData(inventory));
            }
        }
        else
        {
            if (data.PlayerInventory != null && data.PlayerInventory.Count > 0)
            {
                inventory = DTOConverter.ConvertItemDTOListToItemList(data.PlayerInventory);
            }
            else
            {
                inventory = new List<Item>(GetCapacity());
            }
        }
    }
} 
