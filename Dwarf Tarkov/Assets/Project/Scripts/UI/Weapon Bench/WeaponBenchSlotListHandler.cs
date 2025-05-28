using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EventSystem;

public class WeaponBenchSlotListHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList(bool isPrimary)
    {
        if (transform.childCount != 0)
        {
            return;
        }
        var weapons = EventChannels.DatabaseEvents.OnGetAllWeapons?.Invoke();
        foreach (var weapon in weapons)
        {
            if (weapon.IsPrimary == isPrimary)
            {
                GameObject weaponSlot = Instantiate(slot, gameObject.transform);
                weaponSlot.GetComponent<WeaponBenchSlotHandler>().SetWeaponData(weapon);
            }
        }
    }
}
