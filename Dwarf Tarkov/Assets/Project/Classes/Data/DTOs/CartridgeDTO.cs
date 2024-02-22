using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CartridgeDTO : ItemDataDTO
{
    public CartridgeDTO(Cartridge cartridge)
    {
        Name = cartridge.Name;
        SellPrice = cartridge.SellPrice;
        Type = cartridge.Type;
        IsStackable = cartridge.IsStackable;
        MaxStackAmount = cartridge.MaxStackAmount;
        Description = cartridge.Description;
        BuyPrice = cartridge.BuyPrice;

    }

    public CartridgeDTO()
    {

    }
    public int BuyPrice;
}
