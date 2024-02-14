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
        EventChannels.PlayerEvents.OnExperienceGiven += AddExperience;
    }

    private void OnDestroy()
    {
        EventChannels.PlayerEvents.OnExperienceGiven -= AddExperience;
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

    private void LevelUp()
    {
        playerLevel++;
        xpNeededToLevelUp += 50 * playerLevel;
    }
}
