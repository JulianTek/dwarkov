using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvents
{
    public delegate void WeaponEvent();

    public WeaponEvent OnWeaponFired;
    public WeaponEvent OnWeaponReload;
    public WeaponEvent OnWeaponReloaded;
}
