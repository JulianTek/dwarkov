using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponBenchSlotListHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;
    private List<WeaponData> weapons = new List<WeaponData>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateList()
    {
        var array = Resources.FindObjectsOfTypeAll(typeof(WeaponData)) as WeaponData[];
        weapons = array.ToList();
        foreach (var weapon in weapons)
        {
            GameObject weaponSlot = Instantiate(slot, gameObject.transform);
            weaponSlot.GetComponent<WeaponBenchSlotHandler>().SetWeaponData(weapon);
        }
    }
}
