using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostEvents
{
    public delegate void ItemEvent(Item item);
    public delegate void OutpostEvent();

    public ItemEvent OnAddItemToOutpostInventory;
    public ItemEvent OnRemoveItemFromOutpostInventory;

    public OutpostEvent OnShowOutpostInventory;
    public OutpostEvent OnHideOutpostInventory;

    public OutpostEvent OnEnterOutpost;
}
