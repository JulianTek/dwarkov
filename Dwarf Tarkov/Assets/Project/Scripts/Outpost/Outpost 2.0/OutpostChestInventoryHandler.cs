using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class OutpostChestInventoryHandler : BaseOutpostInventoryGridHandler
{

    private void Awake()
    {
        EventChannels.ItemEvents.OnUpdateOutpostInventory += UpdateInventory;
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        SpawnSlots(inventoryObject.GetComponent<OutpostChestInventory>().GetCapacity());
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventChannels.ItemEvents.OnUpdateOutpostInventory -= UpdateInventory;
    }

    protected override void UpdateInventory()
    {
        UpdateSlots(inventoryObject.GetComponent<OutpostChestInventory>().GetItems());
    }
}
