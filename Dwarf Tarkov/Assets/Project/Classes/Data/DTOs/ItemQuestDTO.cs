using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Data
{
    [System.Serializable]
    public class ItemQuestDTO : QuestDTO
    {

        public List<ItemDTO> RequiredItems;

        public ItemQuestDTO(ItemQuest quest)
        {
            Name = quest.Name;
            QuestGiverName = quest.QuestGiverName;
            MenuDescription = quest.MenuDescription;
            Rewards = DTOConverter.ConvertItemListToDTOList(quest.Rewards);
            UnlockLevel = quest.UnlockLevel;
            ExpReward = quest.ExpReward;
            IntroText = quest.IntroText;
            CompletionText = quest.CompletionText;
            RequiredItems = DTOConverter.ConvertItemListToDTOList(quest.RequiredItems);
        }
    }
}

