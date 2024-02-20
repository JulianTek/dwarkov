using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using EventSystem;
public class SaveDataHandler : MonoBehaviour
{
    private SaveData data;
    private int slotNumber;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetSlotNumber(int number)
    {
        slotNumber = number;
    }
}
