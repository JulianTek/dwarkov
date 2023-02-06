using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostInventoryUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        EventChannels.OutpostEvents.OnShowOutpostInventory += ShowInventory;
        EventChannels.OutpostEvents.OnHideOutpostInventory += HideInventory;
    }

    private void OnDisable()
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
