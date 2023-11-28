using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using AI;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float fov = 90f;
    [SerializeField]
    private int rayCount = 10;
    [SerializeField]
    private float angle = 0f;
    private float angleIncrease;
    [SerializeField]
    private float viewDistance = 5f;
    private Vector3 origin;
    private float startingAngle;

    // int is -1 if mesh should be flipped
    private int isFlipped;

    private Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        Vector3 vector = new Vector3(MathF.Cos(angleRad), MathF.Sin(angleRad));
        return RotateVector(vector, -90f);
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GenerateFOV();
    }

    void GenerateFOV()
    {
        angle = startingAngle;
        origin = transform.localPosition;
        angleIncrease = fov / rayCount;


        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, GetVectorFromAngle(angle), viewDistance, layerMask);
            Debug.DrawRay(transform.position, GetVectorFromAngle(angle), Color.green);
            if (!hit.collider)
            {
                vertex = transform.localPosition + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                if (hit.transform.gameObject.GetComponent<PlayerInputHandler>())
                {
                    var currentState = GetComponentInParent<EnemyStateMachine>().GetGameState().GetType();
                    if (currentState == typeof(SpottedPlayerState) || currentState == typeof(WanderState))
                    {
                        EventChannels.EnemyEvents.OnSwitchEnemyState?.Invoke(new SpottedPlayerState());
                    }
                    EventChannels.EnemyEvents.OnPlayerSpotted?.Invoke(hit.point);
                }
                vertex = hit.point.normalized;
            }
            //vertex.x *= isFlipped;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;

        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;

        return n;
    }

    public float GetDistance()
    {
        return viewDistance;
    }

    public float GetFOVAngle()
    {
        return fov;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 dir)
    {
        startingAngle = GetAngleFromVectorFloat(dir) - fov / 2f;
    }

    Vector3 RotateVector(Vector3 vector, float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;

        return new Vector3(x, y, 0f); // Assuming your vectors are in 2D space
    }
}
