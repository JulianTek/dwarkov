using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Mineable Mineral", menuName = "Dwarkov/Create new mineable material")]
public class Mineable : ItemData
{
    [BoxGroup("Split/BasicInfo", LabelText = "Basic Info")]
    public ItemData ItemYielded;
    public float ItemAppearChanceMin;
    public float ItemAppearChanceMax;
    public int ExperienceGainedOnMine;
    [HorizontalGroup("Sprites")]
    [BoxGroup("Sprites/SpriteInfo", LabelText = "Sprite Info")]
    [PreviewField]
    public Sprite Stage2Sprite;
    [BoxGroup("Sprites/SpriteInfo", LabelText = "Sprite Info")]
    [PreviewField]
    public Sprite Stage3Sprite;
}
