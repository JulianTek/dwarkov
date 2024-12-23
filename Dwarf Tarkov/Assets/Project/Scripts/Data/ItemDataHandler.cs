using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EventSystem;

namespace Data
{
    public static class ItemDataHandler
    {
        public static void SaveItem(string name, Item item)
        {
            ItemDTO dto = new ItemDTO(new ItemDataDTO(item.data), item.amount);
            DataSaver<ItemDTO>.Save(dto, name);
        }

        public static Item LoadItem(string name)
        {
            ItemDTO dto = DataSaver<ItemDTO>.Load(name);
            return DTOConverter.ConvertItemDTOToItem(dto);
        }

        public static void SaveInventory(string name, List<Item> inventory)
        {
            DataSaver<List<ItemDTO>>.Save(DTOConverter.ConvertItemListToDTOList(inventory), name);
        }

        public static List<Item> LoadInventory(string name)
        {
            var dtos = DataSaver<IEnumerable<ItemDTO>>.Load(name);
            if (dtos != null)
            {
                List<ItemDTO> dtosList = dtos.ToList();
                List<Item> items = new List<Item>();
                if (dtosList == null || dtosList.Count == 0)
                    return null;
                return DTOConverter.ConvertItemDTOListToItemList(dtosList);
            }
            return null;
        }

        public static void DeleteInventory()
        {
            SaveData data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
            if (data != null)
            {
                data.ClearInventory();
            }
        }
    }
}

