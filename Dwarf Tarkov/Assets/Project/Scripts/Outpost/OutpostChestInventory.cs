using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class OutpostChestInventory : MonoBehaviour
{
    [SerializeField]
    private int inventoryCapacity = 30;
    private List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>(inventoryCapacity);
        EventChannels.OutpostEvents.OnAddItemToOutpostInventory += AddItem;
        EventChannels.OutpostEvents.OnRemoveItemFromOutpostInventory += RemoveItem;
    }

    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnAddItemToOutpostInventory -= AddItem;
        EventChannels.OutpostEvents.OnRemoveItemFromOutpostInventory -= RemoveItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItem(Item item)
    {
        items.Add(item);
    }

    void RemoveItem(Item item)
    {
        if (items.Contains(item))
            items.Remove(item);
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public int GetCapacity()
    {
        return inventoryCapacity;
    }
}
