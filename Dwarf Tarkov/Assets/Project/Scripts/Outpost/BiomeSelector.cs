using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EventSystem;

public class BiomeSelector : UIWithButtonsHandler
{
    [SerializeField]
    [Tooltip("The index value has to be the scene index")]
    private List<BiomeIndex> biomes = new();
    private int currentlySelectedScene;
    private bool uiLoaded = false;
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        EventChannels.OutpostEvents.OnSelectBiome += SetSelectedBiome;
        EventChannels.OutpostEvents.OnGetSelectedScene += GetSelectedScene;
        EventChannels.UIEvents.OnHideBiomeSelector += CloseMenu;
    }

    private void OnDestroy()
    {
        EventChannels.OutpostEvents.OnSelectBiome -= SetSelectedBiome;
        EventChannels.OutpostEvents.OnGetSelectedScene -= GetSelectedScene;
        EventChannels.UIEvents.OnHideBiomeSelector -= CloseMenu;
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
        if (uiLoaded)
            return;
        foreach (var biome in biomes)
        {
            GameObject button = Instantiate(buttonToSpawn, parent.transform);
            button.GetComponent<BiomeUIButtonHandler>().SetBiome(biome.BiomeName);
        }
        uiLoaded = true;
    }
}
