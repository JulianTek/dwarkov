using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class ExctractionSceneEventInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.GameplayEvents.OnPlayerExtracted?.Invoke();
    }
}
