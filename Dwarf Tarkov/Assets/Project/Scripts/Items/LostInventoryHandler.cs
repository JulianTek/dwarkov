using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.SceneManagement;

public class LostInventoryHandler : MonoBehaviour
{
    [SerializeField]
    private List<Item> items = new List<Item>();
    void Start()
    {
        items = ItemDataHandler.LoadInventory("items");
        if (items == null || items.Count == 0 )
            gameObject.SetActive(false);

        EventChannels.ItemEvents.OnSetLostItems += SetLostItems;
    }

    private void OnDestroy()
    {
        EventChannels.ItemEvents.OnSetLostItems -= SetLostItems;
    }

    void SetLostItems(List<Item> items)
    {
        this.items = items;
        ItemDataHandler.SaveInventory("items", this.items);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInputHandler>())
        {
            if (EventChannels.ItemEvents.OnCheckIfListFits(items))
            {
                foreach (var item in items)
                {
                    EventChannels.ItemEvents.OnAddItemToInventory(item.data, item.amount);
                }
                Destroy(gameObject);
            }
            DataSaver<List<Item>>.Delete("items");
        }
    }
}
