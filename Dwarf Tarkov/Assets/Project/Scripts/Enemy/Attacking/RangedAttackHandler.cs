using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackHandler : EnemyAttackHandler
{
    [SerializeField]
    private GameObject projectile;
    public override void Attack()
    {
        base.Attack();
    }
}
