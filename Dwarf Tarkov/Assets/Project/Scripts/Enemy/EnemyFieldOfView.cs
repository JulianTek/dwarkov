using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float fov = 90f;
    [SerializeField]
    private int rayCount = 2;
    [SerializeField]
    private float angle = 0f;
    private float angleIncrease;
    [SerializeField]
    private float viewDistance = 50f;

    private Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;  
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(MathF.Cos(angleRad), MathF.Sin(angleRad));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        angle = 0f;
        Vector3 origin = transform.position;
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
            RaycastHit2D hit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance);
            Debug.DrawRay(origin, GetVectorFromAngle(angle), Color.green);
            Debug.Log(origin);
            if (!hit.collider)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = hit.point; 
            }
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
}
