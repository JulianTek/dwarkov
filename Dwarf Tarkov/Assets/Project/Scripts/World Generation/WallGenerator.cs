using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public static class WallGenerator
    {
        public static void PaintWallTiles(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> floorPositions)
        {
            var wallPositions = FindWallPositions(floorPositions, Direction.Directions);
            foreach (var position in wallPositions)
            {
                tileMapVisualizer.PaintWallTile(position);
            }
        }

        private static HashSet<Vector2Int> FindWallPositions(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
        {
            HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();  
            foreach(var position in floorPositions)
            {
                foreach(var direction in directions)
                {
                    var neighborPosition = position + direction;
                    if (!floorPositions.Contains(neighborPosition))
                    {
                        wallPositions.Add(neighborPosition);
                    }
                }
            }
            return wallPositions;
        }
    }
}

