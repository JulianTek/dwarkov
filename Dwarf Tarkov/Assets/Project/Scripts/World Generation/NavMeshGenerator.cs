using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
        surface.RemoveData();
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
