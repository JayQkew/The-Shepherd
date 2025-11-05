using System.Collections.Generic;
using Creatures.Sheep;
using UnityEngine;
using Utilities;

namespace HerdingSystem
{
    [CreateAssetMenu(fileName = "SheepSpawnData", menuName = "Herding System/SheepSpawn")]
    public class SheepSpawnData : ScriptableObject
    {
        public MinMaxInt clampSheep;
        public GameObject prefab;
        [SerializeReference] public List<SheepData> sheepData = new();
    }
}
