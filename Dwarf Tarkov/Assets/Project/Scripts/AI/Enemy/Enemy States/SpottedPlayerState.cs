using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace AI
{
    public class SpottedPlayerState : GameState
    {
        private float spotCooldown = 7f;
        private float timeLeft;
        public override void Start()
        {
            timeLeft = spotCooldown;
        }

        public override void Stop()
        {
            timeLeft = float.MaxValue;
        }

        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft > 0)
                return;

            EventChannels.EnemyEvents.OnSwitchEnemyState?.Invoke(new WanderState());
        }
    }
}
