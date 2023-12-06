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

    private bool primaryIsSelected;
    // Start is called before the first frame update
    void Start()
    {
        primaryIsSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchWeapon()
    {
        if (primaryIsSelected)
        {
            EventChannels.WeaponEvents.OnSwitchWeapon?.Invoke(secondaryWeapon);
            primaryIsSelected = false;
        }
        else
        {
            EventChannels.WeaponEvents.OnSwitchWeapon?.Invoke(primaryWeapon);
        }
    }
}
