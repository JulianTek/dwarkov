using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EventSystem;
using System.Linq;

namespace Data
{
    [Serializable]
    public class SaveData
    {
        public SaveData(int slotNumber)
        {
            SlotNumber = slotNumber;
            LastSaved = DateTime.Now;
        }
        // Data stuff
        public int SlotNumber { get; private set; }
        public DateTime LastSaved { get; private set; }

        // Inventories
        public List<ItemDTO> OutpostInventory { get; private set; } = new List<ItemDTO>();
        public List<ItemDTO> PlayerInventory { get; private set; } = new List<ItemDTO>();

        // Player stats
        public int PlayerLevel { get; private set; }
        public int PlayerExperience { get; private set; }
        public List<QuestDTO> PlayerQuests { get; private set; }

        // Quest stuff
        public List<QuestDTO> Quests { get; private set; } = new List<QuestDTO>();
        public List<QuestDTO> UnlockedQuests { get; private set; } = new List<QuestDTO>();
        public List<QuestDTO> CompletedQuests { get; private set; } = new List<QuestDTO>();

        // Player Weapons
        public WeaponDTO PrimaryWeapon { get; private set; }
        public WeaponDTO SecondaryWeapon { get; private set; }

        public void SetLastSaved()
        {
            LastSaved = DateTime.Now;
        }

        public IEnumerator Save()
        {
            // Set last saved to now
            SetLastSaved();

            // Set all data
            OutpostInventory = EventChannels.DataEvents.OnGetOutpostInventory?.Invoke();
            PlayerInventory = EventChannels.DataEvents.OnGetPlayerInventory?.Invoke();
            PlayerLevel = (int)EventChannels.DataEvents.OnGetPlayerLevel?.Invoke();
            PlayerExperience = (int)EventChannels.DataEvents.OnGetPlayerExperience?.Invoke();
            PlayerQuests = ConvertQuestsToDTOs(EventChannels.DataEvents.OnGetPlayerQuests?.Invoke());
            Quests = ConvertQuestsToDTOs(EventChannels.DataEvents.OnGetAllQuests?.Invoke());
            UnlockedQuests = ConvertQuestsToDTOs(EventChannels.DataEvents.OnGetUnlockedQuests?.Invoke());
            CompletedQuests = ConvertQuestsToDTOs(EventChannels.DataEvents.OnGetCompletedQuests?.Invoke());
            PrimaryWeapon = new WeaponDTO(EventChannels.DataEvents.OnGetPrimaryWeapon?.Invoke());
            SecondaryWeapon = new WeaponDTO(EventChannels.DataEvents.OnGetSecondaryWeapon?.Invoke()); 
            // Save all data
            DataSaver<SaveData>.Save(this, $"save_{SlotNumber}");
            yield return new WaitForEndOfFrame();
        }

        public List<Item> ConvertDTOsToItems(List<ItemDTO> dtos)
        {
            var allItems  = Resources.FindObjectsOfTypeAll(typeof(ItemData)) as ItemData[];
            List<Item> items = new List<Item>();
            foreach (ItemDTO dto in dtos)
            {
                items.Add(new Item(allItems.FirstOrDefault(item => item.Name == dto.Data.Name), dto.Amount));
            }
            return items;
        }

        public WeaponData GetWeaponDataFromDTO(WeaponDTO dto)
        {
            WeaponData[] weapons = Resources.FindObjectsOfTypeAll<WeaponData>();
            return weapons.FirstOrDefault<WeaponData>(weapon => weapon.firingEventName == dto.FiringEventName);
        }

        public List<QuestDTO> ConvertQuestsToDTOs(List<Quest> quests)
        {
            List<QuestDTO> dtos = new List<QuestDTO>();
            dtos.AddRange(quests.Select(dto => new QuestDTO()));
            return dtos;
        }
    }
 
}