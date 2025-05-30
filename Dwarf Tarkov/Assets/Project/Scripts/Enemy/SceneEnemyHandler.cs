using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class SceneEnemyHandler : MonoBehaviour
{
    [SerializeField]
    private int maxAmountOfEnemies;
    private int currentAmountOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.EnemyEvents.OnSpawnEnemy += CheckIfEnemyCanSpawn;
        EventChannels.EnemyEvents.OnEnemyDeath += RemoveFromCounter;
    }

    private void OnDestroy()
    {
        EventChannels.EnemyEvents.OnSpawnEnemy -= CheckIfEnemyCanSpawn;
        EventChannels.EnemyEvents.OnEnemyDeath -= RemoveFromCounter;
    }

    bool CheckIfEnemyCanSpawn()
    {
        if (currentAmountOfEnemies < maxAmountOfEnemies)
        {
            AddToCounter();
            Debug.Log("can spawn");
            return true;
        }
        return false;
    }

    void AddToCounter()
    {
        currentAmountOfEnemies++;
    }

    void RemoveFromCounter(GameObject go)
    {
        currentAmountOfEnemies--;
    }
}
