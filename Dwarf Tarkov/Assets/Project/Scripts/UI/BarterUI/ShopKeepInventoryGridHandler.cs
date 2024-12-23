using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using System;

public class ShopKeepInventoryGridHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;
    private List<GameObject> slots = new List<GameObject>();

    private ShopkeepInventory currentShopKeep;
    [SerializeField]
    private GameObject UI;
    private bool slotsInitialized;

    private void Start()
    {
        EventChannels.UIEvents.OnOpenBarteringMenu += OpenShop;
        slotsInitialized = false;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnOpenBarteringMenu -= OpenShop;
    }

    void OpenShop(ShopkeepInventory inventory)
    {
        UI.SetActive(true);
        if (slotsInitialized)
            return;
        currentShopKeep = inventory;
        InitSlots();
    }

    void InitSlots()
    {
        if (slots.Count > 0)
            ClearSlots();
        foreach (ShopKeepItem item in currentShopKeep.unlockedItems)
        {
            GameObject itemSlot = Instantiate(slot, transform);
            itemSlot.GetComponent<ShopKeepInventorySlotHandler>().SetSlot(item);
            slots.Add(itemSlot); 
        }
        slotsInitialized = true;
    }

    private void ClearSlots()
    {
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();
    }
}
