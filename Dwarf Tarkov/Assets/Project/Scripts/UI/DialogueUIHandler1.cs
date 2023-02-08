using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUIHandler1 : MonoBehaviour
{
    private void Start()
    {
        EventChannels.UIEvents.OnInitiateDialogue += ShowDialogueBox;
        EventChannels.UIEvents.OnEndDialogue += HideDialogueBox;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnInitiateDialogue -= ShowDialogueBox;
        EventChannels.UIEvents.OnEndDialogue -= HideDialogueBox;
    }
    void ShowDialogueBox(DialogueLine[] lines)
    {
        gameObject.SetActive(true);
    }

    void HideDialogueBox()
    {
        gameObject.SetActive(false);
    }
}
