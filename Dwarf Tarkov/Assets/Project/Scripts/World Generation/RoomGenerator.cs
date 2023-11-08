using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

namespace WorldGeneration
{
    public class RoomGenerator : MonoBehaviour
    {
        private WalkGenerator walkGenerator;
        [SerializeField]
        private TileMapVisualizer tileMapVisualizer;

        [SerializeField]
        private Vector2Int startPosition;

        [SerializeField]
        private int iterations = 10;
        [SerializeField]
        private int length = 10;
        [SerializeField]
        private bool randomPositionEachIteration = true;

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            HashSet<Vector2Int> positions = RunGenerationAlgorithm();
            tileMapVisualizer.Clear();
            tileMapVisualizer.PaintFloorTiles(positions);
            WallGenerator.PaintWallTiles(tileMapVisualizer, positions);
        } 

        public HashSet<Vector2Int> RunGenerationAlgorithm()
        {
            walkGenerator = new WalkGenerator();
            var currentPosition = startPosition;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < iterations; i++)
            {
                var path = walkGenerator.Generate(startPosition, length);
                floorPositions.UnionWith(path);
                if (randomPositionEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
            return floorPositions;
        }
    }
}

