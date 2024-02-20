using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class MineralHandler : MonoBehaviour
{
    public Mineable materialData;
    private bool playerIsInTrigger;

    [SerializeField]
    private int amountMineable;
    [SerializeField]
    private int amountPerMine;

    [SerializeField]
    private float eventRadius;

    private OreSpriteHandler spriteHandler;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerMine += Mine;
        spriteHandler = GetComponentInChildren<OreSpriteHandler>();
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerMine -= Mine;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            SetPlayerInTrigger(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            SetPlayerInTrigger(false);
        }
    }

    private void SetPlayerInTrigger(bool isInTrigger)
    {
        playerIsInTrigger = isInTrigger;
    }

    void Mine()
    {
        if (playerIsInTrigger)
        {
            EventChannels.ItemEvents.OnAddItemToInventory(materialData.ItemYielded, amountPerMine);
            amountMineable--;
            spriteHandler.SetSprite(3-amountMineable);
            FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerEvents/MiningEvents/Mine_Pick");
            Physics2D.CircleCast(transform.position, eventRadius, Vector2.zero, 0f);
            EventChannels.PlayerEvents.OnExperienceGained?.Invoke(materialData.ExperienceGainedOnMine);
            if (amountMineable == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
