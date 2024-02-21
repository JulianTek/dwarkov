using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using Data;
using System.Linq;
public class OutpostChestInventory : MonoBehaviour
{
    [SerializeField]
    private int inventoryCapacity = 30;
    [SerializeField]
    private List<Item> items;
    // Start is called before the first frame update
    void Start()
    {
        SaveData data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (data != null && data.OutpostInventory.Count > 0 && data.OutpostInventory != null)
        {
            items = data.ConvertDTOsToItems(data.OutpostInventory);
        }
        else
        {
            items = new List<Item>()
            {
                new Item(Resources.FindObjectsOfTypeAll<ItemData>().FirstOrDefault(item => item.Name == "6.11x54mm Full Metal Jacket"), 99)
            };
        }
        EventChannels.ItemEvents.OnAddItemToOutpostInventory += AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromOutpostInventory += RemoveItem;
    }

    private void OnDestroy()
    {
        EventChannels.ItemEvents.OnAddItemToOutpostInventory -= AddItem;
        EventChannels.ItemEvents.OnRemoveItemFromOutpostInventory -= RemoveItem;
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
            if (items.Count < inventoryCapacity)
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

    public List<Item> GetItems()
    {
        return items;
    }

    public int GetCapacity()
    {
        return inventoryCapacity;
    }

    public  List<ItemDTO> GetItemsAsDTOs()
    {
        List<ItemDTO> dtos = new List<ItemDTO>();
        foreach (Item item in items)
        {
            dtos.Add(new ItemDTO(item));
        }
        return dtos;
    }
}
