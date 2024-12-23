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

    private RectTransform rectTransform;

    [SerializeField]
    private int charWrapLimit;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

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
        Vector2 mousePos = Input.mousePosition;

        float posX = mousePos.x / Screen.width;
        float posY = mousePos.y / Screen.height;

        if (mousePos.x > Screen.width / 2)
        {
            posX += 0.5f;
        }
        else // Mouse is on the left side
        {
            posX -= 0.5f;
        }

        rectTransform.pivot = new Vector2(posX, posY);
        transform.position = mousePos;
    }
}
