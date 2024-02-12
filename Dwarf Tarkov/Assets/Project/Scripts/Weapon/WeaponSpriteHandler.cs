using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class WeaponSpriteHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private WeaponData weaponData;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponData = GetComponentsInParent<WeaponHandler>()[0].GetWeaponData();
        if (weaponData != null)
            spriteRenderer.sprite = weaponData.Sprite;

        EventChannels.WeaponEvents.OnWeaponReload += Reload;
        EventChannels.WeaponEvents.OnWeaponReloaded += Reloaded;
        EventChannels.WeaponEvents.OnSwitchWeapon += SetWeaponData;
    }

    private void OnDestroy()
    {
        EventChannels.WeaponEvents.OnWeaponReload -= Reload;
        EventChannels.WeaponEvents.OnWeaponReloaded -= Reloaded;
        EventChannels.WeaponEvents.OnSwitchWeapon -= SetWeaponData;
    }

    void Reload()
    {
        spriteRenderer.sprite = weaponData.WeaponEmptySprite;
    }

    void Reloaded()
    {
        spriteRenderer.sprite = weaponData.Sprite;
    }

    void SetWeaponData(WeaponData weaponData)
    {
        this.weaponData = weaponData;
    }
}
