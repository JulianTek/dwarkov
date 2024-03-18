using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using EventSystem;

public static class DTOConverter
{
    public static ItemData ConvertItemDTOToItem(ItemDataDTO dto)
    {
        return EventChannels.DatabaseEvents.OnGetItemData?.Invoke(dto.Name);
    }

    public static ItemDataDTO ConvertItemToDTO(ItemData data)
    {
        return new ItemDataDTO(data);
    }

    public static List<ItemData> ConvertItemDTOListToItemList(List<ItemDataDTO> dtos)
    {
        List<ItemData> items = new List<ItemData>();
        foreach (ItemDataDTO dto in dtos)
        {
            items.Add(ConvertItemDTOToItem(dto));
        }
        return items;
    }

    public static List<ItemDataDTO> ConvertItemListToDTOList(List<ItemData> items)
    {
        List<ItemDataDTO> dtos = new List<ItemDataDTO>();
        foreach (ItemData item in items)
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
}
