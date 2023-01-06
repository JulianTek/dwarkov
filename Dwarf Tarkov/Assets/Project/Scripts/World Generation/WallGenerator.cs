using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public static class WallGenerator
    {
        public static void PaintWallTiles(TileMapVisualizer tileMapVisualizer, IEnumerable<Vector2Int> floorPositions)
        {
            var wallPositions = FindWallPositions(floorPositions, Direction.Directions);
            foreach (var position in wallPositions)
            {
                tileMapVisualizer.PaintWallTile(position);
            }
        }

        public static HashSet<Vector2Int> FindWallPositions(IEnumerable<Vector2Int> floorPositions, List<Vector2Int> directions)
        {
            HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
            HashSet<Vector2Int> tempFloorPositions = floorPositions as HashSet<Vector2Int>;
            foreach(var position in floorPositions)
            {
                foreach(var direction in directions)
                {
                    var neighborPosition = position + direction;
                    if (!tempFloorPositions.Contains(neighborPosition))
                    {
                        wallPositions.Add(neighborPosition);
                    }
                }
            }
            return wallPositions;
        }
    }
}

