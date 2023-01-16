using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void SwitchState(GameState state)
        {
            exitStateNextFrame = true;
            nextState = state;

        }
        public void SwitchState<T>() where T : GameState, new()
        {
            SwitchState(new T());
        }

        protected void Update()
        {
            currentState.Update();

            if (!exitStateNextFrame)
                return;

            currentState.Stop();
            currentState = nextState;
            currentState.Start();

        }
    }
}

