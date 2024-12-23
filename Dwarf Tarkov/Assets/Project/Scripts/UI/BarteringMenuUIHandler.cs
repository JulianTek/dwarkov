using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarteringMenuUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        EventChannels.UIEvents.OnCloseBarteringMenu += CloseMenu;
        EventChannels.PlayerInputEvents.OnInventoryClosed += CloseMenu;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnCloseBarteringMenu -= CloseMenu;
        EventChannels.PlayerInputEvents.OnInventoryClosed -= CloseMenu;
    }

    void CloseMenu()
    {
        gameObject.SetActive(false);
        EventChannels.PlayerInputEvents.OnDisableHUDControls?.Invoke();
    }
}
