using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverDialogueHandler : BaseDialogueHandler
{
    [SerializeField]
    private NPCQuestInventory inventory;
    [SerializeField]
    private Button completeQuestButton;

    private NPCQuestInventory questInventory;
    private bool playerCompletedQuest;

    private new void Start()
    {
        base.Start();
        questInventory = GetComponentInParent<NPCQuestInventory>();
        EventChannels.UIEvents.OnPlayerCompleteQuest += PlayerCompletedQuest;
        EventChannels.NPCEvents.OnPlayerFinishQuest += ResetQuestBool;
    }

    private void OnDisable()
    {
        EventChannels.UIEvents.OnPlayerCompleteQuest -= PlayerCompletedQuest;
        EventChannels.NPCEvents.OnPlayerFinishQuest -= ResetQuestBool;
    }

    public override void RunInteraction()
    {
        InvokeDialogue(new DialogueLine[] {
            idleLine
        });
    }

    public new void InvokeDialogue(DialogueLine[] lines)
    {
        // calling this outside the base since this also checks if a quest is completed
        EventChannels.NPCEvents.OnStartDialogue?.Invoke(npcName);
        if (questInventory.unlockedQuests.Count > 0 && !playerCompletedQuest)
        {
            base.InvokeDialogue(questInventory.NextQuest.IntroText);
        }
        else if (!playerCompletedQuest)
        {
            base.InvokeDialogue(lines);
        }
    }

    public void PlayerCompletedQuest(Quest quest)
    {
        playerCompletedQuest = true;
        completeQuestButton.gameObject.SetActive(true);
        base.InvokeDialogue(new DialogueLine[]
        {
            new DialogueLine(quest.CompletionText, false)
        });
    }

    public void ResetQuestBool(Quest quest)
    {
        playerCompletedQuest = false;
    }
}
