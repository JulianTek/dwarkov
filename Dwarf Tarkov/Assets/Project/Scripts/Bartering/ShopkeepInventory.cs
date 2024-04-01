using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepInventory : MonoBehaviour
{
    [SerializeField]
    private List<ShopKeepItem> inventory = new List<ShopKeepItem>();
    [HideInInspector]
    public List<ShopKeepItem> unlockedItems = new List<ShopKeepItem>();
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.UIEvents.OnPlayerSelectsItemToBuy += BuyItem;
        UnlockItems();
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
                EventChannels.BarteringEvents.OnPlayerHasInsufficientCredits?.Invoke();
            }
        }
    }

    public ShopKeepItem FindShopKeepItemInList(ItemData data)
    {
        foreach (ShopKeepItem item in unlockedItems)
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
        EventChannels.UIEvents.OnCloseBarteringMenu?.Invoke();
    }

    public void UnlockItems()
    {
        unlockedItems.Clear();

        foreach (ShopKeepItem item in inventory)
        {
            if (EventChannels.PlayerEvents.OnGetPlayerLevel?.Invoke() >= item.UnlockLevel)
                unlockedItems.Add(item);
        }
    }
}
