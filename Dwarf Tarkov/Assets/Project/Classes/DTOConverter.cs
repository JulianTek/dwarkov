using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using EventSystem;

public static class DTOConverter
{
    public static ItemData ConvertItemDataDTOToItemData(ItemDataDTO dto)
    {
        var item = EventChannels.DatabaseEvents.OnGetItemData?.Invoke(dto.Name);
        if (item != null)
            return item;
        else
            return EventChannels.DatabaseEvents.OnGetSubtype?.Invoke(dto.Name);
    }

    public static ItemDataDTO ConvertItemDataToDTO(ItemData data)
    {
        return new ItemDataDTO(data);
    }

    public static List<ItemData> ConvertItemDataDTOListToItemDataList(List<ItemDataDTO> dtos)
    {
        List<ItemData> items = new List<ItemData>();
        foreach (ItemDataDTO dto in dtos)
        {
            if (dto is AmmoSubtypeDTO)
                items.Add(ConvertSubtypeDTOToSubtype(dto as AmmoSubtypeDTO));
            else
                items.Add(ConvertItemDataDTOToItemData(dto));
        }
        return items;
    }

    public static List<ItemDataDTO> ConvertItemDataListToDataDTOList(List<ItemData> items)
    {
        List<ItemDataDTO> dtos = new List<ItemDataDTO>();
        foreach (ItemData item in items)
        {
            if (item is AmmoSubtype)
                dtos.Add(ConvertSubtypeToDTO(item as AmmoSubtype));
            else
                dtos.Add(ConvertItemDataToDTO(item));
        }
        return dtos;
    }

    public static Item ConvertItemDTOToItem(ItemDTO dto)
    {
        return new Item(ConvertItemDataDTOToItemData(dto.Data), dto.Amount);
    }

    public static ItemDTO ConvertItemToDTO(Item data)
    {
        return new ItemDTO(data);
    }

    public static List<Item> ConvertItemDTOListToItemList(List<ItemDTO> dtos)
    {
        List<Item> items = new List<Item>();
        foreach (ItemDTO dto in dtos)
        {
            items.Add(ConvertItemDTOToItem(dto));
        }
        return items;
    }

    public static List<ItemDTO> ConvertItemListToDTOList(List<Item> items)
    {
        List<ItemDTO> dtos = new List<ItemDTO>();
        foreach (Item item in items)
        {
            dtos.Add(ConvertItemToDTO(item));
        }
        return dtos;
    }

    public static Quest ConvertQuestDTOToQuest(QuestDTO dto)
    {
        return new Quest(dto);
    }

    public static QuestDTO ConvertQuestToQuestDTO(Quest quest)
    {
        return new QuestDTO(quest);
    }

    public static List<Quest> ConvertQuestDTOListToQuestList(List<QuestDTO> dtos)
    {
        List<Quest> quests = new List<Quest>();
        foreach (QuestDTO dto in dtos)
        {
            quests.Add(ConvertQuestDTOToQuest(dto));
        }
        return quests;
    }

    public static List<QuestDTO> ConvertQuestListToQuestDTOList(List<Quest> quests)
    {
        List<QuestDTO> dtos = new List<QuestDTO>();
        foreach (Quest quest in quests)
        {
            dtos.Add(ConvertQuestToQuestDTO(quest));
        }
        return dtos;
    }

    public static AmmoSubtypeDTO ConvertSubtypeToDTO(AmmoSubtype subtype)
    {
        return new AmmoSubtypeDTO(subtype);
    }

    public static AmmoSubtype ConvertSubtypeDTOToSubtype(AmmoSubtypeDTO dto)
    {
        return EventChannels.DatabaseEvents.OnGetSubtype?.Invoke(dto.Name);
    }
}
