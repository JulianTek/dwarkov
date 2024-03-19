using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using EventSystem;

// Item quests require the player to retrieve 
[System.Serializable]
public class ItemQuest : Quest
{
    public ItemQuest(ItemQuestDTO dto)
    {
        Name = dto.Name;
        QuestGiverName = dto.QuestGiverName;
        MenuDescription = dto.MenuDescription;
        // add rewards
        UnlockLevel = dto.UnlockLevel;
        ExpReward = dto.ExpReward;
        IntroText = dto.IntroText;
        CompletionText = dto.CompletionText;
        //Add required items
    }

    public ItemQuest()
    {

    }
    // please use "amount" to denote how many of an item the player should retrieve
    public List<Item> RequiredItems = new List<Item>();

    public override string GetProgress()
    {
        string text = "Items collected:";
        foreach (Item item in RequiredItems)
        {
            text += $"\n{item.data.Name}: {(EventChannels.ItemEvents.OnGetItemCount?.Invoke(item.data) == -1 ? 0 : EventChannels.ItemEvents.OnGetItemCount?.Invoke(item.data))}/{item.amount}";
        }
        return text;
    }
}
