using Data;
using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostEventsInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.OutpostEvents.OnEnterOutpost?.Invoke();
        EventChannels.WeaponEvents.OnSetCanFire?.Invoke(false);
    }

    private void OnApplicationQuit()
    {
        SaveData data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (data != null)
            StartCoroutine(data.SaveFromOutpost());
    }
}
