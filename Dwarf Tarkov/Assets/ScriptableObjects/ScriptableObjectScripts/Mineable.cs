using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Mineable Mineral", menuName = "Dwarkov/Create new mineable material")]
public class Mineable : ItemData
{
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public ItemData ItemYielded;
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public int ExperienceGainedOnMine;
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public int MineAmount;
    [HorizontalGroup("Sprites")]
    [BoxGroup("Sprites/SpriteInfo", LabelText = "Sprite Info")]
    [PreviewField]
    public Sprite Stage2Sprite;
    [BoxGroup("Sprites/SpriteInfo", LabelText = "Sprite Info")]
    [PreviewField]
    public Sprite Stage3Sprite;
}
