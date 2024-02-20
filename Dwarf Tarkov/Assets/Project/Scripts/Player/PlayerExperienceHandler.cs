using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PlayerExperienceHandler : MonoBehaviour
{
    [SerializeField]
    private int experiencePoints;
    private int playerLevel;
    [SerializeField]
    [Header("Amount of experience for first level up")]
    private int xpNeededToLevelUp;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = 1;
        EventChannels.PlayerEvents.OnExperienceGiven += AddExperience;
        EventChannels.PlayerEvents.OnGetPlayerLevel += GetPlayerLevel;
        EventChannels.DataEvents.OnGetPlayerLevel += GetPlayerLevel;
        EventChannels.DataEvents.OnGetPlayerExperience += GetExperience;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerEvents.OnExperienceGiven -= AddExperience;
        EventChannels.PlayerEvents.OnGetPlayerLevel -= GetPlayerLevel;
        EventChannels.DataEvents.OnGetPlayerLevel -= GetPlayerLevel;
        EventChannels.DataEvents.OnGetPlayerExperience -= GetExperience;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddExperience(int experience)
    {
        experiencePoints += experience;
        if (experiencePoints >= xpNeededToLevelUp)
            LevelUp();
    }

    public void LevelUp()
    {
        playerLevel++;
        xpNeededToLevelUp += 50 * (playerLevel + 1);
    }

    private int GetPlayerLevel()
    {
        return playerLevel;
    }

    private int GetExperience()
    {
        return experiencePoints;
    }
}
