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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float value)
    {
        enemyCurrentHealth -= value;

        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
