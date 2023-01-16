using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData data;
    public int amount;

    public Item(ItemData data, int amount)
    {
        this.data = data;
        this.amount = amount;
    }
}
