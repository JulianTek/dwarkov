using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents
{
    public delegate void HealthbarEvent(float vallue);
    public delegate void DialogueInitEvent(DialogueLine[] line);
    public delegate void DialogueEvent(DialogueLine line);
    public delegate void UIEvent();
    public delegate void QuestEvent(Quest quest);
    public delegate void ShopkeepInventoryEvent(ShopkeepInventory inventory);

    public HealthbarEvent OnUpdateHealthbar;

    public DialogueInitEvent OnInitiateDialogue;
    public DialogueEvent OnContinueDialogue;
    public UIEvent OnEncounterDialogueChoice;
    public UIEvent OnEndDialogue;

    public UIEvent OnPlayerPressConfirm;
    public UIEvent OnPlayerPressDeny;

    public QuestEvent OnPlayerCompleteQuest;

    public ShopkeepInventoryEvent OnOpenBarteringMenu;
    public UIEvent OnCloseBarteringMenu;
}
