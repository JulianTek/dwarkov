using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;
using System;

public class PlayerHealthHUDHandler : MonoBehaviour
{
    private Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        var saveData = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (saveData != null && saveData.PlayerHealth > 0f)
            healthBar.value = saveData.PlayerHealth;
        EventChannels.UIEvents.OnUpdateHealthbar += UpdateHealthbar;
    }

    private void UpdateHealthbar(float value)
    {
        healthBar.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
