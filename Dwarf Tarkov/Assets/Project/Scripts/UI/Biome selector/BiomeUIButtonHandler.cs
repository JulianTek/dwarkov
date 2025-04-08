using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventSystem;

public class BiomeUIButtonHandler : MonoBehaviour
{
    private string biomeName;

    public void SetBiome(string biomeName)
    {
        this.biomeName = biomeName;
        GetComponentInChildren<TextMeshProUGUI>().text = biomeName;
    }

    public void SelectBiome()
    {
        EventChannels.OutpostEvents.OnSetTitle?.Invoke(biomeName);
        EventChannels.OutpostEvents.OnSelectBiome?.Invoke(biomeName);
        SetDescription();
    }

    private void SetDescription()
    {
        EventChannels.OutpostEvents.OnSetDescription?.Invoke(EventChannels.OutpostEvents.OnGetDescription?.Invoke(biomeName));
    }
}
