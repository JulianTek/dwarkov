using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldGeneration
{
    public static class ExtractionPointsGenerator
    {
        public static Vector2Int GenerateExtractionSpots(HashSet<Vector2Int> floorPositions)
        {
            int randomIndex = Random.Range(0, floorPositions.Count);
            return floorPositions.ElementAt(randomIndex);
        }
    }
}

