using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.SceneManagement;

public class ExtractionManager : MonoBehaviour
{
    [SerializeField]
    private int extractionTimeInSeconds;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.ExtractionEvents.OnExtractionTimerFinished += Extract;
    }

    private void OnDestroy()
    {
        EventChannels.ExtractionEvents.OnExtractionTimerFinished -= Extract;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            EventChannels.ExtractionEvents.OnStartExtractionTimer?.Invoke(extractionTimeInSeconds);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            EventChannels.ExtractionEvents.OnStopExtractionTimer?.Invoke();
        }
    }

    void Extract()
    {
        SceneManager.LoadScene(1);
    }
}
