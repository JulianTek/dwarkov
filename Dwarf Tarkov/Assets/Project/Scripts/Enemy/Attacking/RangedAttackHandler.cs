using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class RangedAttackHandler : EnemyAttackHandler
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float projectileSpeed;

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
        GameObject go = ObjectPoolHandler.SpawnObject(projectile, transform.position, Quaternion.identity);
        go.transform.LookAt(EventChannels.PlayerEvents.OnGetPlayerPosition?.Invoke());
        go.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed)
    }
}
