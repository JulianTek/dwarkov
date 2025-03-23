using System.Collections;
using System.Collections.Generic;
using AI;
using EventSystem;
using UnityEngine;

public class EntityTriggerEnvironmentFeature : MonoBehaviour, IEnvironmentFeature
{
    private GameObject entityInTrigger;
    protected bool playerInTrigger = false;

    [SerializeField]
    private float timeInterval;
    private float timePassed = 0f;

    // Update is called once per frame
    void Update()
    {
        if (entityInTrigger != null)
        {
            InteractWithEnvironment();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>()  != null || other.GetComponent<EnemyStateMachine>() != null)
        {
            entityInTrigger = other.gameObject;
            playerInTrigger = other.GetComponent<PlayerInputHandler>() != null ? true : false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        timePassed = 0f;
        entityInTrigger = null;
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
        timePassed = 0f;
    }
}
