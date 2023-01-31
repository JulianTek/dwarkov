using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 positionToMoveTo;
    private NavMeshAgent agent;
    [SerializeField]
    private float distanceThreshold;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        EventChannels.EnemyEvents.OnPlayerSpotted += MoveToPosition;
        EventChannels.EnemyEvents.OnEnemyWander += Wander;
    }

    private void OnDestroy()
    {
        EventChannels.EnemyEvents.OnPlayerSpotted -= MoveToPosition;
        EventChannels.EnemyEvents.OnEnemyWander -= Wander;
    }

    // Update is called once per frame
    void Update()
    {
        if (positionToMoveTo != null)
        {
            if (Vector3.Distance(transform.position, positionToMoveTo) < distanceThreshold)
            {
                EventChannels.EnemyEvents.OnEnemyStopMoving?.Invoke();
                StopMoving();
            }
        }
    }

    void MoveToPosition(Vector3 pos)
    {
        positionToMoveTo = pos;
        agent.SetDestination(pos);
    }

    void StopMoving()
    {
        agent.isStopped = true;
    }

    void Wander()
    {
        agent.SetDestination(new Vector3(transform.position.x + Random.Range(0.2f, 0.6f), transform.position.y + Random.Range(0.2f, 0.6f)));
    }
}
