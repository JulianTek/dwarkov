using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using EventSystem;
public class SaveDataHandler : MonoBehaviour
{
    private SaveData data;
    public int slotNumber { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        EventChannels.DataEvents.OnGetSaveData += GetData;
        EventChannels.DataEvents.OnSetSaveData += SetData;
        EventChannels.GameplayEvents.OnGetCompletedQuests += GetCompletedQuests;
    }

    private void OnDestroy()
    {
        EventChannels.DataEvents.OnGetSaveData -= GetData;
        EventChannels.DataEvents.OnSetSaveData -= SetData;
        EventChannels.GameplayEvents.OnGetCompletedQuests -= GetCompletedQuests;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlotNumber(int number)
    {
        slotNumber = number;
    }

    public SaveData GetData()
    {
        return data;
    }

    void SetData(SaveData data)
    {
        this.data = data;
    }

    private List<Quest> GetCompletedQuests()
    {
        return DTOConverter.ConvertQuestDTOListToQuestList(data.CompletedQuests);
    }
}
