using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents
{
    public delegate void HealthbarEvent(float vallue);
    public delegate void XPPointsEvent(int value);
    public delegate void DialogueInitEvent(DialogueLine[] line);
    public delegate void DialogueEvent(DialogueLine line);
    public delegate void UIEvent();
    public delegate void QuestEvent(Quest quest);
    public delegate void ShopkeepInventoryEvent(ShopkeepInventory inventory);
    public delegate void ItemEvent(ItemData item);
    public delegate void WeaponSlotEvent(bool isPrimary);
    public delegate void StringUIEvent(string str);
    public delegate int GetIntEWvent();
    public delegate float GetFloatEvent();

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

    public ItemEvent OnPlayerSelectsItemToBuy;

    public UIEvent OnShowAmmoTypes;
    public UIEvent OnHideAmmoTypes;
    public UIEvent OnShowNoAmmoTypes;
    public UIEvent OnHideNoAmmoTypes;

    public UIEvent OnShowSellSubmenu;
    public UIEvent OnHideSellSubmenu;
    public UIEvent OnShowBuySubmenu;
    public UIEvent OnHideBuySubmenu;

    public XPPointsEvent OnSetXPPoints;

    public WeaponSlotEvent OnSwitchWeaponSlotSide;

    public StringUIEvent OnSetQuestInfoTitle;
    public StringUIEvent OnSetQuestInfoDescription;
    public StringUIEvent OnSetQuestInfoProgress;

    public GetIntEWvent OnGetHealingCost;
    public GetFloatEvent OnGetHealingAmount;
}
