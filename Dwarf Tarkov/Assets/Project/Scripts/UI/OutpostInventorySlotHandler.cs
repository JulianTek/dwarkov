using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventSystem;

public class OutpostInventorySlotHandler : MonoBehaviour
{
    private bool isTaken = false;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI stackText;
    private bool isPlayerInventory;
    private Item item;
    private void Start()
    {
    }

    public void SetSlot(Item item)
    {
        if (item != null)
        {
            this.item = item;
            SetSprite(item.data.Sprite);
            SetIsTaken(true);
            image.enabled = true;
            if (item.data.IsStackable)
            {
                stackText.text = item.amount.ToString();
                stackText.enabled = true;
            }
        }
    }
    private void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    private void SetIsTaken(bool isNowTaken)
    {
        isTaken = isNowTaken;
    }

    public bool GetIsTaken()
    {
        return isTaken;
    }

    public void ClearSlot()
    {
        SetSprite(null);
        SetIsTaken(false);
        image.enabled = false;
        stackText.text = null;
        stackText.enabled = false;
    }

    public void SwitchItemBetweenStorage()
    {
        if (item != null)
        {
            if (isPlayerInventory)
            {
                EventChannels.ItemEvents.OnRemoveItemFromInventory?.Invoke(item.data, item.amount);
                EventChannels.ItemEvents.OnAddItemToOutpostInventory?.Invoke(item.data, item.amount);
            }
            else
            {
                EventChannels.ItemEvents.OnRemoveItemFromOutpostInventory?.Invoke(item.data, item.amount);
                EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(item.data, item.amount);
            }
            EventChannels.ItemEvents.OnUpdateOutpostInventory?.Invoke();
        }
    }

    public void SetIsPlayer(bool boolean)
    {
        isPlayerInventory = boolean;
    }
}
