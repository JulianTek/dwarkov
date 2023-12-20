using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDTO
{
    public ItemDTO(ItemDataDTO data, int id)
    {
        Id = id;
        Data = data;
    }
    public ItemDataDTO Data { get; private set; }
    public int Id { get; private set; }
}
