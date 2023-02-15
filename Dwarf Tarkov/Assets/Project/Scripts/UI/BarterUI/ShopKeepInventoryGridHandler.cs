using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeepInventoryGridHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;
    private List<GameObject> slots = new List<GameObject>();

    private ShopkeepInventory currentShopKeep;
    private GameObject UI;

    private void Start()
    {
        UI = transform.root.gameObject;
    }

    void OpenShop(ShopkeepInventory inventory)
    {
        currentShopKeep = inventory;
        UI.SetActive(true);
    }

    void InitSlots()
    {
        slots.Clear();
        foreach (ShopKeepItem item in currentShopKeep.inventory)
        {
            Instantiate(slot, transform);
            slot.GetComponent<ShopKeepInventorySlot>().SetSlot(item);
        }
    }
}
