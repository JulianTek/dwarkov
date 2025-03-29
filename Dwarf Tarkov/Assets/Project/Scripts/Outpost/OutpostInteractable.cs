using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostInteractable : MonoBehaviour, IOutpostInteractable
{
    private bool playerInTrigger = false;
    public void Interact()
    {
        if (playerInTrigger)
            RunInteraction();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>() != null)
            playerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>() != null && playerInTrigger)
            playerInTrigger = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void RunInteraction() {}
}
