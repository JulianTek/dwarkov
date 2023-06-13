using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class StackSplitHandler : MonoBehaviour
{
    private PlayerInventory inventory;
    private InventorySlotHandler slotHandler;
    private bool canStack;

    private void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        slotHandler = GetComponent<InventorySlotHandler>();

        EventChannels.PlayerInputEvents.OnToggleStackSplit += SetStacking;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnToggleStackSplit -= SetStacking;
    }

    private void SetStacking(bool stacking)
    {
        canStack = stacking;
    }

    public void SplitStack()
    {
        Debug.Log(canStack);
        if (canStack && inventory.CheckIfSlotFree())
        {
            int amount = inventory.GetAmountOfItem(slotHandler.GetItem());
            int split = amount / 2;
            EventChannels.ItemEvents.OnRemoveItemFromInventory?.Invoke(slotHandler.GetItem(), split);
            EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(slotHandler.GetItem(), split);
            EventChannels.ItemEvents.OnUpdateInventory?.Invoke();
        }
    }
}
