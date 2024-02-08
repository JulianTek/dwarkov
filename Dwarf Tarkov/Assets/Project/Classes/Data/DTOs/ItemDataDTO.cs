using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDataDTO
{
    public ItemDataDTO(ItemData item)
    {
        Name = item.Name;
        SellPrice = item.SellPrice;
        Type = item.Type;
        IsStackable = item.IsStackable;
        MaxStackAmount = item.MaxStackAmount;
        Description = item.Description;
    }
    public string Name { get; private set; }
    public int SellPrice { get; private set; }
    public ItemType Type { get; private set; }
    public bool IsStackable { get; private set; }
    public int MaxStackAmount { get; private set; }
    public string Description { get; private set; }
}
