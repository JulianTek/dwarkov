using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using Data;
using UnityEngine.UI;

public class PauseMenuQuestUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject QuestButtons;
    [SerializeField]
    private GameObject QuestInfo;
    [SerializeField]
    private GameObject QuestButtonPrefab;
    private List<Quest> questsInList = new List<Quest>();
    // Start is called before the first frame update
    void OnEnable()
    {
        ShowList();
        var data = EventChannels.GameplayEvents.OnGetPlayerQuests?.Invoke();
        if (data != null)
        {
            foreach (Quest quest in data)
            {
                if(questsInList.Contains(quest))
                    continue;
                var go = Instantiate(QuestButtonPrefab, QuestButtons.transform);
                go.GetComponent<PauseMenuQuestButtonHandler>().SetQuest(quest);
                go.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(quest.Name);
                go.GetComponent<Button>().onClick.AddListener(ShowInfo);
                go.GetComponent<Button>().onClick.AddListener(() => SetQuestInfo(quest));
                questsInList.Add(quest);
            }
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
    
    public void ShowInfo()
    {
        QuestInfo.SetActive(true);
        QuestButtons.SetActive(false);
    }

    private void SetQuestInfo(Quest quest)
    {
        EventChannels.UIEvents.OnSetQuestInfoTitle?.Invoke(quest.Name);
        EventChannels.UIEvents.OnSetQuestInfoDescription?.Invoke(quest.MenuDescription);
        EventChannels.UIEvents.OnSetQuestInfoProgress?.Invoke(quest.GetProgress());
    }
}
