using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public interface IGenerator
    {
        public HashSet<Vector2Int> Generate(Vector2Int startPosition, int length);
    }
}

