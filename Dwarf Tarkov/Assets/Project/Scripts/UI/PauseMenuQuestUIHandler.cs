using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using Data;

public class PauseMenuQuestUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject QuestButtons;
    [SerializeField]
    private GameObject QuestInfo;
    [SerializeField]
    private GameObject QuestButtonPrefab;
    // Start is called before the first frame update
    void OnEnable()
    {
        ShowList();
        var data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        foreach (QuestDTO dto in data.PlayerQuests)
        {
            var go = Instantiate(QuestButtonPrefab, transform);
            go.GetComponent<PauseMenuQuestButtonHandler>().SetQuest(new Quest(dto));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowList()
    {
        QuestInfo.SetActive(false);
        QuestButtons.SetActive(true);
    }
}
