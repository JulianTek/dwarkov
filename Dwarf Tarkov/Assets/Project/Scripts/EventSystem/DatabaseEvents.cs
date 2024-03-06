using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseEvents
{
    public delegate WeaponData GetWeaponDataEvent(string name);
    public delegate ItemData GetItemDataEvent(string name);
    public delegate AmmoSubtype GetSubtypeaEvent(string name);
    public delegate List<WeaponData> GetWeaponDataListEvent();
    public delegate List<ItemData> GetItemDataListEvent();

    public GetWeaponDataEvent OnGetWeaponData;
    public GetItemDataEvent OnGetItemData;
    public GetSubtypeaEvent OnGetSubtype;
    public GetWeaponDataListEvent OnGetAllWeapons;
    public GetItemDataListEvent OnGetAllItems;
}
