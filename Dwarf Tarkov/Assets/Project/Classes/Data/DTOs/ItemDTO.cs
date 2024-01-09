using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDTO
{
    public ItemDTO(Item item)
    {
        Data = new ItemDataDTO(item.data);
        Amount = item.amount;
    }
    public ItemDTO(ItemDataDTO data, int amount)
    {
        Amount = amount;
        Data = data;
    }
    public ItemDataDTO Data { get; private set; }
    public int Amount { get; private set; }
}
