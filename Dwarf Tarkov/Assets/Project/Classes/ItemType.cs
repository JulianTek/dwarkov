using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    [Description("Rock")]
    Rock,
    [Description("Metal ore")]
    Ore,
    [Description("Precious gem")]
    Gem,
    [Description("Biological matter")]
    BioMatter,
    [Description("Ammunition")]
    Ammunition,
    [Description("Valuable")]
    Valuable,
    [Description("Flower")]
    Flower,
    [Description("Healing item")]
    HealItem,
}
