using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EventSystem;

namespace Data
{
    public class DatabaseHandler : MonoBehaviour
    {
        [SerializeField]
        private Database database;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            EventChannels.DatabaseEvents.OnGetItemData += GetItem;
            EventChannels.DatabaseEvents.OnGetWeaponData += GetWeapon;
            EventChannels.DatabaseEvents.OnGetSubtype += GetSubtype;
            EventChannels.DatabaseEvents.OnGetAllWeapons += GetAllWeapons;
            EventChannels.DatabaseEvents.OnGetAllItems += GetAllItems;
            EventChannels.DatabaseEvents.OnGetAllSubtypes += GetSubtypes;
        }

        private void OnDestroy()
        {
            EventChannels.DatabaseEvents.OnGetItemData -= GetItem;
            EventChannels.DatabaseEvents.OnGetWeaponData -= GetWeapon;
            EventChannels.DatabaseEvents.OnGetSubtype -= GetSubtype;
            EventChannels.DatabaseEvents.OnGetAllWeapons -= GetAllWeapons;
            EventChannels.DatabaseEvents.OnGetAllItems -= GetAllItems;
            EventChannels.DatabaseEvents.OnGetAllSubtypes -= GetSubtypes;
        }

        public WeaponData GetWeapon(string name)
        {
            return database.AllWeapons.Where(weapon => weapon.name == name).FirstOrDefault();
        }

        public List<WeaponData> GetAllWeapons()
        {
            return database.AllWeapons;
        }

        public ItemData GetItem(string name)
        {
            return database.AllItems.Where(item => item.Name == name).FirstOrDefault();
        }

        public List<ItemData> GetAllItems()
        {
            return database.AllItems;
        }

        public AmmoSubtype GetSubtype(string name)
        {
            return database.AllAmmoSubtypes.Where(subtype => subtype.Name == name).FirstOrDefault();
        }

        public List<AmmoSubtype> GetSubtypes()
        {
            return database.AllAmmoSubtypes;
        }
    }
}
