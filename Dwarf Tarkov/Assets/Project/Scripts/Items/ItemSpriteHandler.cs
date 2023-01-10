using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ItemData data;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        data = GetComponentInParent<ItemHandler>().GetItemData();
        spriteRenderer.sprite = data.Sprite;
    }
}
