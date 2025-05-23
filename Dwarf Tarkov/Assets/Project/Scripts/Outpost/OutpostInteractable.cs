using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class OutpostInteractable : MonoBehaviour, IOutpostInteractable
{
    protected void Start()
    {
        EventChannels.PlayerInputEvents.OnInteract += Interact;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnInteract -= Interact;
    }

    protected bool playerInTrigger = false;
    public void Interact()
    {
        if (playerInTrigger)
            RunInteraction();

    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerInputHandler>())
            playerInTrigger = true;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerInputHandler>() && playerInTrigger)
            playerInTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void RunInteraction() {}
}
