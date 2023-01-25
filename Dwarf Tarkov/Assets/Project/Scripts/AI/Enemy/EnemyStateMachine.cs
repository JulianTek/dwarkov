using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

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

