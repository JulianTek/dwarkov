using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Data;
using EventSystem;

public class MineDoorHandler : MonoBehaviour
{
    [SerializeField]
    private float timerToMineMax;
    private float timerTime;
    private bool playerIsInTrigger;

    [SerializeField]
    private TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        timerTime = timerToMineMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsInTrigger)
        {
            timerTime -= Time.deltaTime;
            if (!countdownText.enabled)
            {
                countdownText.enabled = true;
            }
            countdownText.text = $"Heading to the mines in {timerTime.ToString("F2")}s";
        }
        if (timerTime > 0)
            return;
        SaveData data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (data != null)
            StartCoroutine(data.SaveFromOutpost());
        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            playerIsInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            playerIsInTrigger = false;
            countdownText.enabled = false;
            timerTime = timerToMineMax;
        }
    }
}
