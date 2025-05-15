using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class EnemyProjectileHandler : MonoBehaviour
{
    private float damage;
    private Transform origin;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float projectileSpeed;

    private Transform target;

    private void Start()
    {
        target = EventChannels.PlayerEvents.OnGetPlayerPosition?.Invoke();
        GetComponent<Rigidbody2D>().AddForce(transform.InverseTransformPoint(target.position) * projectileSpeed, ForceMode2D.Impulse);
    }

    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.GetComponent<PlayerInputHandler>())
        {
            EventChannels.EnemyEvents.OnEnemyAttack?.Invoke(damage);
            ObjectPoolHandler.ReturnObjectToPool(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetOrigin(Transform origin)
    {
        this.origin = origin;
    }
}
