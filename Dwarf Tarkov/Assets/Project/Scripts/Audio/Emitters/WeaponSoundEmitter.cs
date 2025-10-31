using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class WeaponSoundEmitter : SoundEmitter
{
    private void Start()
    {
        EventChannels.WeaponEvents.OnWeaponFired += EnableCollider;
    }

    private void OnDestroy()
    {
        EventChannels.WeaponEvents.OnWeaponFired -= EnableCollider;
    }
}
