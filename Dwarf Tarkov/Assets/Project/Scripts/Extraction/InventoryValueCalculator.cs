using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventSystem;

public class InventoryValueCalculator : MonoBehaviour
{
    private TextMeshProUGUI valueText;
    // Start is called before the first frame update
    void Awake()
    {
        valueText = GetComponent<TextMeshProUGUI>();
        EventChannels.ExtractionEvents.OnSetInventoryValue += SetInventoryValue;

    }

    private void Start()
    {
        EventChannels.ExtractionEvents.OnGetInventoryValue?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetInventoryValue(int value)
    {
        valueText.text = $"Inventory value: {value} credits";
    }
}
