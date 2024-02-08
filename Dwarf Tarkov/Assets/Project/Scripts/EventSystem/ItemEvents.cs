using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemEvents
{
    public delegate void ItemEvent(ItemData item);
    public delegate bool ItemBoolEvent(ItemData item);
    public delegate void ItemQuantityEvent(ItemData item, int quantity);
    public delegate void ItemStackEvent(ItemData item, int quantity, int quantityToRemove);
    public delegate void InventoryEvent();
    public delegate bool ItemListBoolEvent(List<Item> items);
    public delegate void ItemListEvent(List<Item> items);
    public delegate AmmoSubtype GetAmmoTypeEvent();
    public delegate List<AmmoSubtype> GetAmmoListEvent();

    public ItemQuantityEvent OnAddItemToInventory;
    public ItemQuantityEvent OnRemoveItemFromInventory;

    public ItemQuantityEvent OnAddItemToOutpostInventory;
    public ItemQuantityEvent OnRemoveItemFromOutpostInventory;

    public ItemEvent OnPlayerCollidesWithItem;

    public InventoryEvent OnUpdateInventory;
    public InventoryEvent OnUpdateOutpostInventory;

    public ItemListBoolEvent OnCheckIfListFits;
    public ItemListBoolEvent OnCheckIfItemQuestCompleted;

    public ItemQuantityEvent OnCreateStack;
    public ItemStackEvent OnRemoveFromStack;

    public ItemBoolEvent OnCheckIfItemInInventory;

    public GetAmmoTypeEvent OnGetCurrentlyLoadedAmmo;
    public GetAmmoListEvent OnGetSubtypesInInventory;

    public ItemListEvent OnSetLostItems;
}
