using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.AI;
using EventSystem;

public class InvestigateSoundState : GameState
{
    public Transform soundSource { get; private set; }
    public InvestigateSoundState() : base() { }

    public InvestigateSoundState(GameObject owner) : base(owner) { }

    public InvestigateSoundState(Transform transform) : base()
    {
        soundSource = transform;
    }

    public override void Start()
    {
        EventChannels.EnemyEvents.OnPlayerSpotted?.Invoke(soundSource.position, owner);
    }

    public override void Stop()
    {
        soundSource = null;
    }

    public override void Update()
    {
        if (Vector2.Distance(owner.transform.position, soundSource.position) < 0.5f)
        {
            EventChannels.EnemyEvents.OnEnemyLoseInterest?.Invoke(owner);
            return;
        }
    }
}
