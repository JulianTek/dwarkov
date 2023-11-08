using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBarterSlotHandler : MonoBehaviour
{
    private Item item;
    [SerializeField]
    private Image image;
    private bool isTaken;
    [SerializeField]
    private TextMeshProUGUI amountText;

    public void SetSlot(Item item)
    {
        this.item = item;
        image.sprite = item.data.Sprite;
        image.enabled = true;
        isTaken = true;
        if (item.data.IsStackable)
        {
            amountText.enabled = true;
            amountText.SetText(item.amount.ToString());
        }
    }

    public void ClearSlot()
    {
        item = null;
        image.sprite = null;
        image.enabled = false;
        isTaken = false;
        amountText.enabled = false;
    }

    public void SellItem()
    {
        if (item != null)
        {
            if (item.data.IsStackable)
            {
                EventChannels.BarteringEvents.OnSetItemInSubmenu?.Invoke(item);
                EventChannels.UIEvents.OnShowSubmenu?.Invoke();
            }
            else if ((bool)EventChannels.BarteringEvents.OnPlayerTryToSellItem?.Invoke())
            {
                EventChannels.BarteringEvents.OnPlayerMovesItemToSellBox(item);
                EventChannels.ItemEvents.OnRemoveItemFromInventory?.Invoke(item.data, item.amount);
                ClearSlot();
            }
        }
    }
}
