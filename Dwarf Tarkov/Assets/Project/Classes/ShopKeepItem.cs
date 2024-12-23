using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopKeepItem
{
    public ItemData Data;
    public int CostPerItem;
    public int UnlockLevel;

    public ShopKeepItem(ItemData data, int cost, int unlockLevel)
    {
        Data = data;
        CostPerItem = cost;
        UnlockLevel = unlockLevel;
    }

    public ShopKeepItem(ItemData data, int cost)
    {
        Data = data;
        CostPerItem = cost;
        UnlockLevel = 1;
    }
}
