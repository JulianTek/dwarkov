using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public interface IGenerator
    {
        public IEnumerable<Vector2Int> Generate(Vector2Int startPosition, int length);
    }
}

