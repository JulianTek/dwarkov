using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class OutpostInventoryInteractable : OutpostUIOpenInteractable
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        EventChannels.OutpostEvents.OnHideOutpostInventory += CloseMenu;
    }

    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnHideOutpostInventory -= CloseMenu;
    }
}
