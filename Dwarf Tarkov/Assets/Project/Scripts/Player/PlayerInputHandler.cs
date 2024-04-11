using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EventSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _actionAsset;
    private bool isPaused;
    private Vector2 moveIndex;
    // Start is called before the first frame update
    void Start()
    {

        EventChannels.PlayerInputEvents.OnDisableHUDControls += DisableHUDInput;
        EventChannels.PlayerInputEvents.OnEnableHUDControls += EnableHUDInput;

        EventChannels.GameplayEvents.OnPlayerResumesGame += ResumeGame;
    }


    public void OnDisable()
    {

        EventChannels.PlayerInputEvents.OnDisableHUDControls -= DisableHUDInput;
        EventChannels.PlayerInputEvents.OnEnableHUDControls -= EnableHUDInput;

        EventChannels.GameplayEvents.OnPlayerResumesGame -= ResumeGame;
    }


    public void Update()
    {
        if (!isPaused)
            EventChannels.PlayerInputEvents.OnPlayerMove?.Invoke(moveIndex);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
       moveIndex = ctx.ReadValue<Vector2>();
    }

    public void Aim(InputAction.CallbackContext ctx)
    {
        if (!isPaused)
        {
            Vector2 aimVector = ctx.ReadValue<Vector2>();
            EventChannels.PlayerInputEvents.OnPlayerAim?.Invoke(aimVector);
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            EventChannels.PlayerInputEvents.OnPlayerShootStarted?.Invoke();
        else if (ctx.canceled)
            EventChannels.PlayerInputEvents.OnPlayerShootFinished?.Invoke();
    }

    public void SelectPrimaryWeapon(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerSelectPrimaryWeapon?.Invoke();
    }
    public void SelectSecondaryWeapon(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerSelectSecondaryWeapon?.Invoke();
    }

    public void Mine(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerMine?.Invoke();
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            ToggleSprint(true);
        else if (ctx.canceled)
            ToggleSprint(false);
    }

    public void ToggleSprint(bool isSprinting)
    {
        EventChannels.PlayerInputEvents.OnPlayerSprint?.Invoke(isSprinting);
    }

    public void OpenInventory(InputAction.CallbackContext ctx)
    {
        _actionAsset.FindActionMap("Player").Disable();
        _actionAsset.FindActionMap("HUD").Enable();
        EventChannels.PlayerInputEvents.OnInventoryOpened?.Invoke();
    }

    public void CloseInventory(InputAction.CallbackContext ctx)
    {
        _actionAsset.FindActionMap("Player").Enable();
        _actionAsset.FindActionMap("HUD").Disable();
        EventChannels.PlayerInputEvents.OnInventoryClosed?.Invoke();
        EventChannels.PlayerInputEvents.OnToggleStackSplit?.Invoke(false);
        EventChannels.OutpostEvents.OnHideOutpostInventory?.Invoke();
        EventChannels.OutpostEvents.OnHideWeaponBench?.Invoke();
        TooltipSystem.Hide();
    }

    public void StackSplit(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            EventChannels.PlayerInputEvents.OnToggleStackSplit?.Invoke(true);
        else if (ctx.canceled)
            EventChannels.PlayerInputEvents.OnToggleStackSplit?.Invoke(false);
    }

    public void Reload(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerReload?.Invoke();
    }


    public void Interact(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnInteract?.Invoke();
    }

    public void EnableHUDInput()
    {
        _actionAsset.FindActionMap("Player").Disable();
        _actionAsset.FindActionMap("HUD").Enable();
    }

    public void DisableHUDInput()
    {
        _actionAsset.FindActionMap("Player").Enable();
        _actionAsset.FindActionMap("HUD").Disable();
    }

    public void ToggleAmmoTypes(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes?.Invoke();
    }


    public void PauseGame(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerPauses?.Invoke();
        isPaused = !isPaused;
    }

    public void ResumeGame()
    {
        DisableHUDInput();
    }
}
