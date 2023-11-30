using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AI
{
    public class StateMachine : MonoBehaviour
    {
        protected GameState currentState;
        protected GameState nextState;
        protected bool exitStateNextFrame;

        private void Start()
        {
            exitStateNextFrame = false;
        }

        private void SwitchState(GameState state)
        {
            exitStateNextFrame = true;
            nextState = state;
        }
        public void SwitchState<T>(GameObject owner) where T : GameState
        {
            SwitchState((T)Activator.CreateInstance(typeof(T), owner));
        }

        protected void Update()
        {
            currentState.Update();

            if (!exitStateNextFrame)
                return;

            currentState.Stop();
            currentState = nextState;
            currentState.Start();
            exitStateNextFrame = false;

        }

        public GameState GetGameState()
        {
            return currentState;
        }
    }
}

