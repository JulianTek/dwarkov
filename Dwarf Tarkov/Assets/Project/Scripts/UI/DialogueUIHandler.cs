using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class DialogueUIHandler : MonoBehaviour
{
    private Queue<DialogueLine> sentences = new Queue<DialogueLine>();

    [SerializeField]
    [HorizontalGroup("fields", LabelWidth = 60)]
    [BoxGroup("fields/TextMesh", LabelText = "Text fields")]
    private TextMeshProUGUI nameBox;
    [SerializeField]
    [BoxGroup("fields/TextMesh", LabelText = "Text fields")]
    private TextMeshProUGUI dialogueText;
    [BoxGroup("fields/Buttons", LabelText = "Buttons")]
    [SerializeField]
    private Button continueButton, confirmChoiceButton, denyChoiceButton;
    private void Start()
    {
        EventChannels.UIEvents.OnInitiateDialogue += InitiateDialogue;
        EventChannels.NPCEvents.OnStartDialogue += SetName;
    }

    void InitiateDialogue(DialogueLine[] lines)
    {
        if (sentences.Count > 0)
            sentences.Clear();
        foreach (DialogueLine line in lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        dialogueText.text = sentences.Dequeue().Sentence;
    }

    private void EndDialogue()
    {
        EventChannels.UIEvents.OnEndDialogue?.Invoke();
    }

    private void SetName(string name)
    {
        nameBox.text = name;
    }
}
