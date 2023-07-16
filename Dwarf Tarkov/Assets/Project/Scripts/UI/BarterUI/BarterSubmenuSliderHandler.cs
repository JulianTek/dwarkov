using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;  

public class BarterSubmenuSliderHandler : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private ItemData item;

    public void SetItem(Item item)
    {
        slider.maxValue = item.amount;
        this.item = item.data;
    }

    public void SellAmount()
    {
        EventChannels.BarteringEvents.OnPlayerMovesQuantityToSellbox?.Invoke(item, (int)slider.value);
        EventChannels.UIEvents.OnHideSubmenu?.Invoke();
    }
}
