using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField]
    private float soundRadius = 3f;
    [SerializeField]
    private float duration = 0.5f;
    private float timeElapsed;
    private CircleCollider2D soundCollider;
    // Start is called before the first frame update

    void Awake()
    {
        soundCollider = GetComponent<CircleCollider2D>();
        soundCollider.radius = soundRadius;
        soundCollider.enabled = false;
        timeElapsed = 0f;
    }

    protected void EnableCollider()
    {
        Debug.Log("Collider enabled");
        soundCollider.enabled = true;
    }

    protected void DisableCollider()
    {
        soundCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (soundCollider.enabled)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= duration)
            {
                DisableCollider();
                timeElapsed = 0f;
            }
        }
    }
}
