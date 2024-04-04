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

        private void SwitchState(GameState state)
        {
            exitStateNextFrame = true;
            nextState = state;
        }
        public void SwitchState<T>(GameObject owner) where T : GameState
        {
            // Switches state to a new state. Use new <x>() where <x> is one of the GameState classes
            SwitchState((T)Activator.CreateInstance(typeof(T), owner));
        }

        // This is the same as a monobehaviour's start/update/stop loop. Every state also has their own start/update/stop function which will be called at appropriate times
        private void Start()
        {
            exitStateNextFrame = false;
        }

        protected void Update()
        {
            currentState.Update();

            // if not exiting the current state next frame, do nothing
            if (!exitStateNextFrame)
                return;

            // otherwise stop the current state, set the state to "nextState", run the start method and continue
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

