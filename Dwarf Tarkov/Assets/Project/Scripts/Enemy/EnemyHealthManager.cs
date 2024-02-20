using EventSystem;
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
        Debug.Log(enemyCurrentHealth);
        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
            EventChannels.EnemyEvents.OnEnemyDeath?.Invoke();
        }
    }

    public float GetEnemyHealth()
    {
        return enemyCurrentHealth;
    }
}
