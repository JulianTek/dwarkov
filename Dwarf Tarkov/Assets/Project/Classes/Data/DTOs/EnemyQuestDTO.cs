using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Data
{
    [System.Serializable]
    public class EnemyQuestDTO : QuestDTO
    {
        public string EnemyToKill;
        public int AmountToKill;
        public int AmountKilled;

        public EnemyQuestDTO(EnemyQuest quest)
        {
            Name = quest.Name;
            QuestGiverName = quest.QuestGiverName;
            MenuDescription = quest.MenuDescription;
            Rewards = DTOConverter.ConvertItemListToDTOList(quest.Rewards);
            UnlockLevel = quest.UnlockLevel;
            ExpReward = quest.ExpReward;
            IntroText = quest.IntroText;
            CompletionText = quest.CompletionText;
            EnemyToKill = quest.EnemyToKill;
            AmountToKill = quest.AmountToKill;
            AmountKilled = quest.AmountKilled;
        }
    }
}

