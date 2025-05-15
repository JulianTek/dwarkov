using EventSystem;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepInventory : MonoBehaviour
{
    [SerializeField]
    private List<ShopKeepItem> inventory = new List<ShopKeepItem>();
    [HideInInspector]
    public List<ShopKeepItem> unlockedItems = new List<ShopKeepItem>();

    private void Awake()
    {
        EventChannels.OutpostEvents.OnGetShopInventory += GetShopInventory;
    }

    private ShopkeepInventory GetShopInventory()
    {
        return this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UnlockItems();
        EventChannels.OutpostEvents.OnGetShopInventory += GetShopInventory;
        EventChannels.UIEvents.OnPlayerSelectsItemToBuy += CanBuyItem;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnPlayerSelectsItemToBuy -= CanBuyItem;
        EventChannels.OutpostEvents.OnGetShopInventory -= GetShopInventory;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Checks if an item can be bought, and will show "not enough credits" text if player does not have enough credits
    /// </summary>
    /// <param name="data"></param>
    public void CanBuyItem(ItemData data)
    {
        // Gets the specific shopkeep item based on the data of the item the player is buying
        ShopKeepItem item = FindShopKeepItemInList(data);
        if (item != null)
        {
            // if this item is not null, check if the player has enough credits
            if (EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits(item.Data.BuyPrice))
            {
                // buy the item if the player has enough credits
                BuyItem(item);
            }
            else
            {
                // otherwise, show the not enough credits text
                EventChannels.BarteringEvents.OnPlayerHasInsufficientCredits?.Invoke();
            }
        }
    }

    /// <summary>
    /// Finds the shopkeep item assosciated with the given itemdata
    /// </summary>
    /// <param name="data">Data to find</param>
    /// <returns>The assosciated item, or null if item can't be found</returns>
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

    /// <summary>
    /// Buys the given item
    /// </summary>
    /// <param name="item">Item to buy</param>
    public void BuyItem(ShopKeepItem item)
    {
        // This adds 1 of the given item and removes the amount of credits the item costs, then closes the bartering menu
        EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(item.Data, 1);
        EventChannels.BarteringEvents.OnPlayerBuysItem?.Invoke(item.Data.BuyPrice);
        EventChannels.UIEvents.OnCloseBarteringMenu?.Invoke();
    }

    /// <summary>
    /// Unlocks the items to buy at a user's level
    /// </summary>
    public void UnlockItems()
    {
        unlockedItems.Clear();
        int playerLevel = (int)EventChannels.PlayerEvents.OnGetPlayerLevel?.Invoke();
        foreach (ShopKeepItem item in inventory)
        {
            if ( playerLevel>= item.UnlockLevel)
                unlockedItems.Add(item);
        }
    }
}
