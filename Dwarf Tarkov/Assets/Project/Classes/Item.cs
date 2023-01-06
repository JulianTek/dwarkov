using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Item(ItemData data)
    {
        name = data.name;
        sellValue = data.SellPrice;
        type = data.Type;
    }
    private string name;
    private int sellValue;
    private ItemType type;

    public ItemType Type { get => type;  }
    public int SellValue { get => sellValue; }
    public string Name { get => name; }
}
