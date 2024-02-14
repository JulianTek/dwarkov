using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class ExperiencePointsPoolHandler : MonoBehaviour
{
    [SerializeField]
    private int experiencePointsPooled;
    // Start is called before the first frame update
    void Start()
    {
        EventChannels.PlayerEvents.OnExperienceGained += AddExperiencePointsToPool;
        EventChannels.ExtractionEvents.OnExtractionTimerFinished += AwardPooledExperience;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddExperiencePointsToPool(int xp)
    {
        experiencePointsPooled += xp;
    }

    void AwardPooledExperience()
    {
        EventChannels.PlayerEvents.OnExperienceGiven?.Invoke(experiencePointsPooled);
        experiencePointsPooled = 0;
    }

    public int GetPooledXP()
    {
        return experiencePointsPooled;
    }
}
