using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class ItemHandler : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;
    [SerializeField]
    private int amountToAdd;
    private void Start()
    {
        EventChannels.ItemEvents.OnPlayerCollidesWithItem += AddItemToPlayerInventory;
    }

    private void OnDestroy()
    {
        EventChannels.ItemEvents.OnPlayerCollidesWithItem -= AddItemToPlayerInventory;
    }
    void AddItemToPlayerInventory(ItemData item)
    {
        if (item == itemData)
        {
            EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(item, amountToAdd);
            Destroy(gameObject);
        }
    }

    public ItemData GetItemData()
    {
        return itemData;
    }
}
