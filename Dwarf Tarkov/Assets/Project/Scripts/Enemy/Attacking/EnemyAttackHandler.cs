using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : IEnemyAttackHandler
{
    [SerializeField]
    private float damage;

    public virtual void Attack()
    {}
}
