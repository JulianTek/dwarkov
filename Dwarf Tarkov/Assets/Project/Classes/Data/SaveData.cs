using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public SaveData(int slotNumber)
    {
        SlotNumber = slotNumber;
        LastSaved = DateTime.Now;
    }

    public int SlotNumber { get; private set; }
    public DateTime LastSaved { get; private set; }

    public void SetLastSaved()
    {
        LastSaved = DateTime.Now;
    }
}
