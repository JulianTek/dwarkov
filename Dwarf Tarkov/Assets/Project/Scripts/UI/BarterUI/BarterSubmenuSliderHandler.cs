using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;
using TMPro;

public class BarterSubmenuSliderHandler : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI amountText;

    private ItemData item;

    private void Start()
    {
        amountText.enabled = false;
        EventChannels.BarteringEvents.OnSetItemInSubmenu += SetItem;
    }

    private void OnDestroy()
    {
        EventChannels.BarteringEvents.OnSetItemInSubmenu -= SetItem;
    }

    public void SetItem(Item item)
    {
        slider.maxValue = item.amount;
        this.item = item.data;
    }

    public void SellAmount()
    {
        EventChannels.BarteringEvents.OnPlayerMovesQuantityToSellbox?.Invoke(item, (int)slider.value);
        EventChannels.UIEvents.OnHideSubmenu?.Invoke();
        EventChannels.ItemEvents.OnRemoveItemFromInventory(item, (int)slider.value);
    }

    public void SetText()
    {
        if (!amountText.enabled)
            amountText.enabled = true;
        amountText.SetText(slider.value.ToString());
    }
}
