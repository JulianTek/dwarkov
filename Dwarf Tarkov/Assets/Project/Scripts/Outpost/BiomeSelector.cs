using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EventSystem;

public class BiomeSelector : OutpostUIOpenInteractable
{
    [SerializeField]
    private GameObject biomeButton;
    [SerializeField]
    [Tooltip("The index value has to be the scene index")]
    private List<BiomeIndex> biomes = new();
    private int currentlySelectedScene;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.OutpostEvents.OnSelectBiome += SetSelectedBiome;
        EventChannels.OutpostEvents.OnGetSelectedScene += GetSelectedScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetSelectedBiome(string biomeName)
    {
        currentlySelectedScene = biomes.FirstOrDefault(x => x.BiomeName == biomeName).Index;
    }

    int GetSelectedScene()
    {
        return currentlySelectedScene;
    }

    public override void RunInteraction()
    {
        base.RunInteraction();
        foreach (var biome in biomes)
        {
            // todo: instantiate a button for each biome and run logic
        }
    }
}
