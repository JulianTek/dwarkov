using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class InventoryUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerInputEvents.OnInventoryOpened += ShowInventory;
        EventChannels.PlayerInputEvents.OnInventoryClosed += HideInventory;
        gameObject.SetActive(false);
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnInventoryOpened -= ShowInventory;
        EventChannels.PlayerInputEvents.OnInventoryClosed -= HideInventory;
    }

    void ShowInventory()
    {
        gameObject.SetActive(true);
    }

    void HideInventory()
    {
        gameObject.SetActive(false);
    }
}
