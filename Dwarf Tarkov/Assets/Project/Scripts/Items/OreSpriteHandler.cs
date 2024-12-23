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

    public void SetSprite(int amountMined)
    {
        switch (amountMined)
        {
            case 1:
                spriteRenderer.sprite = mineralHandler.materialData.Stage2Sprite;
                break;
            case 2:
                spriteRenderer.sprite = mineralHandler.materialData.Stage3Sprite;
                break;
            default:
                break;
        }
    }
}
