using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField]
    private float maxDistance;

    private Vector3 startPos;
    private Vector3 currentPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        currentPos = transform.position;

        if (Vector2.Distance(startPos, currentPos) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
