using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    public string desc;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(header, desc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    public void SetText(string headerText, string descText)
    {
        header = headerText;
        desc = descText;
    }
}
