using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEvents
{
    public delegate List<ItemDTO> ItemDTOEvent();
    public delegate int IntEvent();
    public delegate List<Quest> QuestEvent();

    public ItemDTOEvent OnGetPlayerInventory;
    public ItemDTOEvent OnGetOutpostInventory;

    public IntEvent OnGetPlayerLevel;
    public IntEvent OnGetPlayerExperience;

    public QuestEvent OnGetUnlockedQuests;
    public QuestEvent OnGetAllQuests;
    public QuestEvent OnGetCompletedQuests;
    public QuestEvent OnGetPlayerQuests;
}
