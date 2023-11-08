using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class PlayerStartPositionMover : MonoBehaviour
{

    private void Start()
    {
        EventChannels.WorldGenerationEvents.OnMovePlayer += MovePlayer;
    }

    void MovePlayer(Vector2Int position)
    {
        transform.position = new Vector3(position.x, position.y);
    }
}
