using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void PlayerEvent();
    public delegate void ExperienceEvent(int experience);
    public delegate int PlayerLevelEvent();

    public PlayerEvent OnPlayerDeath;
    
    // use this to pool experience on raids
    public ExperienceEvent OnExperienceGained;
    // use this to award experience at the end of a raid
    public ExperienceEvent OnExperienceGiven;

    public PlayerLevelEvent OnGetPlayerLevel;
}
