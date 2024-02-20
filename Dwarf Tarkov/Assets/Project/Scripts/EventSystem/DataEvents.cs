using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class DataEvents
{
    public delegate List<ItemDTO> ItemDTOEvent();
    public delegate int IntEvent();
    public delegate List<Quest> QuestEvent();
    public delegate void SetIntEvent(int number);
    public delegate SaveData DataEvent();

    public ItemDTOEvent OnGetPlayerInventory;
    public ItemDTOEvent OnGetOutpostInventory;

    public IntEvent OnGetPlayerLevel;
    public IntEvent OnGetPlayerExperience;

    public QuestEvent OnGetUnlockedQuests;
    public QuestEvent OnGetAllQuests;
    public QuestEvent OnGetCompletedQuests;
    public QuestEvent OnGetPlayerQuests;

    public SetIntEvent OnSetSaveSlotNumber;

    public DataEvent OnGetSaveData;
}
