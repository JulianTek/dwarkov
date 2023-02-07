using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvents
{
    public delegate void ItemEvent(ItemData item);
    public delegate void ItemQuantityEvent(ItemData item, int quantity);
    public delegate void InventoryEvent();

    public ItemQuantityEvent OnAddItemToInventory;
    public ItemQuantityEvent OnRemoveItemFromInventory;

    public ItemQuantityEvent OnAddItemToOutpostInventory;
    public ItemQuantityEvent OnRemoveItemFromOutpostInventory;

    public ItemEvent OnPlayerCollidesWithItem;

    public InventoryEvent OnUpdateInventory;
}
