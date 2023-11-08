using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public class CorridorGenerator : IGenerator
    {
        public IEnumerable<Vector2Int> Generate(Vector2Int startPosition, int length)
        {
            List<Vector2Int> corridor = new List<Vector2Int>();
            var direction = Direction.GetRandomDirection();
            var currentPosition = startPosition;
            corridor.Add(currentPosition);
            for (int i = 0; i < length; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition);
            }
            return corridor;
        }
    }
}

