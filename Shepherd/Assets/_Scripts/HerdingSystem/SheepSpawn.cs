using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace HerdingSystem
{
    [Serializable]
    public class SheepSpawn
    {
        [SerializeField] private SheepSpawnData data;
        [SerializeField] private Transform parent;
        public Transform spawnPoint;
        public float spawnRadius;

        public void SpawnSheep(int num) {
            int numSheep = num;
            if (data.count + num > data.clampSheep.max) {
                numSheep = data.clampSheep.max - data.count;
            }
            
            for (int i = 0; i < numSheep; i++) {
                Vector3 spawnPos = GetValidSpawnPoint();
                Object.Instantiate(data.prefab, spawnPos, Quaternion.identity, parent);
                data.count++;
            }
        }

        public void SpawnSheep() => SpawnSheep(1);

        private Vector3 GetValidSpawnPoint() {
            for (int i = 0; i < 10; i++) { // 10 attempts
                Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
                randomOffset.y = 0;
                Vector3 candidate = spawnPoint.position + randomOffset;
                
                bool blocked = Physics.CheckSphere(candidate, spawnRadius);
                if (!blocked) return candidate;
            }
            
            return spawnPoint.position;
        }

        public void ClearSheepCount() => data.count = 0;
        
        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
            if (scene.name == "Main Scene") {
                spawnPoint = GameObject.Find("SheepSpawnPoint").transform;
                parent = GameObject.Find("Sheeps").transform;
            }
        }
    }
}
