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
    [SerializeField]
    private TextMeshProUGUI progresstText;

    private void Start()
    {
        EventChannels.UIEvents.OnSetQuestInfoTitle += SetTitle;
        EventChannels.UIEvents.OnSetQuestInfoDescription += SetContent;
        EventChannels.UIEvents.OnSetQuestInfoProgress += SetProgress;
    }

    private void OnDestroy()
    {
        EventChannels.UIEvents.OnSetQuestInfoTitle -= SetTitle;
        EventChannels.UIEvents.OnSetQuestInfoDescription -= SetContent;
        EventChannels.UIEvents.OnSetQuestInfoProgress -= SetProgress;
    }

    private void SetTitle(string text)
    {
        titleText.SetText(text);
    }

    private void SetContent(string text)
    {
        contentText.SetText(text);
    }

    private void SetProgress(string text)
    {
        progresstText.SetText(text);
    }
}
