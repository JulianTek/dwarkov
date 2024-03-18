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

        public static DatabaseHandler instance;


        private void Awake()
        {
            // If the instance doesn't exist, set it to this object
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // If an instance already exists, destroy this object
                Destroy(gameObject);
            }
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

        public void SetDatabase(Database database)
        {
            this.database = database;
        }
    }
}
