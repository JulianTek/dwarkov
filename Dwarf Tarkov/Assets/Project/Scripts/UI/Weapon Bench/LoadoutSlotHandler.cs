using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class LoadoutSlotHandler : MonoBehaviour
{
    [SerializeField]
    private Image slotSprite;
    private WeaponData data;
    // Start is called before the first frame update
    void OnEnable()
    {
        SetWeapon(EventChannels.WeaponEvents.OnGetPrimaryWeapon?.Invoke());

        EventChannels.WeaponEvents.OnSetPrimaryWeapon += SetWeapon;
        EventChannels.WeaponEvents.OnSetSecondaryWeapon += SetWeapon;
        EventChannels.UIEvents.OnSwitchWeaponSlotSide += SwitchSides;
    }

    private void OnDestroy()
    {
        EventChannels.WeaponEvents.OnSetPrimaryWeapon -= SetWeapon;
        EventChannels.WeaponEvents.OnSetSecondaryWeapon -= SetWeapon;
        EventChannels.UIEvents.OnSwitchWeaponSlotSide -= SwitchSides;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetWeapon(WeaponData data)
    {
        if (data == null)
            return;
        this.data = data;
        SetSprite(data.Sprite);
    }

    void SetSprite(Sprite sprite)
    {
        slotSprite.sprite = sprite;
        slotSprite.SetNativeSize();
    }

    void SwitchSides(bool isPrimary)
    {
        if (isPrimary)
            SetWeapon(EventChannels.WeaponEvents.OnGetPrimaryWeapon?.Invoke());
        else
            SetWeapon(EventChannels.WeaponEvents.OnGetSecondaryWeapon?.Invoke());
    }
}
