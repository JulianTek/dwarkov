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
    }
}
