using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEvents
{
    public delegate void NPCNameEvent(string name);
    public delegate void QuestEvent(Quest quest);

    public NPCNameEvent OnStartDialogue;

    public QuestEvent OnPlayerAcceptQuest;
    public QuestEvent OnPlayerFinishQuest;
}
