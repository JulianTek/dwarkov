using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletionUIHandler : MonoBehaviour
{
    private Quest quest;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.UIEvents.OnPlayerCompleteQuest += SetQuest;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnPlayerCompleteQuest -= SetQuest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetQuest(Quest quest)
    {
        this.quest = quest;
    }

    public void CompleteQuest()
    {
        EventChannels.NPCEvents.OnPlayerFinishQuest?.Invoke(quest);
        gameObject.SetActive(false);
        EventChannels.UIEvents.OnEndDialogue?.Invoke();
    }
}
