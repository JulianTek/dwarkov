using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using System;

public class EnemyExperienceHandler : MonoBehaviour
{
    [SerializeField]
    private int experienceGainedOnDeath;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.EnemyEvents.OnEnemyDeath += AwardExperience;
    }

    private void AwardExperience()
    {
        EventChannels.PlayerEvents.OnExperienceGained?.Invoke(experienceGainedOnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
