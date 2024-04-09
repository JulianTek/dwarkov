using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;
using TMPro;

public class BarterSubmenuSliderHandler : MonoBehaviour
{
    [SerializeField]
    private Slider sellSlider;
    [SerializeField]
    private Slider buySlider;
    [SerializeField]
    private TextMeshProUGUI sellAmountText;
    [SerializeField]
    private TextMeshProUGUI buyAmountText;
    [SerializeField]
    private TextMeshProUGUI costText;
    [SerializeField]
    private TextMeshProUGUI notEnoughCreditsText;

    private ItemData item;

    private void Start()
    {
        sellAmountText.enabled = false;
        buyAmountText.enabled = false;
        EventChannels.BarteringEvents.OnSetItemInSubmenu += SetItem;
    }

    private void OnDestroy()
    {
        EventChannels.BarteringEvents.OnSetItemInSubmenu -= SetItem;
    }

    public void SetItem(Item item)
    {
        sellSlider.maxValue = item.amount;
        buySlider.maxValue = item.amount;
        this.item = item.data;
    }

    public void SellAmount()
    {
        EventChannels.BarteringEvents.OnPlayerMovesQuantityToSellbox?.Invoke(item, (int)sellSlider.value);
        EventChannels.UIEvents.OnHideSellSubmenu?.Invoke();
        // i know that if you have multiple stacks of the same item it will just remove it from the first stack. i will probably fix it sometime
        EventChannels.ItemEvents.OnRemoveItemFromInventory(item, (int)sellSlider.value);
        // really sloppy way to do this, "opens" the bartering menu which will in turn also update the numbers in the player inventory. might improve someday
        EventChannels.UIEvents.OnOpenBarteringMenu?.Invoke(new ShopkeepInventory());
        ResetSlider();
    }

    public void BuyAmount()
    {
        if ((bool)EventChannels.BarteringEvents.OnCheckIfPlayerHasEnoughCredits?.Invoke((int)buySlider.value * item.BuyPrice))
        {
            EventChannels.BarteringEvents.OnPlayerBuysItem?.Invoke((int)buySlider.value * item.BuyPrice);
            EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(item, (int)buySlider.value);
            EventChannels.UIEvents.OnHideBuySubmenu?.Invoke();
            EventChannels.UIEvents.OnOpenBarteringMenu?.Invoke(new ShopkeepInventory());
            ResetSlider();
        }
        else
        {
            ShowNotEnoughCredits();
        }
    }

    public void SetText()
    {
        if (!sellAmountText.enabled)
            sellAmountText.enabled = true;
        if (!buyAmountText.enabled)
            buyAmountText.enabled = true;
        buyAmountText.SetText(buySlider.value.ToString());
    }

    private void ResetSlider()
    {
        sellSlider.value = 0;
        buySlider.value = 0;
    }

    public void SetCost()
    {
        costText.SetText($"Cost: {item.BuyPrice * buySlider.value} credits");
    }

    public void ShowNotEnoughCredits()
    {
        notEnoughCreditsText.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(CreditsTextCooldown());
    }

    private IEnumerator CreditsTextCooldown()
    {
        yield return new WaitForSecondsRealtime(2f);
        notEnoughCreditsText.gameObject.SetActive(false);
    }
}
