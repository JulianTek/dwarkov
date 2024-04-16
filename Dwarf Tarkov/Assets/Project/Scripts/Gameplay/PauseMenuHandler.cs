using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject questsButton;

    [Header("Menus")]
    [SerializeField]
    private GameObject mainButtons;
    [SerializeField]
    private GameObject questMenu;
    [SerializeField]
    private GameObject options;

    [Header("Options")]
    [SerializeField]
    private GameObject inputSettings;
    [SerializeField]
    private GameObject videoSettings;
    [SerializeField]
    private GameObject inputSettingsButton;
    [SerializeField]
    private GameObject videoSettingsButton;


    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        isPaused = false;
        pauseMenu.SetActive(false);
        questMenu.SetActive(false);
        EventChannels.PlayerInputEvents.OnPlayerPauses += HandleInput;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerInputEvents.OnPlayerPauses -= HandleInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;

        if (EventChannels.GameplayEvents.OnGetPlayerQuests?.Invoke().Count > 0)
            questsButton.SetActive(true);

        else
            questsButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
        EventChannels.GameplayEvents.OnPlayerResumesGame?.Invoke();
    }

    void HandleInput()
    {
        if (!isPaused)
            PauseGame();
        else
            ResumeGame();
    }

    public void ShowQuestMenu()
    {
        ToggleMainButtons(false);
        questMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ToggleMainButtons(bool isVisible)
    {
        mainButtons.SetActive(isVisible);
    }

    public void ShowOptions()
    {
        options.SetActive(true);
        ToggleMainButtons(false);
        ShowButtons();
    }

    public void HideOptions()
    {
        options.SetActive(false);
        videoSettings.SetActive(false);
        inputSettings.SetActive(false);
        ToggleMainButtons(true);
    }

    public void ShowInputSettings()
    {
        inputSettings.SetActive(true);
        HideButtons();
    }

    public void ShowVideoSettings()
    {
        videoSettings.SetActive(true);
        HideButtons();
    }

    public void HideButtons()
    {
        inputSettingsButton.SetActive(false);
        videoSettingsButton.SetActive(false);
    }

    void ShowButtons()
    {
        inputSettingsButton.SetActive(true);
        videoSettingsButton.SetActive(true);
    }
}
