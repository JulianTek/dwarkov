using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using AI;

public class EnemyAttackManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            EventChannels.EnemyEvents.OnEnemyAttack?.Invoke(25f);
            transform.parent.GetComponent<EnemyStateMachine>().SwitchState<AttackState>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {   
        if (other.GetComponent<PlayerInputHandler>())
        {
            transform.parent.GetComponent<EnemyStateMachine>().SwitchState<SpottedPlayerState>(other.transform.position);
        }
    }
}
