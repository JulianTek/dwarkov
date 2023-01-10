using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum ItemType
{
    [Description("Rock")]
    Rock,
    [Description("Metal ore")]
    Ore,
    [Description("Precious gem")]
    Gem,
    [Description("Biological matter")]
    BioMatter
}
