using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using EventSystem;
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

    private bool canSpawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        canSpawnEnemies = true;
        timerStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnEnemies)
        {
            if (timerStarted)
            {
                timerElapsed += Time.deltaTime;
                if (timerElapsed >= maxInterval)
                {
                    StartCoroutine(SpawnEnemy());
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
    }

    IEnumerator SpawnEnemy()
    {
        if ((bool)(EventChannels.EnemyEvents.OnSpawnEnemy?.Invoke()))
        {
            ObjectPoolHandler.SpawnObject(enemyToSpawn, transform.position, Quaternion.identity);
            SetTimer(false);
        }
        else
        {
            canSpawnEnemies = false;
        }
        yield return null;
    }

    void SetTimer(bool isStart)
    {
        timerStarted = isStart;
        cooldownTimerStarted = !isStart;
    }
}
