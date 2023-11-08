using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public class WalkGenerator : IGenerator
    {
        /// <summary>
        /// Generates based on a simple walk algorithm
        /// </summary>
        /// <param name="startPosition">The position the algorithm will start</param>
        /// <param name="length">The length of the generated floor</param>
        /// <returns>Positions of the floor</returns>
        public IEnumerable<Vector2Int> Generate(Vector2Int startPosition, int length)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>();
            path.Add(startPosition);
            var previousPosition = startPosition;

            for(int i = 0; i < length; i++)
            {
                var newPosition = previousPosition + Direction.GetRandomDirection();
                path.Add(newPosition);
                previousPosition = newPosition;
            }
            return path;
        }
    }
}

