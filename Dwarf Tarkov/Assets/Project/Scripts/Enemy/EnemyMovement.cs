using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;
using AI;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 positionToMoveTo;
    private Vector3 lastMoveDir;
    private NavMeshAgent agent;
    [SerializeField]
    private float distanceThreshold;
    private EnemyFieldOfView fieldOfView;

    private float baseSpeed;
    private float movementMultiplier = 1f;
  

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponentInChildren<EnemyFieldOfView>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        EventChannels.EnemyEvents.OnPlayerSpotted += MoveToPosition;
        EventChannels.EnemyEvents.OnEnemyWander += Wander;
        EventChannels.EnemyEvents.OnEnemyLoseInterest += LoseInterest;
        EventChannels.EnemyEvents.OnChangeEnemyMovementSpeed += ChangeMovementSpeed;
        baseSpeed = agent.speed;
    }

    private void OnDestroy()
    {
        EventChannels.EnemyEvents.OnPlayerSpotted -= MoveToPosition;
        EventChannels.EnemyEvents.OnEnemyWander -= Wander;
        EventChannels.EnemyEvents.OnEnemyLoseInterest -= LoseInterest;
        EventChannels.EnemyEvents.OnChangeEnemyMovementSpeed -= ChangeMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (positionToMoveTo != null)
        {
            if (transform.GetComponentInParent<EnemyStateMachine>().GetGameState().GetType() == typeof(SpottedPlayerState) && Vector3.Distance(transform.position, positionToMoveTo) < distanceThreshold)
            {
                Debug.Log("not moving");
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
        if (transform.GetComponentInParent<EnemyStateMachine>().GetGameState().GetType() == typeof(SpottedPlayerState))
            LoseInterest(gameObject);
    }

    void Wander(GameObject go)
    {
        Debug.Log(go);
        if (gameObject == go)
        {
            float xOffset = Random.Range(-2f, 2f);
            var yOffset = Random.Range(-2f, 2f);
            Vector3 endPoint = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset);
            lastMoveDir = (transform.position - endPoint).normalized;
            agent.SetDestination(endPoint);
            Debug.Log(endPoint);
            Debug.Log(transform.position);
            fieldOfView.SetAimDirection(GetAimDir());
            fieldOfView.SetOrigin(transform.position);
        }
    }

    void LoseInterest(GameObject go)
    {
        if (gameObject == go)
        {
            var stateMachine = transform.GetComponentInParent<EnemyStateMachine>();
            stateMachine.SwitchState<WanderState>(); 
        }
    }

    Vector3 GetAimDir()
    {
        return lastMoveDir;
    }

    private void ChangeMovementSpeed(float value)
    {
        if (value == 1f)
            agent.speed = baseSpeed;
        else
            agent.speed = value;
    }
}
