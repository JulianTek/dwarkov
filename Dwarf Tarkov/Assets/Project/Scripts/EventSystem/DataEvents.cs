using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class DataEvents
{
    public delegate List<Item> ItemEventvent();
    public delegate int IntEvent();
    public delegate int IntEventBoolParam(bool param);
    public delegate List<Quest> QuestEvent();
    public delegate void SetIntEvent(int number);
    public delegate SaveData DataEvent();
    public delegate void SaveDataEvent(SaveData saveData);
    public delegate WeaponData WeaponDataEvent();
    public delegate AmmoSubtype AmmoSubtypeEvent();
    public delegate float FloatEvent();

    public ItemEventvent OnGetPlayerInventory;
    public ItemEventvent OnGetOutpostInventory;

    public IntEvent OnGetPlayerLevel;
    public IntEvent OnGetPlayerExperience;

    public QuestEvent OnGetUnlockedQuests;
    public QuestEvent OnGetAllQuests;
    public QuestEvent OnGetCompletedQuests;
    public QuestEvent OnGetPlayerQuests;

    public SetIntEvent OnSetSaveSlotNumber;

    public DataEvent OnGetSaveData;

    public SaveDataEvent OnSetSaveData;

    public WeaponDataEvent OnGetPrimaryWeapon;
    public WeaponDataEvent OnGetSecondaryWeapon;

    public IntEventBoolParam OnGetAmountOfBullets;

    public AmmoSubtypeEvent OnGetCurrentSubtype;

    public FloatEvent OnGetPlayerHealth;
}
