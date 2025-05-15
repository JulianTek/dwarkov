using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using EventSystem;

public class BiomeButtonHandler : MonoBehaviour
{
    private string biomeName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBiome()
    {
        EventChannels.OutpostEvents.OnSelectBiome?.Invoke(biomeName);
    }
}
