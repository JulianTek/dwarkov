using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            return ConvertDTOToItem(dto);
        }

        public static Item ConvertDTOToItem(ItemDTO dto)
        {
            var items = Resources.FindObjectsOfTypeAll(typeof(ItemData)) as ItemData[];
            foreach (var item in items)
            {
                if (item.Name == dto.Data.Name)
                    return new Item(item, dto.Amount);
            }
            return null;
        }

        public static void SaveInventory(string name, List<Item> iventory)
        {
            List<ItemDTO> itemDTOs = new List<ItemDTO>();
            foreach (Item item in iventory)
            {
                itemDTOs.Add(new ItemDTO(item));
            }
            DataSaver<List<ItemDTO>>.Save(itemDTOs, name);
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
                foreach (ItemDTO itemDTO in dtosList)
                {
                    items.Add(ConvertDTOToItem(itemDTO));
                }
                return items;
            }
            return null;
        }
    }
}

