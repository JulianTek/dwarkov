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

    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        isPaused = false;
        pauseMenu.SetActive(false);

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

        if (EventChannels.DataEvents.OnGetSaveData?.Invoke().PlayerQuests.Count > 0)
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
