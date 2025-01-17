using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Heal Item", menuName = "Dwarkov/Create new healing item")]
public class HealItem : ItemData
{
    [BoxGroup("Split/HealingInfo", LabelText = "Healing Info")]
    public bool HealOverTime;
    [BoxGroup("Split/HealingInfo", LabelText = "Healing Info")]
    public float HealTime;
    [BoxGroup("Split/HealingInfo", LabelText = "Healing Info")]
    public float HealValue;
}
