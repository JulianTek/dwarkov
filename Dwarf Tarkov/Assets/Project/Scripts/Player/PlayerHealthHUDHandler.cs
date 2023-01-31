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
