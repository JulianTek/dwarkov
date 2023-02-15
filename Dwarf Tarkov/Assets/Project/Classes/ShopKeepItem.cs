using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopKeepItem
{
    public ItemData Data;
    public int CostPerItem;

    public ShopKeepItem(ItemData data, int cost)
    {
        Data = data;
        CostPerItem = cost;
    }
}
