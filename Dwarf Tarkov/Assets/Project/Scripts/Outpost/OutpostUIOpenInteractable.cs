using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class OutpostUIOpenInteractable : OutpostInteractable
{
    [SerializeField]
    private GameObject uiToOpen;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RunInteraction()
    {
        EventChannels.PlayerInputEvents.OnEnableHUDControls?.Invoke();
        uiToOpen.SetActive(true);
    }

    protected void CloseMenu()
    {
        EventChannels.PlayerInputEvents.OnDisableHUDControls?.Invoke();
        uiToOpen.SetActive(false);
    }
}
