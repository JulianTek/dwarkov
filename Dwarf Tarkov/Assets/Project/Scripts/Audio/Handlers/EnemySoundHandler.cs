using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class EnemySoundHandler : SoundHandler
{
    private EnemyStateMachine enemyStateMachine;

    public new void Start()
    {
        enemyStateMachine = GetComponentInParent<EnemyStateMachine>();
    }

    protected override void OnHeardEvent(Transform transform)
    {
        enemyStateMachine.SwitchState<InvestigateSoundState>(transform.position);
    }
}
