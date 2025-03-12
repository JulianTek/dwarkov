using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ToxicBogHandler : EntityTriggerEnvironmentFeature 
{
    [SerializeField]
    private float damage;
    public override void EnvironmentInteraction()
    {
        if (playerInTrigger)
            EventChannels.EnemyEvents.OnEnemyAttack?.Invoke(damage);
        else
            EventChannels.EnemyEvents.OnEnemyTakesDamage?.Invoke(damage);
    }
}
