using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using System;

namespace WorldGeneration
{
    public class RoomGenerator : MonoBehaviour
    {
        private WalkGenerator walkGenerator;
        private CorridorGenerator corridorGenerator;
        [SerializeField]
        private TileMapVisualizer tileMapVisualizer;

        [SerializeField]
        private Vector2Int startPosition;

        [SerializeField]
        private int iterations = 10;
        [SerializeField]
        private int roomLength = 10;
        [SerializeField]
        private bool randomPositionEachIteration = true;


        [SerializeField]
        [Range(0.1f, 1f)]
        private float roomChance;
        [SerializeField]
        private int corridorLength, corridorCount;
        [SerializeField]
        private bool roomFirst;

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            walkGenerator = new WalkGenerator();
            corridorGenerator = new CorridorGenerator();
            tileMapVisualizer.Clear();
            if (roomFirst)
            {
                // Will generate room-first-based world
                GenerateRoom();
            }
            else
            {
                // Will generate corridor-first-based world
                CorridorFirstGeneration();
            }
        }

        private void CorridorFirstGeneration()
        {
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

            //Generates and visualizes the corridors
            GenerateCorridors(floorPositions, roomPositions);

            var rooms = CreateRooms(roomPositions);

            floorPositions.UnionWith(rooms);

            tileMapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.PaintWallTiles(tileMapVisualizer, floorPositions);
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> roomPositions)
        {
            HashSet<Vector2Int> rooms = new HashSet<Vector2Int>();
            int amountOfRooms = Mathf.RoundToInt(roomPositions.Count * roomChance);

            List<Vector2Int> roomsToCreate = roomPositions.OrderBy(x => Guid.NewGuid()).Take(amountOfRooms).ToList();

            foreach (var room in roomsToCreate)
            {
                var roomFloor = walkGenerator.Generate(room, roomLength);
                rooms.UnionWith(roomFloor);
            }

            return rooms;
        }

        private void GenerateRoom()
        {
            HashSet<Vector2Int> positions = RunGenerationAlgorithm();
            tileMapVisualizer.PaintFloorTiles(positions);
            WallGenerator.PaintWallTiles(tileMapVisualizer, positions);
        }

        public HashSet<Vector2Int> RunGenerationAlgorithm()
        {
            var currentPosition = startPosition;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < iterations; i++)
            {
                var path = walkGenerator.Generate(currentPosition, roomLength);
                floorPositions.UnionWith(path);
                if (randomPositionEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
            return floorPositions;
        }

        private void GenerateCorridors(HashSet<Vector2Int> floorpositions, HashSet<Vector2Int> roomPositions)
        {
            var currentPos = startPosition;
            roomPositions.Add(currentPos);
            for (int i = 0; i < corridorCount; i++)
            {
                var corridor = corridorGenerator.Generate(currentPos, corridorLength) as List<Vector2Int>;
                currentPos = corridor[corridor.Count - 1];
                roomPositions.Add(currentPos);
                floorpositions.UnionWith(corridor);
            }
        }

        public void Clear()
        {
            tileMapVisualizer.Clear();
        }
    }
}

