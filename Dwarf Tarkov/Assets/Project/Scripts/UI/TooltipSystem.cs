using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    public TooltipInstance tooltip;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        instance = this;
        tooltip.gameObject.SetActive(false);
    }

    public static void Show(string header, string desc)
    {
        instance.tooltip.gameObject.SetActive(true);
        instance.tooltip.SetText(header, desc);
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
