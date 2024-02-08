using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Data;
using TMPro;

public class TitleScreenHandler : MonoBehaviour
{
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
            }
            else
            {
                SaveSlots.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().SetText($"Last played on {data.LastSaved}");
            } 
        }
    }

    public void ShowOptions()
    {

    }

    public void HideOptions()
    {

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
        DataSaver<SaveData>.Save(new SaveData(slotNumber), $"save_{slotNumber}");;
    }

    public void StartGame(int slotNumber)
    {
        SaveData data = LoadData(slotNumber);
        if (data != null)
        {
            // correctly handle save data
        }
        else
        {
            StartNewGame(slotNumber);
        }
        SceneManager.LoadScene(2);
    }
}
