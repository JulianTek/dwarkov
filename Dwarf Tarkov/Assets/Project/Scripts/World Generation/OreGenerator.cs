using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldGeneration
{
    public class OreGenerator : MonoBehaviour
    {
        [SerializeField]
        private float oreChance;
        [SerializeField]
        private List<GameObject> ores;
        public void GenerateOres(HashSet<Vector2Int> floorPositions)
        {
            foreach (Vector2Int floorPosition in floorPositions)
            {
                float value = Random.Range(0f, 100f);
                GameObject oreToSpawn = CheckIfOreGenerated(value);
                if (oreToSpawn != null)
                    Instantiate(oreToSpawn, new Vector3(floorPosition.x, floorPosition.y), Quaternion.identity);

            }
        }

        public GameObject CheckIfOreGenerated(float value)
        {
            foreach (GameObject ore in ores)
            {
                float min = ore.GetComponent<MineralHandler>().materialData.ItemAppearChanceMin;
                float max = ore.GetComponent<MineralHandler>().materialData.ItemAppearChanceMax;
                if (value >= min && value <= max)
                    return ore;
            }
            return null;
        }
    }
}

