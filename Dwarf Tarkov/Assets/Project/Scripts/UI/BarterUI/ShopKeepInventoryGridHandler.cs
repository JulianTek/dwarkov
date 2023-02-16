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
            ClearSlots();
        foreach (ShopKeepItem item in currentShopKeep.inventory)
        {
            GameObject itemSlot = Instantiate(slot, transform);
            itemSlot.GetComponent<ShopKeepInventorySlotHandler>().SetSlot(item);
            slots.Add(itemSlot); 
        }
    }

    private void ClearSlots()
    {
        foreach (GameObject slot in slots)
        {
            GameObject.Destroy(slot);
        }
        slots.Clear();
    }
}
