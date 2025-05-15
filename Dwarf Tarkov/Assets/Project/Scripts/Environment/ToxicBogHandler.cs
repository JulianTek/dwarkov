using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ToxicBogHandler : EntityTriggerEnvironmentFeature 
{
    [SerializeField]
    private float damage;
    [Tooltip("Input what you want the NEW speed multiple to be for entities (eg 0.5 if yoy want them to move half as fast")]
    [SerializeField]
    private float slowMultiplier;
    public override void EnvironmentInteraction()
    {
        if (playerInTrigger)
        {
            EventChannels.EnemyEvents.OnEnemyAttack?.Invoke(damage);
        }

        else
        {
            EventChannels.EnemyEvents.OnEnemyTakesDamage?.Invoke(damage);
        }
    }

    public new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (playerInTrigger)
            EventChannels.PlayerEvents.OnPlayerChangeMoveSpeed?.Invoke(slowMultiplier);
        else
            EventChannels.EnemyEvents.OnChangeEnemyMovementSpeed?.Invoke(slowMultiplier);
    }

    public new void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (playerInTrigger)
            EventChannels.PlayerEvents.OnPlayerChangeMoveSpeed.Invoke(1f);
        else
            EventChannels.EnemyEvents.OnChangeEnemyMovementSpeed.Invoke(1f);
    }
}
