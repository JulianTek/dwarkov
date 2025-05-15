using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventSystem;

public class InventorySlotHandler : MonoBehaviour
{
    private bool isTaken = false;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI stackText;
    private Item item;


    private TooltipHandler tooltip;

    private void Start()
    {
        tooltip = GetComponent<TooltipHandler>();
    }

    public void UseItem()
    {
        EventChannels.ItemEvents.OnRemoveItemFromInventory(item.data, 1);
        item.data.UseItem();
        EventChannels.PlayerInputEvents.OnInventoryClosed?.Invoke();
        TooltipSystem.Hide();
    }

    public void SetSlot(Item item)
    {
        if (item != null)
        {
            SetSprite(item.data.Sprite);
            SetIsTaken(true);
            image.enabled = true;
            if (item.data.IsStackable)
            {
                stackText.text = item.amount.ToString();
                stackText.enabled = true;
            }
            this.item = item;
            GetComponent<TooltipHandler>().SetText(item.data.Name, item.data.Description);
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
        item = null;
        image.enabled = false;
        stackText.text = null;
        stackText.enabled = false;
    }

    public Item GetItem()
    {
        return item;
    }
}
