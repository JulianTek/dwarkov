using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvents
{
    public delegate void GameplayEvent();
    public delegate void QuestEvent(Quest quest);
    public delegate List<Quest> QuestListEvent();
    public GameplayEvent OnPlayerResumesGame;

    public GameplayEvent OnPlayerExtracted;

    public QuestListEvent OnGetPlayerQuests;
    public QuestEvent OnCompleteQuest;
}
