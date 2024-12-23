using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostEvents
{
    public delegate void ItemEvent(ItemData item, int amount);
    public delegate void OutpostEvent();

    public ItemEvent OnAddItemToOutpostInventory;
    public ItemEvent OnRemoveItemFromOutpostInventory;

    public OutpostEvent OnShowOutpostInventory;
    public OutpostEvent OnHideOutpostInventory;

    public OutpostEvent OnEnterOutpost;

    public OutpostEvent OnShowWeaponBench;
    public OutpostEvent OnHideWeaponBench;
}
