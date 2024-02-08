using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 positionToMoveTo;
    private Vector3 lastMoveDir;
    private NavMeshAgent agent;
    [SerializeField]
    private float distanceThreshold;
    private EnemyFieldOfView fieldOfView;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponentInChildren<EnemyFieldOfView>();
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

    void MoveToPosition(Vector3 pos, GameObject go)
    {
        if (gameObject == go)
        {
            positionToMoveTo = pos;
            agent.SetDestination(pos);
        }
    }

    void StopMoving()
    {
        agent.isStopped = true;
    }

    void Wander(GameObject go)
    {
        if (gameObject == go)
        {
            float xOffset = Random.Range(-2f, 2f);
            var yOffset = Random.Range(-2f, 2f);
            Vector3 endPoint = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset);
            lastMoveDir = (transform.position - endPoint).normalized;
            agent.SetDestination(endPoint);

            fieldOfView.SetAimDirection(GetAimDir());
            fieldOfView.SetOrigin(transform.position);
        }
    }

    Vector3 GetAimDir()
    {
        return lastMoveDir;
    }
}
