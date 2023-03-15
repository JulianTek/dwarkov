using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarterSlotHandler : MonoBehaviour
{
    private Item item;
    [SerializeField]
    private Image image;
    private bool isTaken;

    public void SetSlot(Item item)
    {
        this.item = item;
        image.sprite = item.data.Sprite;
        image.enabled = true;
        isTaken = true;
    }

    public void ClearSlot()
    {
        item = null;
        image.sprite = null;
        image.enabled = false;
        isTaken = false;
    }

    public void SellItem()
    {
        if (item != null)
        {
            if ((bool)EventChannels.BarteringEvents.OnPlayerTryToSellItem?.Invoke())
            {
                EventChannels.BarteringEvents.OnPlayerMovesItemToSellBox(item);
                EventChannels.ItemEvents.OnRemoveItemFromInventory?.Invoke(item.data, item.amount);
                ClearSlot();
            }
        }
    }
}
