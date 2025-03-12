using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class EntityTriggerEnvironmentFeature : MonoBehaviour, IEnvironmentFeature
{
    private GameObject entityInTrigger;
    protected bool playerInTrigger = false;

    [SerializeField]
    private float timeInterval;
    private float timePassed;

    // Update is called once per frame
    void Update()
    {
        while (entityInTrigger != null)
        {
            InteractWithEnvironment();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<PlayerInputHandler>()  != null || GetComponent<EnemyStateMachine>() != null)
        {
            entityInTrigger = other.gameObject;
            playerInTrigger = GetComponent<PlayerInputHandler>() != null ? true : false;
        }
    }

    public virtual void EnvironmentInteraction()
    {
        // do nothing, let subclasses handle this
    }

    public void InteractWithEnvironment()
    {
        timePassed += Time.deltaTime;
        if (timePassed < timeInterval)
            return;
        EnvironmentInteraction();
    }
}
