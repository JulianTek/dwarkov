using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.SceneManagement;

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
            Die();
            return;
        }
        EventChannels.UIEvents.OnUpdateHealthbar?.Invoke(playerHealth);
    }

    void Die()
    {
        EventChannels.PlayerEvents.OnPlayerDeath?.Invoke();
    }
}
