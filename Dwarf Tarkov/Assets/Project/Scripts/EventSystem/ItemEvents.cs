using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemEvents
{
    public delegate void ItemEvent(ItemData item);
    public delegate bool ItemBoolEvent(ItemData item);
    public delegate void ItemQuantityEvent(ItemData item, int quantity);
    public delegate void InventoryEvent();
    public delegate bool ItemListEvent(List<Item> items);
    public delegate AmmoSubtype GetAmmoTypeEvent();
    public delegate List<AmmoSubtype> GetAmmoListEvent();

    public ItemQuantityEvent OnAddItemToInventory;
    public ItemQuantityEvent OnRemoveItemFromInventory;

    public ItemQuantityEvent OnAddItemToOutpostInventory;
    public ItemQuantityEvent OnRemoveItemFromOutpostInventory;

    public ItemEvent OnPlayerCollidesWithItem;

    public InventoryEvent OnUpdateInventory;
    public InventoryEvent OnUpdateOutpostInventory;

    public ItemListEvent OnCheckIfListFits;
    public ItemListEvent OnCheckIfItemQuestCompleted;

    public ItemBoolEvent OnCheckIfItemInInventory;

    public GetAmmoTypeEvent OnGetCurrentlyLoadedAmmo;
    public GetAmmoListEvent OnGetSubtypesInInventory;
}
