using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventSystem
{
    public class WorldGenerationEvents
    {
        public delegate void LocationEvent(Vector2Int pos);

        public LocationEvent OnMovePlayer;
    }
}

