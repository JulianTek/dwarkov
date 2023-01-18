using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class EnemyStateMachine : StateMachine
    {
        public void Start()
        {
            currentState = new WanderState();
        }
    }
}

