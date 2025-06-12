using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class HealerDialogueHandler : BaseDialogueHandler
{
    [SerializeField]
    private GameObject healButton;

    private int costToHeal;
    private float healthToHeal;

    void Awake()
    {
        EventChannels.UIEvents.OnGetHealingAmount += GetHealAmount;
        EventChannels.UIEvents.OnGetHealingCost += GetCost;
        EventChannels.UIEvents.OnEndDialogue += HideHealButton;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnGetHealingAmount -= GetHealAmount;
        EventChannels.UIEvents.OnGetHealingCost -= GetCost;
        EventChannels.UIEvents.OnEndDialogue -= HideHealButton;
    }

    public override void RunInteraction()
    {
        float playerHealth = (float)EventChannels.DataEvents.OnGetPlayerHealth?.Invoke();
        if (playerHealth < 100)
        {
            healButton.SetActive(true);
            int creditAmount = (int)EventChannels.ItemEvents.OnGetItemCount?.Invoke(EventChannels.DatabaseEvents.OnGetItemData?.Invoke("Credit"));
            healthToHeal = Mathf.Clamp(creditAmount / 3, 0, 100 - playerHealth);
            costToHeal = (int)healthToHeal * 3;
            if (healthToHeal + playerHealth >= 100)
            {
                InvokeDialogue(new DialogueLine[]
                {
                new DialogueLine($"Oh goodness! You need healing! I can offer you my services in exchange for some credits. " +
                $"It'll only run you {costToHeal} credits to heal you back up to full!", false)
                });
            }
            else
            {
                InvokeDialogue(new DialogueLine[]
                {
                new DialogueLine($"Oh goodness! You need healing! I can offer you my services in exchange for some credits. " +
                $"It'll only run you {costToHeal} credits, but I can't heal you up to full health", false)
                });
            }
        }
        else
        {
            InvokeDialogue(new DialogueLine[]
            {
                new DialogueLine("Come to me if you need to be healed", false)
            });
        }
    }

    public int GetCost()
    {
        return costToHeal;
    }

    public float GetHealAmount()
    {
        return healthToHeal;
    }

    private void HideHealButton()
    {
        healButton.SetActive(false);
    }
}
