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
        EventChannels.DataEvents.OnGetPlayerHealth += GetHealth;
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
        EventChannels.PlayerEvents.OnPlayerHeal += InvokeHealing;
    }

    private void OnDestroy()
    {
        EventChannels.DataEvents.OnGetPlayerHealth -= GetHealth;
        EventChannels.EnemyEvents.OnEnemyAttack -= TakeDamage;
        EventChannels.PlayerEvents.OnPlayerHeal -= InvokeHealing;
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

    public void InvokeHealing(float healValue, float timeToHeal, bool healOverTime)
    {
        if (healOverTime)
            StartCoroutine(HealOverTimeCoroutine(healValue, timeToHeal));
        else
            StartCoroutine(HealCoroutine(healValue, timeToHeal));
    }

    IEnumerator HealCoroutine(float healValue, float timeToHeal)
    {
        EventChannels.PlayerInputEvents.OnSetMovement?.Invoke(false);
        yield return new WaitForSecondsRealtime(timeToHeal);
        playerHealth = Mathf.Clamp(playerHealth + healValue, .1f, maxHealth);
        EventChannels.UIEvents.OnUpdateHealthbar?.Invoke(playerHealth);
        EventChannels.PlayerInputEvents.OnSetMovement?.Invoke(true);
    }

    IEnumerator HealOverTimeCoroutine(float healValue, float timeToHeal)
    {
        EventChannels.PlayerInputEvents.OnSetMovement?.Invoke(false);
        float elapsedTime = 0;
        float startHealth = playerHealth;
        float target = Mathf.Clamp(playerHealth + healValue, .1f, maxHealth);

        while (elapsedTime < timeToHeal)
        {
            elapsedTime += Time.deltaTime;
            playerHealth = Mathf.Lerp(startHealth, target, elapsedTime / timeToHeal);
            EventChannels.UIEvents.OnUpdateHealthbar?.Invoke(playerHealth);
            yield return null;
        }
        EventChannels.PlayerInputEvents.OnSetMovement?.Invoke(true);
    }
}
