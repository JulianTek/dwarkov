using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Data;
using TMPro;
using EventSystem;
using UnityEngine.UI;

public class TitleScreenHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject MainButtons;
    [SerializeField]
    private GameObject Options;
    [SerializeField]
    private GameObject SaveSlots;

    private void Start()
    {
        for (int i = 0; i < SaveSlots.transform.childCount; i++)
        {
            SaveData data = LoadData(i);
            if (data == null)
            {
                SaveSlots.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText("No data saved!");
                SaveSlots.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                SaveSlots.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText($"Last played on {data.LastSaved}");
            } 
        }
    }

    public void ShowSlots()
    {
        SaveSlots.SetActive(true);
        MainButtons.SetActive(false);
    }

    public void ShowOptions()
    {
        Options.SetActive(true);
        MainButtons.SetActive(false);
    }

    public void HideOptions()
    {
        Options.SetActive(false);
        MainButtons.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private SaveData LoadData(int slotNumber)
    {
        SaveData data = DataSaver<SaveData>.Load($"save_{slotNumber}");
        if (data == null)
            return null;
        return data;
    }

    private void StartNewGame(int slotNumber)
    {
        SaveData data = new SaveData(slotNumber);
        EventChannels.DataEvents.OnSetSaveData?.Invoke(data);
        DataSaver<SaveData>.Save(data, $"save_{slotNumber}");
    }

    public void StartGame(int slotNumber)
    {
        SaveData data = LoadData(slotNumber);
        if (data != null)
        {
            EventChannels.DataEvents.OnSetSaveData?.Invoke(data);
        }
        else
        {
            StartNewGame(slotNumber);
        }
        SceneManager.LoadScene(1);
    }

    public void DeleteSave(int slotNumber)
    {
        DataSaver<SaveData>.Delete($"save_{slotNumber}");
        Start();
    }
}
