using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipInstance : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headerText;
    [SerializeField]
    private TextMeshProUGUI descText;
    [SerializeField]
    private LayoutElement layoutElement;

    [SerializeField]
    private int charWrapLimit;

    public void SetText(string headerText, string descText)
    {
        this.headerText.SetText(headerText);
        this.descText.SetText(descText);
    }

    private void Update()
    {
        int headerLength = headerText.text.Length;
        int descLength = descText.text.Length;

        layoutElement.enabled = (headerLength > charWrapLimit || descLength > charWrapLimit) ? true : false;
    }
}
