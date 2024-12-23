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

    [SerializeField]
    private GameObject VideoOptions;
    [SerializeField]
    private GameObject InputOptions;
    [SerializeField]
    private GameObject VideoOptionsButton;
    [SerializeField]
    private GameObject InputOptionsButton;

    [SerializeField]
    private GameObject Credits;
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
        VideoOptions.SetActive(false);
        InputOptions.SetActive(false);
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

    public void ShowVideoSettings()
    {
        VideoOptions.SetActive(true);
    }

    public void ShowInputSettings()
    {
        InputOptions.SetActive(true);
    }

    public void HideOptionButtons()
    {
        VideoOptionsButton.SetActive(false);
        InputOptionsButton.SetActive(false);
    }

    public void ShowOptionButtons()
    {
        VideoOptionsButton.SetActive(true);
        InputOptionsButton.SetActive(true);
    }

    public void ShowCredits()
    {
        MainButtons.SetActive(false);
        Credits.SetActive(true);
    }

    public void HideCredits()
    {
        MainButtons.SetActive(true);
        Credits.SetActive(false);
    }
}
