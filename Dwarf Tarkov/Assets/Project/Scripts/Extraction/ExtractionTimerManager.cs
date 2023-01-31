using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventSystem;

public class ExtractionTimerManager : MonoBehaviour
{
    private float timeLeft;
    private bool timerStarted;
    private TextMeshProUGUI timerText;
    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerStarted = false;
        EventChannels.ExtractionEvents.OnStartExtractionTimer += StartTimer;
        EventChannels.ExtractionEvents.OnStopExtractionTimer += StopTimer;
        timerText.enabled = false;
    }

    private void OnDisable()
    {
        EventChannels.ExtractionEvents.OnStartExtractionTimer -= StartTimer;
        EventChannels.ExtractionEvents.OnStopExtractionTimer -= StopTimer;
    }

    private void StartTimer(int duration)
    {
        timeLeft = duration;
        timerStarted = true;
        timerText.enabled = true;
    }

    private void Update()
    {
        if (timerStarted)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = $"Extracting in {timeLeft.ToString("F2")}s";

            if (timeLeft <= 0)
            {
                EventChannels.ExtractionEvents.OnExtractionTimerFinished?.Invoke();
                timerStarted = false;
            }
        }
    }

    private void StopTimer()
    {
        timerStarted = false;
    }
}
