using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    [SerializeField]
    private string npcName;

    [SerializeField]
    private DialogueLine[] lines;

    public void InitDialogue()
    {
        EventChannels.NPCEvents.OnStartDialogue?.Invoke(npcName);
        EventChannels.UIEvents.OnInitiateDialogue?.Invoke(lines);
    }
}
