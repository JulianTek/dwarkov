using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestInventory : MonoBehaviour
{
    private List<Quest> quests = new List<Quest>();
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest += AddQuestToInventory;
        EventChannels.NPCEvents.OnPlayerFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest -= AddQuestToInventory;
        EventChannels.NPCEvents.OnPlayerFinishQuest -= FinishQuest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddQuestToInventory(Quest quest)
    {
        quests.Add(quest);
        Debug.Log(quest.Name);
    }

    private void FinishQuest(Quest quest)
    {
        quests.Remove(quest);
    }
}
