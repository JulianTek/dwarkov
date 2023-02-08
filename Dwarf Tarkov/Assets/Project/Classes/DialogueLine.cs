using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class DialogueLine
{
    [HorizontalGroup("horizontal")]
    [TextArea(3, 10)]
    public string Sentence;
    [HorizontalGroup("horizontal")]
    public bool IsChoice;

    public DialogueLine(string sentence, bool isChoice)
    {
        Sentence = sentence;
        IsChoice = isChoice;
    }
}
