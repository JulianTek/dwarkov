using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour, IEnemyAttackHandler
{
    [SerializeField]
    protected float damage;
    protected bool playerInTrigger;

    [SerializeField]
    protected float cooldown;
    protected float timePassed;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            Attack();
            playerInTrigger = true;
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (playerInTrigger && other.GetComponent<PlayerInputHandler>())
            playerInTrigger = false;
    } 

    public virtual void Attack()
    {}
}
