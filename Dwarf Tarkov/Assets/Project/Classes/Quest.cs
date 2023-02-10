using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string Name;
    public List<Item> Rewards;
    public int UnlockLevel;
    public float ExpReward;
}
