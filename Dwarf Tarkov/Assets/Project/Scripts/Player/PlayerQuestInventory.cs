using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using System.Linq;

public class PlayerQuestInventory : MonoBehaviour
{
    private List<Quest> quests = new List<Quest>();
    // Start is called before the first frame update
    void Start()
    {
        SaveData data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (data != null && data.PlayerQuests != null)
            quests = ConvertDTOsToQuests(data.Quests);
        else
            quests = new List<Quest>();


        EventChannels.NPCEvents.OnPlayerAcceptQuest += AddQuestToInventory;
        EventChannels.NPCEvents.OnPlayerFinishQuest += FinishQuest;

        EventChannels.NPCEvents.OnStartDialogue += CheckIfQuestsCompleted;
        EventChannels.UIEvents.OnPlayerCompleteQuest += CompleteQuest;

        EventChannels.GameplayEvents.OnGetPlayerQuests += GetQuests;

        EventChannels.DataEvents.OnGetPlayerQuests += GetQuests;

        EventChannels.EnemyEvents.OnEnemyDeathWithName += TrackEnemyKills;
    }

    private void OnDisable()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest -= AddQuestToInventory;
        EventChannels.NPCEvents.OnPlayerFinishQuest -= FinishQuest;

        EventChannels.NPCEvents.OnStartDialogue -= CheckIfQuestsCompleted;
        EventChannels.UIEvents.OnPlayerCompleteQuest -= CompleteQuest;

        EventChannels.GameplayEvents.OnGetPlayerQuests -= GetQuests;

        EventChannels.DataEvents.OnGetPlayerQuests -= GetQuests;

        EventChannels.EnemyEvents.OnEnemyDeathWithName -= TrackEnemyKills;
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
                    else if (quest is EnemyQuest)
                        if (CheckIfEnemyQuestCompleted(quest as EnemyQuest))
                            EventChannels.UIEvents.OnPlayerCompleteQuest?.Invoke(quest);
            }
        }
    }

    private bool CheckIfItemQuestCompleted(ItemQuest quest)
    {
        return (bool)EventChannels.ItemEvents.OnCheckIfItemQuestCompleted?.Invoke(quest.RequiredItems);
    }

    private bool CheckIfEnemyQuestCompleted(EnemyQuest quest)
    {
        return quest.AmountKilled >= quest.AmountToKill;
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

    private List<Quest> ConvertDTOsToQuests(List<QuestDTO> dtos)
    {
        List<Quest> quests = new List<Quest>();
        foreach (QuestDTO questDTO in dtos)
        {
            if (questDTO is EnemyQuestDTO)
                quests.Add(new EnemyQuest(questDTO as EnemyQuestDTO));
            else if (questDTO is ItemQuestDTO)
                quests.Add(new ItemQuest(questDTO as ItemQuestDTO));
        }
        return quests;
    }

    private void TrackEnemyKills(string enemyName)
    {
        foreach (Quest quest in quests)
        {
            if (quest is EnemyQuest)
            {
                var questToCheck = quest as EnemyQuest;
                if (questToCheck.EnemyToKill == enemyName)
                {
                    questToCheck.IncreaseAmountKilled();
                }
            }
        }
    }
} 
