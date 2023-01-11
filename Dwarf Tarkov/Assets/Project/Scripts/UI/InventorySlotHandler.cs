using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotHandler : MonoBehaviour
{
    private bool isTaken = false;
    [SerializeField]
    private Image image;

    private void Start()
    {
        image.enabled = false;
    }

    public void SetSlot(Item item)
    {
        if (item != null)
        {
            SetSprite(item.data.Sprite);
            SetIsTaken(true);
            image.enabled = true;
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
}
