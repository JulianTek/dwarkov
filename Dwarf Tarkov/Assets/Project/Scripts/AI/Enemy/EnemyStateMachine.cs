using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;

namespace AI
{
    public class EnemyStateMachine : StateMachine
    {
        public void SwitchState<T>(Vector2 playerPos) where T: GameState, new()
        {
            GameState state = new SpottedPlayerState(playerPos);
            state.SetOwner(owner);
            SwitchState(state);
        } 

        public void Start()
        {
            owner = gameObject;
            currentState = new WanderState(owner);
        }
    }
}

