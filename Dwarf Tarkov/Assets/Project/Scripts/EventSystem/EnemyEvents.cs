using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents
{
    public delegate void StateEvent(GameState state);
    public delegate void EnemyPositionEvent(Vector3 position);
    public delegate void EnemyEvent();
    public delegate void DamageEvent(float damage);

    public StateEvent OnSwitchEnemyState;

    public EnemyPositionEvent OnPlayerSpotted;

    public EnemyEvent OnEnemyStopMoving;
    public EnemyEvent OnEnemyWander;

    public DamageEvent OnEnemyAttack;
    public DamageEvent OnEnemyTakesDamage;
}