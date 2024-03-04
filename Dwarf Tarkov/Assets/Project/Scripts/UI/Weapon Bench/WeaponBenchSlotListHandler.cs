using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        ClearList();
        var weapons = Resources.FindObjectsOfTypeAll(typeof(WeaponData)) as WeaponData[];
        foreach (var weapon in weapons)
        {
            if (weapon.IsPrimary == isPrimary)
            {
                GameObject weaponSlot = Instantiate(slot, gameObject.transform);
                weaponSlot.GetComponent<WeaponBenchSlotHandler>().SetWeaponData(weapon);
            }
        }
    }

    void ClearList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
