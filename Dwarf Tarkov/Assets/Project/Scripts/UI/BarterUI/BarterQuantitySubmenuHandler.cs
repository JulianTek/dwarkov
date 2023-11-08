using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using System;

public class BarterQuantitySubmenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject stackSubmenu;
    [SerializeField]
    private GameObject sellBox;

    private void Start()
    {
        EventChannels.UIEvents.OnShowSubmenu += ShowSubmenu;
        EventChannels.UIEvents.OnHideSubmenu += HideSubmenu;
        stackSubmenu.SetActive(false);
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnShowSubmenu -= ShowSubmenu;
        EventChannels.UIEvents.OnHideSubmenu -= HideSubmenu;
    }

    private void HideSubmenu()
    {
        stackSubmenu.SetActive(false);
        sellBox.SetActive(true);
    }

    void ShowSubmenu()
    {
        stackSubmenu.SetActive(true);
        sellBox.SetActive(false);
    }
}
