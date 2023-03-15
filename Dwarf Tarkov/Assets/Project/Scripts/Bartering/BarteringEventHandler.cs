using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarteringEventHandler : MonoBehaviour
{
    [SerializeField]
    private ItemData credit;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.BarteringEvents.OnPlayerSellsItems += AddCredits;
    }

    private void OnDestroy()
    {
        EventChannels.BarteringEvents.OnPlayerSellsItems -= AddCredits;
    }

    public void AddCredits(int amount)
    {
        EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(credit, amount);
    }
}
