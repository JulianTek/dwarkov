using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepInventory : MonoBehaviour
{
    public List<ShopKeepItem> inventory = new List<ShopKeepItem>();
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.UIEvents.OnPlayerSelectsItemToBuy += BuyItem;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnPlayerSelectsItemToBuy -= BuyItem;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyItem(ItemData data)
    {
        ShopKeepItem item = FindShopKeepItemInList(data);
        if (item != null)
        {
            if (EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits(item.CostPerItem))
            {
                BuyItem(item);
            }
            else
            {
                Debug.Log("Not Enough credits");
            }
        }
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
