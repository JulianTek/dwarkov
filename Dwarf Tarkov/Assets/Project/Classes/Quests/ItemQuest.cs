using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

// Item quests require the player to retrieve 
[System.Serializable]
public class ItemQuest : Quest
{
    public ItemQuest(ItemQuestDTO dto)
    {
        Name = dto.Name;
        QuestGiverName = dto.QuestGiverName;
        // add rewards
        UnlockLevel = dto.UnlockLevel;
        ExpReward = dto.ExpReward;
        IntroText = dto.IntroText;
        CompletionText = dto.CompletionText;
        //Add required items
    }
    // please use "amount" to denote how many of an item the player should retrieve
    public List<Item> RequiredItems = new List<Item>();
}
