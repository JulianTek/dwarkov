using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents
{
    public delegate void StateEvent(GameState state, GameObject go);
    public delegate void EnemyPositionEvent(Vector3 position);
    public delegate void EnemyEvent();
    public delegate void EnemyObjectEvent(GameObject go);
    public delegate void DamageEvent(float damage);
    public delegate bool EnemySpawnEvent();

    public StateEvent OnSwitchEnemyState;

    public EnemyPositionEvent OnPlayerSpotted;

    public EnemyEvent OnEnemyStopMoving;
    public EnemyObjectEvent OnEnemyWander;

    public DamageEvent OnEnemyAttack;
    public DamageEvent OnEnemyTakesDamage;

    public EnemyEvent OnEnemyDeath;

    public EnemySpawnEvent OnSpawnEnemy;
}
