using System.Collections;
using System.Collections.Generic;
using EventSystem;
using TMPro;
using UnityEngine;

public class BiomeTitleTextHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.OutpostEvents.OnSetTitle += SetTitle;
        SetTitle("Surface Depths");
    }

    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnSetTitle -= SetTitle;
    }

    void SetTitle(string text)
    {
        titleText.text = text;
    }
}
