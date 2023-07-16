using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarteringEvents
{
    public delegate bool HasEnoughCreditsEvent(int credits);
    public delegate bool IsSpaceInSellboxEvent();
    public delegate void CreditsEvent(int credits);
    public delegate void ItemEvent(Item item);
    public delegate void SlotEvent(GameObject slot);
    public delegate void BarteringEvent();
    public delegate void ItemQuantityEvent(ItemData data, int amount);

    public HasEnoughCreditsEvent OnCheckIfPlayerHasEnoughCredits;

    public CreditsEvent OnPlayerBuysItem;
    public CreditsEvent OnPlayerSellsItems;

    public ItemEvent OnPlayerMovesItemToSellBox;
    public SlotEvent OnPlayerMovesItemFromSellBox;

    public IsSpaceInSellboxEvent OnPlayerTryToSellItem;

    public BarteringEvent OnPlayerHasInsufficientCredits;

    public ItemQuantityEvent OnPlayerMovesQuantityToSellbox;
}
