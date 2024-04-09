using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using System;

public class BarterQuantitySubmenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject sellStackSubmenu;
    [SerializeField]
    private GameObject buySubmenu;
    [SerializeField]
    private GameObject sellBox;

    private void Start()
    {
        EventChannels.UIEvents.OnShowSellSubmenu += ShowSellSubmenu;
        EventChannels.UIEvents.OnHideSellSubmenu += HideSellSubmenu;
        EventChannels.UIEvents.OnShowBuySubmenu += ShowBuySubmenu;
        EventChannels.UIEvents.OnHideBuySubmenu += HideBuySubmenu;
        sellStackSubmenu.SetActive(false);
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnShowSellSubmenu -= ShowSellSubmenu;
        EventChannels.UIEvents.OnHideSellSubmenu -= HideSellSubmenu;
        EventChannels.UIEvents.OnShowBuySubmenu -= ShowBuySubmenu;
        EventChannels.UIEvents.OnHideBuySubmenu -= HideBuySubmenu;
    }

    private void HideSellSubmenu()
    {
        sellStackSubmenu.SetActive(false);
        sellBox.SetActive(true);
    }

    private void ShowSellSubmenu()
    {
        sellStackSubmenu.SetActive(true);
        sellBox.SetActive(false);
    }

    private void ShowBuySubmenu()
    {
        buySubmenu.SetActive(true);
        sellBox.SetActive(false);
    }

    private void HideBuySubmenu()
    {
        buySubmenu.SetActive(false);
        sellBox.SetActive(true);
    }
}
