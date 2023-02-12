using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string Name;
    public string QuestGiverName;
    public List<Item> Rewards;
    public int UnlockLevel;
    public float ExpReward;
    public DialogueLine[] IntroText;
    [TextArea(3, 10)]
    public string CompletionText;
}
