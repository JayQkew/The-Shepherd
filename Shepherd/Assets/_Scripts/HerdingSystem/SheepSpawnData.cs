using UnityEngine;
using Utilities;

namespace HerdingSystem
{
    [CreateAssetMenu(fileName = "SheepSpawnData", menuName = "Herding System/SheepSpawn")]
    public class SheepSpawnData : ScriptableObject
    {
        public int count;
        public MinMaxInt clampSheep;
        public GameObject prefab;
    }
}
