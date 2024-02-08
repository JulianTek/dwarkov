using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class TitleScreenHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject Options;
    [SerializeField]
    private GameObject SaveSlots;

    private void Start()
    {
        
    }
    public void StartGame()
    {

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

    private void LoadData(int slotNumber)
    {
        SaveData data = DataSaver<SaveData>.Load($"save_{slotNumber}");
        if (data == null)
            return;

    }
}
