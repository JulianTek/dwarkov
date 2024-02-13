using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PlayerWeaponInventoryHandler : MonoBehaviour
{
    [SerializeField]
    private WeaponData primaryWeapon;
    [SerializeField]
    private WeaponData secondaryWeapon;

    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerInputEvents.OnPlayerSelectPrimaryWeapon += SelectPrimary;
        EventChannels.PlayerInputEvents.OnPlayerSelectSecondaryWeapon += SelectSecondary;
        SelectPrimary();
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerSelectPrimaryWeapon -= SelectPrimary;
        EventChannels.PlayerInputEvents.OnPlayerSelectSecondaryWeapon -= SelectSecondary;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectPrimary()
    {
        EventChannels.WeaponEvents.OnSwitchWeapon?.Invoke(primaryWeapon);
    }

    private void SelectSecondary()
    {
        EventChannels.WeaponEvents.OnSwitchWeapon?.Invoke(secondaryWeapon);
    }

    public WeaponData GetPrimaryWeapon()
    {
        return primaryWeapon;
    }
    public WeaponData GetSecondaryWeapon()
    {
        return secondaryWeapon;
    }
}
