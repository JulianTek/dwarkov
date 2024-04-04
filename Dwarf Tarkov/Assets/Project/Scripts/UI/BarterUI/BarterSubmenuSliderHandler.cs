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
        // i know that if you have multiple stacks of the same item it will just remove it from the first stack. i will probably fix it sometime
        EventChannels.ItemEvents.OnRemoveItemFromInventory(item, (int)slider.value);
        // really sloppy way to do this, "opens" the bartering menu which will in turn also update the numbers in the player inventory. might improve someday
        EventChannels.UIEvents.OnOpenBarteringMenu?.Invoke(new ShopkeepInventory());
        ResetSlider();
    }

    public void SetText()
    {
        if (!amountText.enabled)
            amountText.enabled = true;
        amountText.SetText(slider.value.ToString());
    }

    private void ResetSlider()
    {
        slider.value = 0;
    }
}
