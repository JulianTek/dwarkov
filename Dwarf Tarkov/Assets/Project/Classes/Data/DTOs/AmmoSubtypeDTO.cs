using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoSubtypeDTO : CartridgeDTO
{
    public AmmoSubtypeDTO(AmmoSubtype subtype)
    {
        Name = subtype.Name;
        SellPrice = subtype.SellPrice;
        Type = subtype.Type;
        IsStackable = subtype.IsStackable;
        MaxStackAmount = subtype.MaxStackAmount;
        Description = subtype.Description;
        BuyPrice = subtype.BuyPrice;
        BulletSpeed = subtype.BulletSpeed;
        Damage = subtype.Damage;
    }
    public float BulletSpeed;
    public int Damage;
}
