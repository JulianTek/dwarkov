using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class ItemCollisionHandler : MonoBehaviour
{
    private ItemData data;

    private void Start()
    {
        data = GetComponent<ItemHandler>().GetItemData();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            EventChannels.ItemEvents.OnPlayerCollidesWithItem?.Invoke(data);
        }
    }
}
