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

    public ItemDataDTO()
    {

    }
    public string Name { get; protected set; }
    public int SellPrice { get; protected set; }
    public ItemType Type { get; protected set; }
    public bool IsStackable { get; protected set; }
    public int MaxStackAmount { get; protected set; }
    public string Description { get; protected set; }
}
