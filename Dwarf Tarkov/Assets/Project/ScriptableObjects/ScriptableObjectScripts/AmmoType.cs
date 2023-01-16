using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Ammo type", menuName = "Dwarkov/Create new ammo type")]
public class AmmoType : ItemData
{
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public int BuyPrice;
    [BoxGroup("Split/AmmoInfo", LabelText = "Ammo Info")]
    public float BulletSpeed;
    [BoxGroup("Split/AmmoInfo", LabelText = "Ammo Info")]
    public int Damage;
}
