using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.SceneManagement;
using Data;
public class PlayerHealthManager : MonoBehaviour
{
    private float maxHealth = 100f;
    private float playerHealth;

    private void Awake()
    {
        SaveData data = EventChannels.DataEvents.OnGetSaveData?.Invoke();
        if (data == null)
        {
            playerHealth = maxHealth;
            return;
        }
        playerHealth = data.PlayerHealth;
    }

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

    public float GetHealth()
    {
        return playerHealth;
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

    void Heal(float healValue)
    {
        playerHealth = Mathf.Clamp(playerHealth + healValue, .1f, maxHealth);
    }
}
