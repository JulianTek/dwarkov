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
        private Vector2 playerPos;

        public SpottedPlayerState() : base() { }
        public SpottedPlayerState(Vector2 playerPos)
        { 
            this.playerPos = playerPos;
        }

        public override void Start()
        {
            Debug.Log($"{owner} entered player spotted state and is chasing {playerPos}"); 
            timeLeft = spotCooldown;
            EventChannels.EnemyEvents.OnPlayerSpotted?.Invoke(playerPos, owner);
        }

        public override void Stop()
        {
            timeLeft = float.MaxValue;
        }

        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft > 0 || Vector2.Distance(owner.transform.position, playerPos) >= 0.5f)
                return;
            EventChannels.EnemyEvents.OnEnemyLoseInterest?.Invoke(owner);
        }
    }
}
