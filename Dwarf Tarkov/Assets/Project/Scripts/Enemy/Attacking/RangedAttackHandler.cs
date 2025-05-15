using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class RangedAttackHandler : EnemyAttackHandler
{
    [SerializeField]
    private GameObject projectile;

    private void Update()
    {
        if (!playerInTrigger)
            return;

        timePassed += Time.deltaTime;
        if (timePassed >= cooldown)
        {
            Attack();
            timePassed = 0;
        }
    }

    public override void Attack()
    {
        FireProjectile(damage);
    }

    void FireProjectile(float damage)
    {
        GameObject go = ObjectPoolHandler.SpawnObject(projectile, transform.position, transform.rotation);
        EnemyProjectileHandler enemyProjectileHandler = go.GetComponent<EnemyProjectileHandler>();
        enemyProjectileHandler.SetDamage(damage);
        enemyProjectileHandler.SetOrigin(transform);

    }
}
