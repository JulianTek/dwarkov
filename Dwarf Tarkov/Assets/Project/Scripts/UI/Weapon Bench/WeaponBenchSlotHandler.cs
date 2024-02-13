using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBenchSlotHandler : MonoBehaviour
{
    [SerializeField]
    private Image slotSprite;
    private WeaponData weaponData;
    // Start is called before the first frame update
    void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetSprite(Sprite sprite)
    {
        slotSprite.sprite = sprite;
        slotSprite.SetNativeSize();
    }

    public void SetWeaponData(WeaponData data)
    {
        weaponData = data;
        SetSprite(data.Sprite);
    }
}
