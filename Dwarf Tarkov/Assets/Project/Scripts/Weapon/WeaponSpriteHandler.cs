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
        weaponData = GetComponentInParent<WeaponHandler>().GetWeaponData();
        spriteRenderer.sprite = weaponData.Sprite;

        EventChannels.WeaponEvents.OnWeaponReload += Reload;
        EventChannels.WeaponEvents.OnWeaponReloaded += Reloaded;
    }

    private void OnDestroy()
    {
        EventChannels.WeaponEvents.OnWeaponReload -= Reload;
        EventChannels.WeaponEvents.OnWeaponReloaded -= Reloaded;
    }

    void Reload()
    {
        spriteRenderer.sprite = weaponData.WeaponEmptySprite;
    }

    void Reloaded()
    {
        spriteRenderer.sprite = weaponData.Sprite;
    }
}
