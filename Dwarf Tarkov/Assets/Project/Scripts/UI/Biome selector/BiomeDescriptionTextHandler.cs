using System.Collections;
using System.Collections.Generic;
using EventSystem;
using TMPro;
using UnityEngine;

public class BiomeDescriptionTextHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    private void Start()
    {
        EventChannels.OutpostEvents.OnSetDescription += SetDescription;
        SetDescription(EventChannels.OutpostEvents.OnGetDescription?.Invoke("Surface Depths"));
    }

    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnSetDescription -= SetDescription;
    }

    void SetDescription(string description)
    {
        descriptionText.text = description;
    }
}
