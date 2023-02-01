using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

public class OutpostDoorHandler : MonoBehaviour
{
    [HorizontalGroup("Stuff")]
    [BoxGroup("Stuff/Transforms", LabelText = "Transforms")]
    [SerializeField]
    private Vector3 bedroomTransform;
    [BoxGroup("Stuff/Transforms", LabelText = "Transforms")]
    [SerializeField]
    private Vector3 outpostTransform;
    [BoxGroup("Stuff/TileInfo", LabelText = "TileInfo")]
    [SerializeField]
    private Tilemap doorTileMap;
    [BoxGroup("Stuff/TileInfo", LabelText = "TileInfo")]
    [SerializeField]
    private TileBase doorClosed;
    [BoxGroup("Stuff/TileInfo", LabelText = "TileInfo")]
    [SerializeField]
    private TileBase doorOpen;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            doorTileMap.SetTile(Vector3Int.FloorToInt(transform.position), doorOpen);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
