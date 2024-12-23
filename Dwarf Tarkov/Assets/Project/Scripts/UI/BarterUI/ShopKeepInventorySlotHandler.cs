using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class ShopKeepInventorySlotHandler : MonoBehaviour
{
    private ShopKeepItem item;
    private bool isTaken;
    [SerializeField]
    private Image image;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetSlot(ShopKeepItem item)
    {
        if (item != null)
        {
            this.item = item;
            image.sprite = item.Data.Sprite;
            image.enabled = true;
            isTaken = true;
        }
    }

    public void Clear()
    {
        item = null;
        image.sprite = null;
        image.enabled = false;
        isTaken = false;
    }

    public void BuyItem()
    {
        if (item.Data.IsStackable)
        {
            EventChannels.BarteringEvents.OnSetItemInSubmenu?.Invoke(new Item(item.Data, item.Data.MaxStackAmount));
            EventChannels.UIEvents.OnShowBuySubmenu?.Invoke();
        }
        else
            EventChannels.UIEvents.OnPlayerSelectsItemToBuy?.Invoke(item.Data);
    }
}
