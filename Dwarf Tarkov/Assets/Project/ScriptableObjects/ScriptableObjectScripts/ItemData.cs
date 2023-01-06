using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Item", menuName = "Dwarkov/Create new item")]
public class ItemData : ScriptableObject
{
    [HideLabel]
    [PreviewField(50)]
    public Sprite Sprite;
    [HorizontalGroup("Split")]
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public string Name;
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public int SellPrice;
    [BoxGroup("Split/TypeInfo")]
    [EnumPaging]
    public ItemType Type;
}
