using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class BaseDialogueHandler : OutpostInteractable
{
    [SerializeField]
    protected string npcName;

    [SerializeField]
    protected DialogueLine idleLine;

    public override void RunInteraction()
    {
        InvokeDialogue(new DialogueLine[] {
            idleLine
        });
    }
    public void InvokeDialogue(DialogueLine[] lines)
    {
        EventChannels.NPCEvents.OnStartDialogue?.Invoke(npcName);
        EventChannels.UIEvents.OnInitiateDialogue?.Invoke(lines);
    }
}
