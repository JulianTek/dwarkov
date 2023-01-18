using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSpriteHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MineralHandler mineralHandler;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mineralHandler = GetComponentInParent<MineralHandler>();
        spriteRenderer.sprite = mineralHandler.materialData.Sprite;
    }   
}
