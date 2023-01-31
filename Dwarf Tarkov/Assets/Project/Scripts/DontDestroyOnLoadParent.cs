using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadParent : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
