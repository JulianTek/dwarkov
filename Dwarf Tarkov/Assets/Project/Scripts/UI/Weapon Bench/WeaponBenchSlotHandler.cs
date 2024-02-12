using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBenchSlotHandler : MonoBehaviour
{
    private Image slotSprite;
    private WeaponData weaponData;
    // Start is called before the first frame update
    void Start()
    {
        slotSprite = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetSprite(Sprite sprite)
    {
        slotSprite.sprite = sprite;
    }

    public void SetWeaponData(WeaponData data)
    {
        weaponData = data;
        SetSprite(data.Sprite);
    }
}
