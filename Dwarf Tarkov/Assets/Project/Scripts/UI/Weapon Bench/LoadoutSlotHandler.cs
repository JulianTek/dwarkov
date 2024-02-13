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
        SetWeapon(EventChannels.WeaponEvents.OnGetWeaponData?.Invoke());

        EventChannels.WeaponEvents.OnSwitchWeapon += SetWeapon;
    }

    private void OnDestroy()
    {
        EventChannels.WeaponEvents.OnSwitchWeapon -= SetWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetWeapon(WeaponData data)
    {
        this.data = data;
        SetSprite(data.Sprite);
    }

    void SetSprite(Sprite sprite)
    {
        slotSprite.sprite = sprite;
        slotSprite.SetNativeSize();
    }
}
