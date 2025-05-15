using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopKeepItem
{
    public ItemData Data;
    public int UnlockLevel;

    public ShopKeepItem(ItemData data, int unlockLevel)
    {
        Data = data;
        UnlockLevel = unlockLevel;
    }

    public ShopKeepItem(ItemData data)
    {
        Data = data;
        UnlockLevel = 1;
    }
}
