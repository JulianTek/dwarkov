using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

public class OutpostDoorHandler : MonoBehaviour
{
    [HorizontalGroup("Stuff", LabelWidth = 60)]
    [BoxGroup("Stuff/Transforms", LabelText = "Transforms")]
    [SerializeField]
    private Vector3 bedroomTransform;
    [BoxGroup("Stuff/TileInfo", LabelText = "TileInfo")]
    [SerializeField]
    private Tilemap doorTileMap;
    [BoxGroup("Stuff/TileInfo", LabelText = "TileInfo")]
    [SerializeField]
    private TileBase doorClosed;
    [BoxGroup("Stuff/TileInfo", LabelText = "TileInfo")]
    [SerializeField]
    private TileBase doorOpen;
    private bool doorOpened;
    private Vector3Int doorPosition;

    private void Start()
    {
        doorOpened = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>() && !doorOpened)
        {
            Vector3Int pos = Vector3Int.FloorToInt(other.transform.position);
            Vector3Int doorPos = new Vector3Int(pos.x, pos.y + 1);
            doorPosition = doorPos;
            SetTile(doorOpen, doorPosition);
            StartCoroutine(TransformPlayer(other.gameObject, bedroomTransform));
            doorOpened = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInputHandler>())
        {
            StopAllCoroutines();
            SetTile(doorClosed, doorPosition);
            doorOpened = false;
        }
    }

    private IEnumerator TransformPlayer(GameObject player, Vector3 pos)
    {
        yield return new WaitForSecondsRealtime(1f);
        player.transform.position = pos;
    }

    private void SetTile(TileBase tile, Vector3Int pos)
    {
        doorTileMap.SetTile(pos, tile);
        doorTileMap.RefreshAllTiles();
    }
}
