using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Dwarkov/Create new database")]
public class Database : ScriptableObject
{
    public List<ItemData> AllItems;
    public List<WeaponData> AllWeapons;
    public List<Cartridge> AllCartridges;
    public List<AmmoSubtype> AllAmmoSubtypes;
}
