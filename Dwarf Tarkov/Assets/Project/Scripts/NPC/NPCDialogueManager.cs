using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCDialogueManager : MonoBehaviour
{
    [SerializeField]
    private string npcName;

    [SerializeField]
    private DialogueLine idleLine;

    private NPCQuestInventory questInventory;
    [SerializeField]
    private Button completeQuestButton;

    private bool playerCompletedQuest;

    private void Start()
    {
        questInventory = GetComponent<NPCQuestInventory>();
        EventChannels.UIEvents.OnPlayerCompleteQuest += PlayerCompletedQuest;
        EventChannels.NPCEvents.OnPlayerFinishQuest += ResetQuestBool;
    }

    private void OnDisable()
    {
        EventChannels.UIEvents.OnPlayerCompleteQuest -= PlayerCompletedQuest;
        EventChannels.NPCEvents.OnPlayerFinishQuest -= ResetQuestBool;
    }

    public void InitDialogue()
    {
        EventChannels.NPCEvents.OnStartDialogue?.Invoke(npcName);
        if (questInventory.unlockedQuests.Count > 0 && !playerCompletedQuest)
        {
            InvokeDialogue(questInventory.NextQuest.IntroText);
        }
        else if (!playerCompletedQuest)
        {
            InvokeDialogue(new DialogueLine[]
            {
                idleLine
            }) ;
        } 
    }

    public void PlayerCompletedQuest(Quest quest)
    {
        playerCompletedQuest = true;
        completeQuestButton.gameObject.SetActive(true);
        InvokeDialogue(new DialogueLine[]
        {
            new DialogueLine(quest.CompletionText, false)
        });
    }

    public void InvokeDialogue(DialogueLine[] lines)
    {
        EventChannels.UIEvents.OnInitiateDialogue?.Invoke(lines);
    }

    public void ResetQuestBool(Quest quest)
    {
        playerCompletedQuest = false;
    }
}
