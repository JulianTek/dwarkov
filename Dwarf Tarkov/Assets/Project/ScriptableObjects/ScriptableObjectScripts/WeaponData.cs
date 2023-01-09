using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Weapon", menuName = "Dwarkov/Create new Weapon")]
public class WeaponData : ScriptableObject
{
    [HorizontalGroup("Split")]
    [BoxGroup("Split/WeaponMechanics", LabelText = "Mechanics")]
    public int Damage;
    [BoxGroup("Split/WeaponMechanics", LabelText = "Mechanics")]
    public bool IsAutoFire;
    [BoxGroup("Split/WeaponMechanics", LabelText = "Mechanics")]
    public float RateOfFire;
    [BoxGroup("Split/Ammo", LabelText = "Ammo Stats")]
    public int MagCapacity;
    [BoxGroup("Split/Ammo", LabelText = "Ammo Stats")]
    public float ReloadTime;
    [HorizontalGroup("Split2")]
    [BoxGroup("Split2/Sprites")]
    [PreviewField(50)]
    public Sprite Sprite;
    [BoxGroup("Split2/Sprites")]
    [PreviewField(50)]
    public Sprite WeaponEmptySprite;
    [BoxGroup("Split2/Sprites")]
    [PreviewField(50)]
    public Sprite MagazineSprite;
    [BoxGroup("Split2/Sprites")]
    [PreviewField(50)]
    public Sprite CaseSprite;
    [BoxGroup("Split2/Spread", LabelText = "Spread and other stats")]
    public float BaseSpreadAngle;
    [BoxGroup("Split2/Spread", LabelText = "Spread and other stats")]
    public int AmountOfBullets;
}
