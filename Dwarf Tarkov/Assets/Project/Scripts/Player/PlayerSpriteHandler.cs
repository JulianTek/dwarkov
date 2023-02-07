using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventChannels.ExtractionEvents.OnExtractionTimerFinished += HideSprite;
        EventChannels.OutpostEvents.OnEnterOutpost += ShowSprite;
    }

    private void OnDestroy()
    {
        EventChannels.ExtractionEvents.OnExtractionTimerFinished -= HideSprite;
        EventChannels.OutpostEvents.OnEnterOutpost -= ShowSprite;
    }

    void HideSprite()
    {
        spriteRenderer.enabled = false;
    }

    void ShowSprite()
    {
        transform.parent.position = Vector3.zero;
        spriteRenderer.enabled = true;
    }
}
