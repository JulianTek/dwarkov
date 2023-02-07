using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostInventoryUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.OutpostEvents.OnShowOutpostInventory += ShowInventory;
        EventChannels.OutpostEvents.OnHideOutpostInventory += HideInventory;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnShowOutpostInventory -= ShowInventory;
        EventChannels.OutpostEvents.OnHideOutpostInventory -= HideInventory;
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
