using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.SceneManagement;

public class LostInventoryHandler : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    void Start()
    {
        items = ItemDataHandler.LoadInventory("items");
        if (items == null || items.Count == 0 )
            gameObject.SetActive(false);

        EventChannels.ItemEvents.OnSetLostItems += SetLostItems;
    }   

    void SetLostItems(List<Item> items)
    {
        this.items = items;
        ItemDataHandler.SaveInventory("items", this.items);
        SceneManager.LoadScene(3);
    }
}
