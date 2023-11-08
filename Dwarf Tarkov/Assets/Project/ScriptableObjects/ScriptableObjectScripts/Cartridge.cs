using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Cartridge", menuName = "Dwarkov/Create new cartridge")]
public class Cartridge : ItemData
{
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public int BuyPrice;
}
