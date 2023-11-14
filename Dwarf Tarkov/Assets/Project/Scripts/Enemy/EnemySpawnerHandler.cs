using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemySpawnerHandler : MonoBehaviour
{
    [SerializeField]
    [AssetsOnly]
    private GameObject enemyToSpawn;

    [VerticalGroup("Split")]
    [BoxGroup("Split/Time", LabelText = "Timer settings")]
    [SerializeField]
    private float maxInterval;
    [SerializeField]
    private float cooldownTimer;

    private float timerElapsed;

    private bool cooldownTimerStarted;
    private bool timerStarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            timerElapsed += Time.deltaTime;
            if (timerElapsed >= maxInterval)
            {
                // spawn enemy
                SetTimer(false);
            }
        }
        else if (cooldownTimerStarted)
        {
            timerElapsed += Time.deltaTime;
            if (timerElapsed >= maxInterval)
            {
                SetTimer(true);
            }
        }
    }

    void SetTimer(bool isStart)
    {
        timerStarted = isStart;
        cooldownTimerStarted = !isStart;
    }
}
