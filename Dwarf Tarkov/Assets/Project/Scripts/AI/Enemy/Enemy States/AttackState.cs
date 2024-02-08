using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace AI
{
    public class AttackState : GameState
    {
        private float attackCooldown = 3f;
        private float timeLeft;

        public AttackState() : base() { }

        public AttackState(GameObject owner) : base(owner) { }

        public override void Start()
        {
            timeLeft = attackCooldown;
            Debug.Log("Attacking..");
        }

        public override void Stop()
        {
            
        }

        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            Debug.Log(timeLeft);
            if (timeLeft > 0)
                return;

            EventChannels.EnemyEvents.OnEnemyAttack?.Invoke(10f);
            timeLeft = attackCooldown;
        }
    }
}
