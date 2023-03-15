using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepInteractionHandler : MonoBehaviour
{
    private bool playerIsInTrigger;
    private ShopkeepInventory inventory;

    private void Start()
    {
        inventory = GetComponentInParent<ShopkeepInventory>();
        EventChannels.PlayerInputEvents.OnInteract += Interact;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnInteract -= Interact;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
            playerIsInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
            playerIsInTrigger = false;
    }

    private void Interact()
    {
        if (playerIsInTrigger)
        {
            EventChannels.UIEvents.OnOpenBarteringMenu?.Invoke(inventory);
            EventChannels.PlayerInputEvents.OnEnableHUDControls?.Invoke();
        }

    }
}
 