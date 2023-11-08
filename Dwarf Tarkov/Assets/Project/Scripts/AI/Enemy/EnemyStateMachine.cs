using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;

namespace AI
{
    public class EnemyStateMachine : StateMachine
    {
        public void Start()
        {
            currentState = new WanderState();
            EventChannels.EnemyEvents.OnSwitchEnemyState += SwitchState;
        }

        private void OnDestroy()
        {
            EventChannels.EnemyEvents.OnSwitchEnemyState -= SwitchState;
        }
    }
}

