using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class OutpostUIOpenInteractable : OutpostInteractable
{
    [SerializeField]
    private GameObject uiToOpen;
    [SerializeField]
    private bool closeMenuOnStart = true;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        if (closeMenuOnStart)
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
