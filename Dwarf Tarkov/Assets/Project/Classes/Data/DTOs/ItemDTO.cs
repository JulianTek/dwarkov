using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDTO
{

    public ItemDTO(Item item)
    {
        Data = new ItemDataDTO(item.data);
        Amount = item.amount;
    }

    public ItemDTO()
    {

    }
    public ItemDTO(ItemDataDTO data, int amount)
    {
        Amount = amount;
        Data = data;
    }
    public ItemDataDTO Data { get; protected set; }
    public int Amount { get; protected set; }
}
