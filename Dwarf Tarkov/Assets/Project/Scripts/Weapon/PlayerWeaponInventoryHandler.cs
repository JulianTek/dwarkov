using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using Data;

public class PlayerWeaponInventoryHandler : MonoBehaviour
{
    [SerializeField]
    private WeaponData primaryWeapon;
    [SerializeField]
    private WeaponData secondaryWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        EventChannels.PlayerInputEvents.OnPlayerSelectPrimaryWeapon += SelectPrimary;
        EventChannels.PlayerInputEvents.OnPlayerSelectSecondaryWeapon += SelectSecondary;
        EventChannels.DataEvents.OnGetPrimaryWeapon += GetPrimaryWeapon;
        EventChannels.DataEvents.OnGetSecondaryWeapon += GetSecondaryWeapon;
        EventChannels.WeaponEvents.OnGetPrimaryWeapon += GetPrimaryWeapon;
        EventChannels.WeaponEvents.OnGetSecondaryWeapon += GetSecondaryWeapon;
        EventChannels.WeaponEvents.OnSetPrimaryWeapon += SetPrimaryWeapon;
        EventChannels.WeaponEvents.OnSetSecondaryWeapon += SetSecondaryWeapon;

        SaveData data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (data != null && data.PrimaryWeapon != null && data.SecondaryWeapon != null)
        {
            primaryWeapon = data.GetWeaponDataFromDTO(data.PrimaryWeapon);
            Debug.Log(primaryWeapon);
            secondaryWeapon = data.GetWeaponDataFromDTO(data.SecondaryWeapon);

        }

    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerSelectPrimaryWeapon -= SelectPrimary;
        EventChannels.PlayerInputEvents.OnPlayerSelectSecondaryWeapon -= SelectSecondary;
        EventChannels.DataEvents.OnGetPrimaryWeapon -= GetPrimaryWeapon;
        EventChannels.DataEvents.OnGetSecondaryWeapon -= GetSecondaryWeapon;
        EventChannels.WeaponEvents.OnGetPrimaryWeapon -= GetPrimaryWeapon;
        EventChannels.WeaponEvents.OnGetWeaponData -= GetSecondaryWeapon;
        EventChannels.WeaponEvents.OnSetPrimaryWeapon -= SetPrimaryWeapon;
        EventChannels.WeaponEvents.OnSetSecondaryWeapon -= SetSecondaryWeapon;
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

    private void SetPrimaryWeapon(WeaponData data)
    {
        primaryWeapon = data;
    }

    private void SetSecondaryWeapon(WeaponData data)
    {
        secondaryWeapon = data;
    }
}
