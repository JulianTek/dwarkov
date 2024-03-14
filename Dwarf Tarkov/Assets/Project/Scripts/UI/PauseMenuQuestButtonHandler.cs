using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuQuestButtonHandler : MonoBehaviour
{
    private Quest quest;
    public void SetQuest(Quest quest)
    {
        this.quest = quest;
    }

    public Quest GetQuest()
    {
        return quest;
    }

    public void SetQuestInfo()
    {
        EventChannels.UIEvents.OnSetQuestInfoTitle?.Invoke(quest.Name);
        EventChannels.UIEvents.OnSetQuestInfoDescription?.Invoke(quest.MenuDescription);
    }
}
