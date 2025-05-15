using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class HealingUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HealPlayer()
    {
        int cost = (int)EventChannels.UIEvents.OnGetHealingCost?.Invoke();
        float healAmount = (float)EventChannels.UIEvents.OnGetHealingAmount?.Invoke();

        EventChannels.ItemEvents.OnRemoveItemFromInventory?.Invoke(EventChannels.DatabaseEvents.OnGetItemData?.Invoke("Credit"), cost);
        EventChannels.PlayerEvents.OnPlayerHeal?.Invoke(healAmount, 0f, false);
        EventChannels.UIEvents.OnEndDialogue?.Invoke();
    }
}
