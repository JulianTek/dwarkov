using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item quests require the player to retrieve 
[System.Serializable]
public class ItemQuest : Quest
{
    // please use "amount" to denote how many of an item the player should retrieve
    public List<Item> RequiredItems = new List<Item>();
}
