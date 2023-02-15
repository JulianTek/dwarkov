using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeepInventorySlot : MonoBehaviour
{
    private ShopKeepItem item;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetSlot(ShopKeepItem item)
    {
        this.item = item;
        GetComponent<SpriteRenderer>().sprite = item.Data.Sprite;
    }
}
