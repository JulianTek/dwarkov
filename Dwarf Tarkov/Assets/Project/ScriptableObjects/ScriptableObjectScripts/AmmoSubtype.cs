using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Subtype", menuName = "Dwarkov/Create new ammo subtype")]
public class AmmoSubtype : Cartridge
{
    [BoxGroup("Split/AmmoInfo", LabelText = "Ammo Info")]
    public float BulletSpeed;
    [BoxGroup("Split/AmmoInfo", LabelText = "Ammo Info")]
    public int Damage;
}
