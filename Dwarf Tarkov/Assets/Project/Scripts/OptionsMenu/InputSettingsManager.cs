using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSettingsManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset controls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        controls.Disable();
    }

    private void OnDisable()
    {
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
