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

    private void Start()
    {
        questInventory = GetComponent<NPCQuestInventory>();
        EventChannels.UIEvents.OnPlayerCompleteQuest += PlayerCompletedQuest;
    }

    private void OnDisable()
    {
        EventChannels.UIEvents.OnPlayerCompleteQuest -= PlayerCompletedQuest;
    }

    public void InitDialogue()
    {
        EventChannels.NPCEvents.OnStartDialogue?.Invoke(npcName);
        if (questInventory.quests.Count > 0)
        {
            InvokeDialogue(questInventory.NextQuest.IntroText);
        }
    }

    public void PlayerCompletedQuest(Quest quest)
    {
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
}
