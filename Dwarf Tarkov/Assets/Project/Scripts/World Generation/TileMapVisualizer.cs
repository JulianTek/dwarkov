using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration
{
    public class TileMapVisualizer : MonoBehaviour
    {
        [SerializeField]
        private Tilemap floorTileMap, wallTileMap;
        [SerializeField]
        private TileBase floorTile, wallTile;

        public void PaintFloorTiles(IEnumerable<Vector2Int> positions)
        {
            PaintTiles(positions, floorTileMap, floorTile);
        }

        internal void PaintWallTile(Vector2Int position)
        {
            PaintSingleTile(position, wallTileMap, wallTile);
        }

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
        {
            foreach (var position in positions)
            {
                PaintSingleTile(position, tileMap, tile);
            }
        }

        private void PaintSingleTile(Vector2Int position, Tilemap tileMap, TileBase tile)
        {
            var tilePos = tileMap.WorldToCell((Vector3Int)position);
            tileMap.SetTile(tilePos, tile);
        }

        public void Clear()
        {
            floorTileMap.ClearAllTiles();
            wallTileMap.ClearAllTiles();
        }
    }
}

