using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
public class MineEventInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.WeaponEvents.OnSetCanFire?.Invoke(true);
    }
}
