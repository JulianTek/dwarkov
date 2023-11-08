using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class StateMachine : MonoBehaviour
    {
        protected static GameState currentState;
        protected static GameState nextState;
        protected static bool exitStateNextFrame;
         
        private void Start()
        {
            exitStateNextFrame = false;
        }

        public static void SwitchState(GameState state)
        {
            exitStateNextFrame = true;
            nextState = state;

        }
        public static void SwitchState<T>() where T : GameState, new()
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
            exitStateNextFrame = false;

        }

        public GameState GetGameState()
        {
            return currentState;
        }
    }
}

