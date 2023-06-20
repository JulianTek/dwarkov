using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using System;
using EventSystem;

namespace WorldGeneration
{
    public class RoomGenerator : MonoBehaviour
    {
        private WalkGenerator walkGenerator;
        private CorridorGenerator corridorGenerator;
        [SerializeField]
        private OreGenerator oreGenerator;
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

        [SerializeField]
        private int amountOfExtractionZones;
        [SerializeField]
        private GameObject extractionZoneGameObject;

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            walkGenerator = new WalkGenerator();
            corridorGenerator = new CorridorGenerator();
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
            FindPlayerLocation(startPosition, floorPositions, WallGenerator.FindWallPositions(floorPositions, Direction.Directions));
            oreGenerator.GenerateOres(floorPositions);
            GenerateExtractionZones(floorPositions);
        }

        private void GenerateExtractionZones(HashSet<Vector2Int> floorPositions)
        {
            for (int i = 0; i < amountOfExtractionZones; i++)
            {
                Vector2Int pos = ExtractionPointsGenerator.GenerateExtractionSpots(floorPositions);
                Instantiate(extractionZoneGameObject, new Vector3(pos.x + 0.5f, pos.y + 0.5f), Quaternion.identity);
            }
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> roomPositions)
        {
            GenerateFirstRoom();
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

        private void GenerateFirstRoom()
        {
            HashSet<Vector2Int> positions = new HashSet<Vector2Int>()
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(0, 3),
                new Vector2Int(-1, 0),
                new Vector2Int(-1, 1),
                new Vector2Int(-1, 2),
                new Vector2Int(-1, 3),
                new Vector2Int(-1, 4),
                new Vector2Int(-2, 0),
                new Vector2Int(-2, 1),
                new Vector2Int(-2, 2),
                new Vector2Int(-2, 3),
                new Vector2Int(-3, 0),
                new Vector2Int(-3, 1),
                new Vector2Int(-3, 2),
                new Vector2Int(-3, 3),
                new Vector2Int(-3, 4),
            };
            tileMapVisualizer.PaintFloorTiles(positions);
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

        private void FindPlayerLocation(Vector2Int position, HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> wallPositions)
        {
            if (!CheckIfPlayerPositionValid(position, floorPositions, wallPositions))
            {
                FindPlayerLocation(GenerateRandomPosition(), floorPositions, wallPositions);
            }
            else
            {
                EventChannels.WorldGenerationEvents.OnMovePlayer?.Invoke(position);
            }
        }

        public void Clear()
        {
            tileMapVisualizer.Clear();
        }

        public void SetIterations(int iterations)
        {
            this.iterations = iterations;
        }

        public void SetRoomLength(int roomLength)
        {
            this.roomLength = roomLength;
        }

        public void SetCorridorLenght(int corridorLength)
        {
            this.corridorLength = corridorLength;
        }

        public void SetCorridorAmount(int corridorAmount)
        {
            corridorCount = corridorAmount;
        }

        public void SetRoomFirstGeneration(bool isRoomFirst)
        {
            roomFirst = isRoomFirst;
        }

        public bool CheckIfPlayerPositionValid(Vector2Int playerPosition, HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> wallPositions)
        {
            return floorPositions.Contains(playerPosition) && !wallPositions.Contains(playerPosition);
        }

        public Vector2Int GenerateRandomPosition()
        {
            return new Vector2Int(Mathf.RoundToInt(Random.Range(-30f, 30f)), Mathf.RoundToInt(Random.Range(-30f, 30f)));
        }
    }
}

