using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Item", menuName = "Dwarkov/Create new item")]
public class ItemData : ScriptableObject, IItemInteractor
{
    [HideLabel]
    [PreviewField(50)]
    public Sprite Sprite;
    [HorizontalGroup("Split", LabelWidth = 80, Title = "Basic item info")]
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public string Name;
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public int BuyPrice;
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public int SellPrice;
    [BoxGroup("Split/TypeInfo", LabelText = "Type info")]
    [EnumPaging]
    public ItemType Type;
    [HorizontalGroup("StackSplit", LabelWidth = 80, Title = "Stacking Info", MarginRight = 20)]
    public bool IsStackable;
    [HorizontalGroup("StackSplit", LabelWidth = 85, Title = "Stacking Info")]
    public int MaxStackAmount;
    [TextArea]
    public string Description;

    public virtual void UseItem()
    {
        throw new System.NotImplementedException();
    }
}
