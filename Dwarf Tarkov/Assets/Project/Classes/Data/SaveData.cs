using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EventSystem;  

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
        public List<Quest> PlayerQuests { get; private set; }

        // Quest stuff
        public List<Quest> Quests { get; private set; } = new List<Quest>();
        public List<Quest> UnlockedQuests { get; private set; } = new List<Quest>();
        public List<Quest> CompletedQuests { get; private set; } = new List<Quest>();


        public void SetLastSaved()
        {
            LastSaved = DateTime.Now;
        }

        public void Save()
        {
            // Set last saved to now
            SetLastSaved();

            // Set all data
            OutpostInventory = EventChannels.DataEvents.OnGetOutpostInventory?.Invoke();
            PlayerInventory = EventChannels.DataEvents.OnGetPlayerInventory?.Invoke();
            PlayerLevel = (int)EventChannels.DataEvents.OnGetPlayerLevel?.Invoke();
            PlayerExperience = (int)EventChannels.DataEvents.OnGetPlayerExperience?.Invoke();
            PlayerQuests = EventChannels.DataEvents.OnGetPlayerQuests?.Invoke();
            Quests = EventChannels.DataEvents.OnGetAllQuests?.Invoke();
            UnlockedQuests = EventChannels.DataEvents.OnGetUnlockedQuests?.Invoke();
            CompletedQuests = EventChannels.DataEvents.OnGetCompletedQuests?.Invoke();

            // Save all data
            DataSaver<SaveData>.Save(this, $"save_{SlotNumber}");
        }
    }

}