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
        DontDestroyOnLoad(gameObject);
        EventChannels.PlayerEvents.OnExperienceGained += AddExperiencePointsToPool;
        EventChannels.GameplayEvents.OnPlayerExtracted += SetXPPoolText;
    }
    private void OnDestroy()
    {
        EventChannels.PlayerEvents.OnExperienceGained -= AddExperiencePointsToPool;
        EventChannels.GameplayEvents.OnPlayerExtracted -= SetXPPoolText;
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
    }

    public int GetPooledXP()
    {
        return experiencePointsPooled;
    }

    public void ResetXPPool()
    {
        experiencePointsPooled = 0;
    }

    private void SetXPPoolText()
    {
        EventChannels.UIEvents.OnSetXPPoints?.Invoke(experiencePointsPooled);
        AwardPooledExperience();
        ResetXPPool();
    }
}
