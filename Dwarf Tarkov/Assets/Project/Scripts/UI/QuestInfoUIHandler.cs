using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventSystem;

public class QuestInfoUIHandler : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI contentText;

    private void Start()
    {
        EventChannels.UIEvents.OnSetQuestInfoTitle += SetTitle;
        EventChannels.UIEvents.OnSetQuestInfoDescription += SetContent;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnSetQuestInfoTitle -= SetTitle;
        EventChannels.UIEvents.OnSetQuestInfoDescription -= SetContent;
    }

    private void SetTitle(string text)
    {
        titleText.SetText(text);
    }

    private void SetContent(string text)
    {
        contentText.SetText(text);
    }
}
