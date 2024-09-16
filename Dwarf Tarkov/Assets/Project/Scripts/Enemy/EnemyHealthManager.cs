using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private float EnemyMaxHealth = 50f;
    private float enemyCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = EnemyMaxHealth;
        EventChannels.EnemyEvents.OnEnemyTakesDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        EventChannels.EnemyEvents.OnEnemyTakesDamage -= TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float value)
    {
        enemyCurrentHealth -= value;
        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
            EventChannels.EnemyEvents.OnEnemyDeath?.Invoke(gameObject);
            EventChannels.EnemyEvents.OnEnemyDeathWithName?.Invoke(GetEnemyName());
        }
    }

    private string GetEnemyName()
    {
        return gameObject.name[..^7];
    }

    public float GetEnemyHealth()
    {
        return enemyCurrentHealth;
    }
}
