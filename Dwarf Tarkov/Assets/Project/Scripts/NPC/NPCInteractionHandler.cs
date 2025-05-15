using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionHandler : MonoBehaviour
{
    private bool playerIsInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerInputEvents.OnInteract += InteractWithNPC;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            playerIsInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            playerIsInTrigger = false;
        }
    }

    void InteractWithNPC()
    {
        if (playerIsInTrigger)
        {
            var questGiverDialogMan = GetComponentInParent<QuestGiverDialogueManager>();
            if (questGiverDialogMan != null)
                questGiverDialogMan.InitDialogue();
            else
            {
                GetComponentInParent<HealerDialogueManager>().InitDialogue();
            }
        }
    }

}
