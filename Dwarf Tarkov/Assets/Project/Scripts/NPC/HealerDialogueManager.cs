using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class HealerDialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject healButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitDialogue()
    {
        healButton.SetActive(true);
        float playerHealth = (float)EventChannels.DataEvents.OnGetPlayerHealth?.Invoke();
        if (playerHealth < 100)
        {
            InvokeDialogue(new DialogueLine[]
            {
                new DialogueLine($"Oh goodness! You need healing! I can offer you my services in exchange for some credits. It'll only run you {(100 - playerHealth) * 3} credits to heal you back up to full!", false),
            });
        }
    }

    public void InvokeDialogue(DialogueLine[] lines)
    {
        EventChannels.UIEvents.OnInitiateDialogue?.Invoke(lines);
    }
}
