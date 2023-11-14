using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EventSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls playerControls;
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        playerControls.Player.Mine.performed += Mine;
        playerControls.Player.Shoot.started += ShootStarted;
        playerControls.Player.Shoot.canceled += ShootEnded;
        playerControls.Player.Reload.performed += Reload;
        playerControls.Player.Sprint.started += Sprint;
        playerControls.Player.Sprint.canceled += StopSprint;
        playerControls.Player.OpenInventory.performed += OpenInventory;
        playerControls.Player.Interact.performed += Interact;
        playerControls.Player.ToggleAmmoTypes.performed += ToggleAmmoTypes;

        playerControls.HUD.Close.performed += CloseInventory;
        playerControls.HUD.SplitStack.started += EnableStackSplit;
        playerControls.HUD.SplitStack.canceled += DisableStackSplit;

        EventChannels.PlayerInputEvents.OnDisableHUDControls += DisableHUDInput;
        EventChannels.PlayerInputEvents.OnEnableHUDControls += EnableHUDInput;
    }

    private void OnDestroy()
    {
        playerControls.Player.Mine.performed -= Mine;
        playerControls.Player.Shoot.started -= ShootStarted;
        playerControls.Player.Shoot.canceled -= ShootEnded;
        playerControls.Player.Sprint.started -= Sprint;
        playerControls.Player.Sprint.canceled -= StopSprint;
        playerControls.Player.OpenInventory.performed -= OpenInventory;
        playerControls.Player.Interact.performed -= Interact;
        playerControls.Player.ToggleAmmoTypes.performed -= ToggleAmmoTypes;

        playerControls.HUD.Close.performed -= CloseInventory;
    }

    private void Update()
    {
        Vector2 movementVector = playerControls.Player.Movement.ReadValue<Vector2>();
        Vector2 aimVector = playerControls.Player.Aim.ReadValue<Vector2>();
        EventChannels.PlayerInputEvents.OnPlayerAim?.Invoke(aimVector);
        EventChannels.PlayerInputEvents.OnPlayerMove?.Invoke(movementVector);
    }

    private void OnDisable()
    {
        playerControls.Player.Mine.performed -= Mine;
        playerControls.Player.Shoot.started -= ShootStarted;
        playerControls.Player.Shoot.canceled -= ShootEnded;
        playerControls.Player.Sprint.started -= Sprint;
        playerControls.Player.Sprint.canceled -= StopSprint;
        playerControls.Player.OpenInventory.performed -= OpenInventory;
        playerControls.Player.Interact.performed -= Interact;
        playerControls.Player.ToggleAmmoTypes.performed -= ToggleAmmoTypes;

        playerControls.HUD.Close.performed -= CloseInventory;
    }

    void ShootStarted(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerShootStarted?.Invoke();
    }

    void ShootEnded(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerShootFinished?.Invoke();
    }

    void Mine(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerMine?.Invoke();
    }

    void Sprint(InputAction.CallbackContext ctx)
    {
        ToggleSprint(true);
    }

    void StopSprint(InputAction.CallbackContext ctx)
    {
        ToggleSprint(false);
    }

    private void ToggleSprint(bool isSprinting)
    {
        EventChannels.PlayerInputEvents.OnPlayerSprint?.Invoke(isSprinting);
    }

    private void OpenInventory(InputAction.CallbackContext ctx)
    {
        playerControls.Player.Disable();
        playerControls.HUD.Enable();
        EventChannels.PlayerInputEvents.OnInventoryOpened?.Invoke();
    }

    private void CloseInventory(InputAction.CallbackContext ctx)
    {
        playerControls.Player.Enable();
        playerControls.HUD.Disable();
        EventChannels.PlayerInputEvents.OnInventoryClosed?.Invoke();
        EventChannels.PlayerInputEvents.OnToggleStackSplit?.Invoke(false);
    }

    private void EnableStackSplit(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnToggleStackSplit?.Invoke(true);
    }

    private void DisableStackSplit(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnToggleStackSplit?.Invoke(false);
    }

    private void Reload(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnPlayerReload?.Invoke();
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnInteract?.Invoke();
    }

    private void EnableHUDInput()
    {
        playerControls.Player.Disable();
        playerControls.HUD.Enable();
    }

    private void DisableHUDInput()
    {
        playerControls.Player.Enable();
        playerControls.HUD.Disable();
    }

    private void ToggleAmmoTypes(InputAction.CallbackContext ctx)
    {
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes?.Invoke();
    }
}