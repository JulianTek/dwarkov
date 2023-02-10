using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestInventory : MonoBehaviour
{
    private List<Quest> quests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest += AddQuestToInventory;
        EventChannels.NPCEvents.OnPlayerFinishQuest += FinishQuest;

        EventChannels.NPCEvents.OnStartDialogue += CheckIfQuestsCompleted;
    }

    private void OnDisable()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest -= AddQuestToInventory;
        EventChannels.NPCEvents.OnPlayerFinishQuest -= FinishQuest;

        EventChannels.NPCEvents.OnStartDialogue -= CheckIfQuestsCompleted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddQuestToInventory(Quest quest)
    {
        quests.Add(quest);
        Debug.Log(quest.Name);
    }

    private void FinishQuest(Quest quest)
    {
        quests.Remove(quest);
    }
    
    private void CheckIfQuestsCompleted(string name)
    {
        foreach (Quest quest in quests)
        {
            if (quest is ItemQuest)
                if (CheckIfItemQuestCompleted(quest as ItemQuest))
                    MarkQuestComplete(quest);
        }

        foreach (Quest quest in completedQuests)
        {
            if (quest is ItemQuest)
                if (!CheckIfItemQuestCompleted(quest as ItemQuest))
                    MarkQuestIncomplete(quest);
        }
    }

    private bool CheckIfItemQuestCompleted(ItemQuest quest)
    {
        return (bool)EventChannels.ItemEvents.OnCheckIfItemQuestCompleted?.Invoke(quest.RequiredItems);
    }

    private void MarkQuestComplete(Quest quest)
    {
        quests.Remove(quest);
        completedQuests.Add(quest);
    }

    private void MarkQuestIncomplete(Quest quest)
    {
        quests.Add(quest);
        completedQuests.Remove(quest);
    }
} 
