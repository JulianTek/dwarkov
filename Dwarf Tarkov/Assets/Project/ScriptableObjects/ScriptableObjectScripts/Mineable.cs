using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Mineable Mineral", menuName = "Dwarkov/Create new mineable material")]
public class Mineable : ItemData
{
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public ItemData ItemYielded;
}
