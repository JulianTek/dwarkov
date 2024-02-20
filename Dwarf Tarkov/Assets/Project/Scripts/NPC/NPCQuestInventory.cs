using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestInventory : MonoBehaviour
{
    //put all quests an npc can have in this list
    [SerializeReference]
    public List<Quest> quests = new List<Quest>();
    public List<Quest> unlockedQuests { get; private set; } = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();
    public Quest NextQuest;

    private void Start()
    {
        RefreshQuests();
        NextQuest = unlockedQuests[0];
        EventChannels.UIEvents.OnPlayerPressConfirm += ConfirmQuest;
        EventChannels.GameplayEvents.OnCompleteQuest += CompleteQuest;

        EventChannels.DataEvents.OnGetAllQuests += GetAllQuests;
        EventChannels.DataEvents.OnGetUnlockedQuests += GetUnlockedQuests;
        EventChannels.DataEvents.OnGetCompletedQuests += GetCompletedQuests;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnPlayerPressConfirm -= ConfirmQuest;
        EventChannels.GameplayEvents.OnCompleteQuest -= CompleteQuest;

        EventChannels.DataEvents.OnGetAllQuests -= GetAllQuests;
        EventChannels.DataEvents.OnGetUnlockedQuests -= GetUnlockedQuests;
        EventChannels.DataEvents.OnGetCompletedQuests -= GetCompletedQuests;
    }

    void ConfirmQuest()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest?.Invoke(NextQuest);
        unlockedQuests.Remove(NextQuest);
        if (unlockedQuests.Count > 0)
            NextQuest = unlockedQuests[0];
        else
        {
            NextQuest = null;
        }
    }

    public void RefreshQuests()
    {
        int playerLevel = (int)EventChannels.PlayerEvents.OnGetPlayerLevel?.Invoke();
        foreach (Quest quest in quests)
        {
            if (playerLevel >= quest.UnlockLevel && !unlockedQuests.Contains(quest) && !completedQuests.Contains(quest) && (bool)!EventChannels.GameplayEvents.OnGetPlayerQuests?.Invoke().Contains(quest))
                unlockedQuests.Add(quest);
        }
        NextQuest = unlockedQuests[0];
    }

    void CompleteQuest(Quest quest)
    {
        completedQuests.Add(quest);
    }

    public List<Quest> GetCompletedQuests()
    {
        return completedQuests;
    }

    public List<Quest> GetAllQuests()
    {
        return quests;
    }

    public List<Quest> GetUnlockedQuests()
    {
        return unlockedQuests;
    }
}
