using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;

public class BiomeTitleDescriptionHandler : MonoBehaviour
{
    [SerializeField]
    private List<BiomeNameDescription> biomeDescriptions = new();
    private void Awake()
    {
        EventChannels.OutpostEvents.OnGetDescription += GetDescription;
    }

    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnGetDescription -= GetDescription;
    }

    string GetDescription(string name)
    {
        return biomeDescriptions.FirstOrDefault(x => x.BiomeName == name).Description;
    }
}
