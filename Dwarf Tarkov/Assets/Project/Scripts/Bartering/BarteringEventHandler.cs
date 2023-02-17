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
        EventChannels.BarteringEvents.OnPlayerSellsItem += AddCredits;
    }

    private void OnDestroy()
    {
        EventChannels.BarteringEvents.OnPlayerSellsItem -= AddCredits;
    }

    public void AddCredits(int amount)
    {
        EventChannels.ItemEvents.OnAddItemToInventory?.Invoke(credit, amount);
    }
}
