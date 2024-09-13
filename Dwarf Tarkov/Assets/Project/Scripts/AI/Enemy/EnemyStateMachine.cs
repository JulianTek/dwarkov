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
            owner = gameObject;
            currentState = new WanderState(owner);
        }
    }
}

