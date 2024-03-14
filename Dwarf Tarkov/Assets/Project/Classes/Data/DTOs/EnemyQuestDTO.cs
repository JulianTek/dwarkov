using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Data
{
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
            Rewards = new List<ItemDTO>();
            Rewards.AddRange(quest.Rewards.Select(dto => new ItemDTO()));
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

