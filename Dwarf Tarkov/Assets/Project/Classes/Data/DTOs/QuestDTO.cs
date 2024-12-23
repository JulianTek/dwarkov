using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Data
{
    [System.Serializable]
    public class QuestDTO
    {
        public QuestDTO(Quest quest)
        {
            Name = quest.Name;
            QuestGiverName = quest.QuestGiverName;
            MenuDescription = quest.MenuDescription;
            Rewards = new List<ItemDTO>();
            Rewards.AddRange(DTOConverter.ConvertItemListToDTOList(quest.Rewards));
            UnlockLevel = quest.UnlockLevel;
            ExpReward = quest.ExpReward;
            IntroText = quest.IntroText;
            CompletionText = quest.CompletionText;
        }

        public QuestDTO()
        {

        }

        public string Name;
        public string QuestGiverName;
        public string MenuDescription;
        public List<ItemDTO> Rewards;
        public int UnlockLevel;
        public float ExpReward;
        public DialogueLine[] IntroText;
        public string CompletionText;
    }
}

