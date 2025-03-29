using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BiomeDescriptionHandler : MonoBehaviour
{
    [SerializeField]
    private List<BiomeNameDescription> biomeDescriptions = new();
    private void Start()
    {
        
    }

    string GetDescription(string name)
    {
        return biomeDescriptions.FirstOrDefault(x => x.BiomeName == name).Description;
    }
}
