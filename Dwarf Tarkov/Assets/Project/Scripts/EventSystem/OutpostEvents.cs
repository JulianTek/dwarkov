using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OutpostEvents
{
    public delegate void ItemEvent(ItemData item, int amount);
    public delegate void OutpostEvent();
    public delegate ShopkeepInventory ShopInventoryEvent();
    public delegate void StringOutpostEvent(string value);
    public delegate int GetIntOutpostEvent();
    public delegate string ReturnStringEvent(string name);

    public ItemEvent OnAddItemToOutpostInventory;
    public ItemEvent OnRemoveItemFromOutpostInventory;

    public OutpostEvent OnShowOutpostInventory;
    public OutpostEvent OnHideOutpostInventory;

    public OutpostEvent OnEnterOutpost;

    public OutpostEvent OnShowWeaponBench;
    public OutpostEvent OnHideWeaponBench;

    public ShopInventoryEvent OnGetShopInventory;

    public StringOutpostEvent OnSelectBiome;
    public GetIntOutpostEvent OnGetSelectedScene;
    public StringOutpostEvent OnSetDescription;
    public ReturnStringEvent OnGetDescription;
    public StringOutpostEvent OnSetTitle;
}
