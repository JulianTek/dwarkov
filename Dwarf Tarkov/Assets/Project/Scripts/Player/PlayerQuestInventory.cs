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
        EventChannels.UIEvents.OnPlayerCompleteQuest += CompleteQuest;

        EventChannels.GameplayEvents.OnGetPlayerQuests += GetQuests;

        EventChannels.DataEvents.OnGetPlayerQuests += GetQuests;
    }

    private void OnDisable()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest -= AddQuestToInventory;
        EventChannels.NPCEvents.OnPlayerFinishQuest -= FinishQuest;

        EventChannels.NPCEvents.OnStartDialogue -= CheckIfQuestsCompleted;
        EventChannels.UIEvents.OnPlayerCompleteQuest -= CompleteQuest;

        EventChannels.GameplayEvents.OnGetPlayerQuests -= GetQuests;

        EventChannels.DataEvents.OnGetPlayerQuests -= GetQuests;
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
            if (quest.QuestGiverName == name)
            {
                if (quest is ItemQuest)
                    if (CheckIfItemQuestCompleted(quest as ItemQuest))
                        EventChannels.UIEvents.OnPlayerCompleteQuest?.Invoke(quest);
            }
        }
    }

    private bool CheckIfItemQuestCompleted(ItemQuest quest)
    {
        return (bool)EventChannels.ItemEvents.OnCheckIfItemQuestCompleted?.Invoke(quest.RequiredItems);
    }

    private void CompleteQuest(Quest quest)
    {
        if (quest is ItemQuest)
        {
            ItemQuest itemQuest = quest as ItemQuest;
            CompleteItemQuest(itemQuest.RequiredItems);
        }
        foreach (Item item in quest.Rewards)
        {
            EventChannels.ItemEvents.OnAddItemToInventory(item.data, item.amount);
        }
        EventChannels.GameplayEvents.OnCompleteQuest?.Invoke(quest);  
    }

    private void CompleteItemQuest(List<Item> items)
    {
        foreach (Item item in items)
        {
            EventChannels.ItemEvents.OnRemoveItemFromInventory?.Invoke(item.data, item.amount);
        }
    }

    private List<Quest> GetQuests()
    {
        return quests;
    }
} 
