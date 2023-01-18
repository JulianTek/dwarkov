using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField]
    private float fov = 90f;
    [SerializeField]
    private int rayCount = 2;
    [SerializeField]
    private float angle = 0f;
    private float angleIncrease;
    [SerializeField]
    private float viewDistance = 50f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 origin = Vector3.zero;
        angleIncrease = fov / rayCount;
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


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

            angle -= angleIncrease;
            vertexIndex++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(MathF.Cos(angleRad), MathF.Sin(angleRad));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
