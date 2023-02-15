using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepInventory : MonoBehaviour
{
    [SerializeField]
    private List<ShopKeepItem> inventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyItem(ItemData data)
    {
        ShopKeepItem item = FindShopKeepItemInList(data);
        if (item != null)
            if (EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits(item.CostPerItem))
                BuyItem(item);

    }

    public ShopKeepItem FindShopKeepItemInList(ItemData data)
    {
        foreach (ShopKeepItem item in inventory)
        {
            if (item.Data == data)
            {
                return item;
            }
        }
        return null;
    }

    public void BuyItem(ShopKeepItem item)
    {
        EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(item.Data, 1);
        EventChannels.BarteringEvents.OnPlayerBuysItem?.Invoke(item.CostPerItem);
    }
}
