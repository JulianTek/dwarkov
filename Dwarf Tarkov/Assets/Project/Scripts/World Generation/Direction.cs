using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public static class Direction
    {
        private static List<Vector2Int> directions = new List<Vector2Int>()
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
    };

        public static List<Vector2Int> Directions { get => directions;}

        public static Vector2Int GetRandomDirection()
        {
            return directions[Random.Range(0, directions.Count)];
        }
    }
}
