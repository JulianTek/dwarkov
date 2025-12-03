using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;

public class PlayerSubtypeHandler : MonoBehaviour
{
    private bool currentlyTogglingAmmoTypes;
    private int ammoTypeIndex;
    private AmmoSubtype subtypeLoaded;
    private AmmoSubtype cachedSubTypeLoaded;

    private void Awake()
    {
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes += ToggleAmmoTypes;
        EventChannels.ItemEvents.OnGetCurrentlyLoadedAmmo += GetCurrentlyLoadedAmmoType;
        EventChannels.ItemEvents.OnGetSubtypesInInventory += GetAmmoTypesInInventory;
        EventChannels.WeaponEvents.OnGetAmmoType += GetCurrentlyLoadedAmmoType;
        EventChannels.DataEvents.OnGetCurrentSubtype += GetCurrentlyLoadedAmmoType;
        EventChannels.WeaponEvents.OnSwitchWeapon += SwitchAmmoTypes;
        EventChannels.WeaponEvents.OnSetCurrentSubtype += SetCurrentlyLoadedSubtype;
        EventChannels.WeaponEvents.OnWeaponReloaded += UnsetCurrentlyToggling;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnToggleAmmoTypes -= ToggleAmmoTypes;
        EventChannels.ItemEvents.OnGetCurrentlyLoadedAmmo -= GetCurrentlyLoadedAmmoType;
        EventChannels.ItemEvents.OnGetSubtypesInInventory -= GetAmmoTypesInInventory;
        EventChannels.WeaponEvents.OnGetAmmoType -= GetCurrentlyLoadedAmmoType;
        EventChannels.DataEvents.OnGetCurrentSubtype -= GetCurrentlyLoadedAmmoType;
        EventChannels.WeaponEvents.OnSwitchWeapon -= SwitchAmmoTypes;
        EventChannels.WeaponEvents.OnSetCurrentSubtype -= SetCurrentlyLoadedSubtype;
        EventChannels.WeaponEvents.OnWeaponReloaded -= UnsetCurrentlyToggling;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentlyTogglingAmmoTypes = false;
        var saveData = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (saveData != null && saveData.CurrentlyLoadedSubtype != null)
        {
            subtypeLoaded = EventChannels.DatabaseEvents.OnGetSubtype(saveData.CurrentlyLoadedSubtype.Name);
        }
    }
    public void ToggleAmmoTypes()
    {
        WeaponData data = EventChannels.WeaponEvents.OnGetWeaponData?.Invoke();
        EventChannels.WeaponEvents.OnSetCanFire?.Invoke(false);
        // If toggling ammo types already, loop between all available ammo types
        if (currentlyTogglingAmmoTypes)
        {
            if (ammoTypeIndex == data.AmmoSubtypes.Count - 1)
                ammoTypeIndex = 0;
            else if (ammoTypeIndex > data.AmmoSubtypes.Count)
                ammoTypeIndex = 1;
            else
                ammoTypeIndex++;
        }
        else
        {
            // If not toggling ammo types already, open the UI for it
            if (GetAmmoTypesInInventory().Count != 0)
            {
                EventChannels.UIEvents.OnShowAmmoTypes?.Invoke();
                currentlyTogglingAmmoTypes = true;
            }
            else
            {
                EventChannels.UIEvents.OnShowNoAmmoTypes?.Invoke();
            }
        }
    }
    public AmmoSubtype GetCurrentlyLoadedAmmoType()
    {
        return subtypeLoaded;
    }

    public List<AmmoSubtype> GetAmmoTypesInInventory()
    {
        WeaponData data = EventChannels.WeaponEvents.OnGetWeaponData?.Invoke();
        List<AmmoSubtype> typesInInventory = new List<AmmoSubtype>();
        foreach (AmmoSubtype ammoSubtype in data.AmmoSubtypes)
        {
            if ((bool)EventChannels.ItemEvents.OnCheckIfItemInInventory?.Invoke(ammoSubtype))
            {
                typesInInventory.Add(ammoSubtype);
            }
        }
        return typesInInventory.OrderBy<AmmoSubtype, string>(type => type.Name).ToList();
    }

    private void SwitchAmmoTypes(WeaponData data)
    {
        (subtypeLoaded, cachedSubTypeLoaded) = (cachedSubTypeLoaded, subtypeLoaded);
    }

    private void SetCurrentlyLoadedSubtype(AmmoSubtype subtypeToLoad)
    {
        subtypeLoaded = subtypeToLoad;
    }

    private void UnsetCurrentlyToggling()
    {
        currentlyTogglingAmmoTypes = false;
    }
}
