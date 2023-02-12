using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestInventory : MonoBehaviour
{
    [SerializeReference]
    public List<Quest> quests = new List<Quest>();
    public Quest NextQuest;

    private void Start()
    {
        NextQuest = quests[0];
        EventChannels.UIEvents.OnPlayerPressConfirm += ConfirmQuest;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnPlayerPressConfirm -= ConfirmQuest;
    }

    void ConfirmQuest()
    {
        EventChannels.NPCEvents.OnPlayerAcceptQuest?.Invoke(NextQuest);
        quests.Remove(NextQuest);
        if (quests.Count > 0)
            NextQuest = quests[0];
    }
}
