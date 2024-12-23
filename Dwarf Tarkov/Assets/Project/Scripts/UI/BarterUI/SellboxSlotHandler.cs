using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellboxSlotHandler : MonoBehaviour
{
    private Item item;
    [SerializeField]
    private Image image;
    public bool isTaken;

    public void SetSlot(Item item)
    {
        this.item = item;
        image.sprite = item.data.Sprite;
        image.enabled = true;
        isTaken = true;
    }

    public void ClearSlot()
    {
        item = null;
        image.sprite = null;
        image.enabled = false;
        isTaken = false;
    }

    public Item GetItem()
    {
        return item;
    }
}
