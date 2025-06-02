using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ShopkeepInteractionHandler : OutpostUIOpenInteractable
{
    [SerializeField]
    private ShopkeepInventory inventory;

    public override void RunInteraction()
    {
        EventChannels.UIEvents.OnOpenBarteringMenu?.Invoke(inventory);
        EventChannels.PlayerInputEvents.OnEnableHUDControls?.Invoke();
    }
}
