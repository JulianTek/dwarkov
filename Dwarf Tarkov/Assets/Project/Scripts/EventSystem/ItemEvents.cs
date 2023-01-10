using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvents
{
    public delegate void ItemEvent(ItemData item);
    public delegate void ItemQuantityEvent(ItemData item, int quantity);

    public ItemQuantityEvent OnAddItemToInventory;
    public ItemQuantityEvent OnRemoveItemFromInventory;

    public ItemEvent OnPlayerCollidesWithItem;
}
