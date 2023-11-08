using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class OutpostChestToggler : MonoBehaviour
{
    private bool playerIsInTrigger;
    // Start is called before the first frame update
    void Start()
    {
        playerIsInTrigger = false;
        EventChannels.PlayerInputEvents.OnInteract += OpenInventory;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            playerIsInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            playerIsInTrigger = false;
        }
    }

    private void OpenInventory()
    {
        if (playerIsInTrigger)
        {
            EventChannels.OutpostEvents.OnShowOutpostInventory?.Invoke();
        }
    }
}
