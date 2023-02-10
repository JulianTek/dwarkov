using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestInventory : MonoBehaviour
{
    [SerializeField]
    private List<Quest> quests = new List<Quest>();
    private Quest NextQuest;

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
    }
}
