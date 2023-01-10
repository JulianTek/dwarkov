using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData data;
    public int amount;

    public Item(ItemData data)
    {
        this.data = data;
    }
}
