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

        // use empty ctor to create dev data
        public SaveData()
        {
            LastSaved = DateTime.Now;
        }

        // Boolean checks if the game has saved before, since this happens the first time the player leaves for the mines, this can be used to populate inventories, quests and initial unlocks
        public bool GameStarted { get; private set; } = false;
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

        // Weapon stats
        public int CurrentBulletsInPrimaryMag { get; private set; }
        public int CurrentBulletsInSecondaryMag { get; private set; }
        public AmmoSubtypeDTO CurrentlyLoadedSubtype { get; private set; }

        public void SetLastSaved()
        {
            LastSaved = DateTime.Now;
        }

        public IEnumerator SaveFromOutpost()
        {
            // Set last saved to now
            SetLastSaved();

            // Set all data
            GameStarted = true;
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
            CurrentBulletsInPrimaryMag = (int)EventChannels.DataEvents.OnGetAmountOfBullets?.Invoke(true);
            CurrentBulletsInSecondaryMag = (int)EventChannels.DataEvents.OnGetAmountOfBullets?.Invoke(false);
            CurrentlyLoadedSubtype = ConvertSubtypeToDTO(EventChannels.DataEvents.OnGetCurrentSubtype?.Invoke());
            // Save all data
            DataSaver<SaveData>.Save(this, $"save_{SlotNumber}");
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator SaveDevData(List<Item> inventory)
        {
            // Set last saved to now
            SetLastSaved();

            // implement quest logic later

            // Convert items to DTOs
            PlayerInventory = EventChannels.DataEvents.OnGetPlayerInventory?.Invoke();
            OutpostInventory = EventChannels.DataEvents.OnGetOutpostInventory?.Invoke();

            PlayerLevel = (int)EventChannels.DataEvents.OnGetPlayerLevel?.Invoke();
            PlayerExperience = (int)EventChannels.DataEvents.OnGetPlayerExperience?.Invoke();

            PrimaryWeapon = new WeaponDTO(EventChannels.DataEvents.OnGetPrimaryWeapon?.Invoke());
            SecondaryWeapon = new WeaponDTO(EventChannels.DataEvents.OnGetSecondaryWeapon?.Invoke());
            CurrentBulletsInPrimaryMag = (int)EventChannels.DataEvents.OnGetAmountOfBullets?.Invoke(true);
            CurrentBulletsInSecondaryMag = (int)EventChannels.DataEvents.OnGetAmountOfBullets?.Invoke(false);
            CurrentlyLoadedSubtype = ConvertSubtypeToDTO(EventChannels.DataEvents.OnGetCurrentSubtype?.Invoke());
            DataSaver<SaveData>.Save(this, "dev");
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator SaveFromMines()
        {
            // Set last saved to now
            SetLastSaved();

            // Set data that can be accessed in the mines
            PlayerLevel = (int)EventChannels.DataEvents.OnGetPlayerLevel?.Invoke();
            PlayerExperience = (int)EventChannels.DataEvents.OnGetPlayerExperience?.Invoke();
            PlayerInventory = EventChannels.DataEvents.OnGetPlayerInventory?.Invoke();
            PlayerQuests = ConvertQuestsToDTOs(EventChannels.DataEvents.OnGetPlayerQuests?.Invoke());
            PrimaryWeapon = new WeaponDTO(EventChannels.DataEvents.OnGetPrimaryWeapon?.Invoke());
            SecondaryWeapon = new WeaponDTO(EventChannels.DataEvents.OnGetSecondaryWeapon?.Invoke());
            CurrentBulletsInPrimaryMag = (int)EventChannels.DataEvents.OnGetAmountOfBullets?.Invoke(true);
            CurrentBulletsInSecondaryMag = (int)EventChannels.DataEvents.OnGetAmountOfBullets?.Invoke(false);
            CurrentlyLoadedSubtype = ConvertSubtypeToDTO(EventChannels.DataEvents.OnGetCurrentSubtype?.Invoke());

            // Save all data
            DataSaver<SaveData>.Save(this, $"save_{SlotNumber}");
            yield return new WaitForEndOfFrame();
        }

        public List<Item> ConvertDTOsToItems(List<ItemDTO> dtos)
        {
            var allItems = EventChannels.DatabaseEvents.OnGetAllItems?.Invoke();
            allItems.AddRange(EventChannels.DatabaseEvents.OnGetAllSubtypes?.Invoke());
            List<Item> items = new List<Item>();
            foreach (ItemDTO dto in dtos)
            {
                items.Add(new Item(allItems.FirstOrDefault(item => item.Name == dto.Data.Name), dto.Amount));
            }
            return items;
        }

        public WeaponData GetWeaponDataFromDTO(WeaponDTO dto)
        {
            List<WeaponData> weapons = EventChannels.DatabaseEvents.OnGetAllWeapons?.Invoke();
            return weapons.FirstOrDefault<WeaponData>(weapon => weapon.firingEventName == dto.FiringEventName);
        }

        public List<QuestDTO> ConvertQuestsToDTOs(List<Quest> quests)
        {
            List<QuestDTO> dtos = new List<QuestDTO>();
            foreach (Quest quest in quests)
            {
                if (quest is ItemQuest)
                    dtos.Add(new ItemQuestDTO(quest as ItemQuest));
                else if (quest is EnemyQuest)
                    dtos.Add(new EnemyQuestDTO(quest as EnemyQuest));
            }
            return dtos;
        }

        public AmmoSubtypeDTO ConvertSubtypeToDTO(AmmoSubtype subtype)
        {
            return new AmmoSubtypeDTO(subtype);
        }
    }

}