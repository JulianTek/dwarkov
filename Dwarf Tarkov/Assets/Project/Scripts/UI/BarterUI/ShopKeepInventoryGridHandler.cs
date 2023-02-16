using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class ShopKeepInventoryGridHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;
    private List<GameObject> slots = new List<GameObject>();

    private ShopkeepInventory currentShopKeep;
    [SerializeField]
    private GameObject UI;

    private void Start()
    {
        EventChannels.UIEvents.OnOpenBarteringMenu += OpenShop;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnOpenBarteringMenu -= OpenShop;
    }

    void OpenShop(ShopkeepInventory inventory)
    {
        currentShopKeep = inventory;
        UI.SetActive(true);
        InitSlots();
    }

    void InitSlots()
    {
        if (slots.Count > 0)
            slots.Clear();
        foreach (ShopKeepItem item in currentShopKeep.inventory)
        {
            Instantiate(slot, transform);
            slot.GetComponent<ShopKeepInventorySlotHandler>().SetSlot(item);
            slots.Add(slot); 
        }
    }
}
