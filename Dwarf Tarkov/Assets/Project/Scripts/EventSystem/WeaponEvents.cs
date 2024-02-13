using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvents
{
    public delegate void WeaponActionEvent();
    public delegate void WeaponEvent(WeaponData weapon);
    public delegate AmmoSubtype AmmoSubtypeEvent();
    public delegate WeaponData WeaponDataEvent();

    public WeaponActionEvent OnWeaponFired;
    public WeaponActionEvent OnWeaponReload;
    public WeaponActionEvent OnWeaponReloaded;

    public AmmoSubtypeEvent OnGetAmmoType;

    public WeaponEvent OnSwitchWeapon;

    public WeaponDataEvent OnGetWeaponData;
}
