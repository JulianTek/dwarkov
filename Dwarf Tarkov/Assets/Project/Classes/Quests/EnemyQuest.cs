using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
[System.Serializable]
public class EnemyQuest : Quest
{
    public EnemyQuest(EnemyQuestDTO dto)
    {
        Name = dto.Name;
        QuestGiverName = dto.QuestGiverName;
        // add rewards
        UnlockLevel = dto.UnlockLevel;
        ExpReward = dto.ExpReward;
        IntroText = dto.IntroText;
        CompletionText = dto.CompletionText;
        EnemyToKill = dto.EnemyToKill;
        AmountToKill = dto.AmountToKill;
        AmountKilled = dto.AmountKilled;
    }

    public EnemyQuest()
    {

    }

    public string EnemyToKill;
    public int AmountToKill;
    public int AmountKilled { get; private set; }

    public void IncreaseAmountKilled()
    {
        AmountKilled++;
    }
}
