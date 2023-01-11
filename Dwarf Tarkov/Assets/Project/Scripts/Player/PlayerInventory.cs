using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int InventoryCapacity;
    private List<Item> items;
    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>(InventoryCapacity);
        EventChannels.ItemEvents.OnAddItemToInventory += AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromInventory += RemoveItem;
    }

    private void OnDestroy()
    {
        EventChannels.ItemEvents.OnAddItemToInventory -= AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromInventory -= RemoveItem;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddItem(ItemData item, int amount)
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
                            items.Add(new Item(item));
                        }
                        return;
                    }
                }
            }
            else
            {
                items.Add(new Item(item));
            }
        }
        else
        {
            if (items.Count < InventoryCapacity)
                items.Add(new Item(item));
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
                    if (amount <= item.MaxStackAmount)
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
}
