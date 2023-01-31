using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField]
    private float playerHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.EnemyEvents.OnEnemyAttack += TakeDamage;
    }

    private void OnDestroy()
    {
        EventChannels.EnemyEvents.OnEnemyAttack -= TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            // invoke death event;
            Debug.Log("dead");
        }
        EventChannels.UIEvents.OnUpdateHealthbar?.Invoke(playerHealth);
    }
}
