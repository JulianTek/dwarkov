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
            Item itemToSplit = slotHandler.GetItem();
            int amount = itemToSplit.amount;
            int split = amount / 2;
            EventChannels.ItemEvents.OnRemoveFromStack?.Invoke(itemToSplit.data, itemToSplit.amount, split);
            EventChannels.ItemEvents.OnCreateStack?.Invoke(slotHandler.GetItem().data, split);
            EventChannels.ItemEvents.OnUpdateInventory?.Invoke();
        }
    }
}
