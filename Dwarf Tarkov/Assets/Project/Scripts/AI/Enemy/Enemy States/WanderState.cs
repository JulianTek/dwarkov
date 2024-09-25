using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;

namespace AI
{
    public class WanderState : GameState
    {
        private float wanderCooldown;

        public WanderState() : base() { }
        public WanderState(GameObject owner) : base(owner) { }

        public override void Start()
        {
            owner.GetComponent<NavMeshAgent>().isStopped = false;
            wanderCooldown = Random.Range(1f, 7f);
            Debug.Log($"{owner} entered wander state");
        }

        public override void Stop()
        {
            
        }

        public override void Update()
        {
            wanderCooldown -= Time.deltaTime;
            if (wanderCooldown > 0)
                return;

            Debug.Log(owner);
            EventChannels.EnemyEvents.OnEnemyWander?.Invoke(owner);
            wanderCooldown = Random.Range(1f, 7f);
        }

    }
}

