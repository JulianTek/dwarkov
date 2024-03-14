using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using EventSystem;

[System.Serializable]
public class Quest
{
    public Quest(QuestDTO questDTO)
    {
        Name= questDTO.Name;
        QuestGiverName= questDTO.QuestGiverName;
        MenuDescription= questDTO.MenuDescription;
        // add rewards
        UnlockLevel= questDTO.UnlockLevel;
        ExpReward= questDTO.ExpReward;
        IntroText = questDTO.IntroText;
        CompletionText = questDTO.CompletionText;
    }

    public Quest()
    {

    }

    public string Name;
    public string QuestGiverName;
    [TextArea(3, 10)]
    public string MenuDescription;
    public List<Item> Rewards;
    public int UnlockLevel;
    public float ExpReward;
    public DialogueLine[] IntroText;
    [TextArea(3, 10)]
    public string CompletionText;
}
